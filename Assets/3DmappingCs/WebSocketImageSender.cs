using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using Unity.Collections;
using System;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using Newtonsoft.Json;

public class CameraImageSender : MonoBehaviour
{
    public Camera arUnityCamera;
    public ARCameraManager cameraManager;
    [System.Serializable]
    public class ZoneingParams
    {
        // ゾーンの角度情報
        public float phi;
        public float theta;
        public int x;
        public int y;

        // 静止画
        public string jpgBase64;

        // 内部パラメータ行列 (3x3)
        public float[][] K;

        // 外部パラメータ: カメラ回転（四元数）
        public float[] R; // 長さ4

        // 外部パラメータ: カメラ位置ベクトル
        public float[] T; // 長さ3
    }

    [SerializeField]
    public UnityEvent<string> OnZoneingEvent;

    private int initial = 0;
    private float seidopram = 2.0f;
    private int x_int = 0;
    private int y_int = 0;
    public void DelaySub(string jsonStr)
    {
        StartCoroutine(CaptureAndSendLoop(jsonStr));
    }

    IEnumerator CaptureAndSendLoop(string jsonStr)
    {
        Dictionary<string, object> json = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonStr);
        if (json["meta"].ToString() == "delay")
        {
            int delay = int.Parse(json["contents"].ToString());
            while (true)
            {
                if (cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
                {
                    yield return StartCoroutine(SendImageAndCameraParams(image));
                }
                yield return new WaitForSeconds(delay); // n秒おき
            }
        }
    }

    IEnumerator SendImageAndCameraParams(XRCpuImage image)
    {
        var conversionParams = new XRCpuImage.ConversionParams
        {
            inputRect = new RectInt(0, 0, image.width, image.height),
            outputDimensions = new Vector2Int(image.width, image.height),
            outputFormat = TextureFormat.RGBA32,
            transformation = XRCpuImage.Transformation.None
        };

        var rawData = new NativeArray<byte>(image.GetConvertedDataSize(conversionParams), Allocator.Temp);
        image.Convert(conversionParams, rawData);
        image.Dispose();

        Texture2D tex = new Texture2D(image.width, image.height, TextureFormat.RGBA32, false);
        tex.LoadRawTextureData(rawData);
        tex.Apply();
        rawData.Dispose();

        byte[] jpgBytes = tex.EncodeToJPG(50);
        Destroy(tex);

        // カメラ内部パラメータ
        float fx = arUnityCamera.projectionMatrix[0, 0];
        float fy = arUnityCamera.projectionMatrix[1, 1];
        float cx = arUnityCamera.pixelWidth / 2f;
        float cy = arUnityCamera.pixelHeight / 2f;
        float[][] K = new float[][] { new float[] { fx, 0, cx }, new float[] { 0, fy, cy }, new float[] { 0, 0, 1 } };

        //初期設定 1回のみ
        if (initial == 0)
        {
            float W = arUnityCamera.pixelWidth;
            float H = arUnityCamera.pixelHeight;
            float hfov = Mathf.Atan(1 / fx);
            float vfov = Mathf.Atan(1 / fy);
            Debug.Log($"水平視野角 (rad): {hfov}, 垂直視野角 (rad): {vfov}");
            float target = seidopram * Mathf.PI / (hfov * (1 - Mathf.Cos(vfov)));

            float x = SolveX(target);

            x_int = Mathf.RoundToInt(x);
            y_int = Mathf.RoundToInt(x_int / 2);
            Debug.Log($"分割数 x: {x_int}, y: {y_int}");
            initial = 1;
        }

        float SolveX(float target)
        {
            // ニュートン法で近似解を求める
            float x = 5f; // 初期値
            for (int i = 0; i < 20; i++)
            {
                float fx = x / (1 - Mathf.Cos(Mathf.PI / x)) - target;
                float dfx = (1 - Mathf.Cos(Mathf.PI / x)) + x * Mathf.PI * Mathf.Sin(Mathf.PI / x) / (x * x);
                dfx /= Mathf.Pow(1 - Mathf.Cos(Mathf.PI / x), 2);

                x -= fx / dfx;

                if (Mathf.Abs(fx) < 1e-6f)
                    break;
            }
            return x;
        }

        // カメラ外部パラメータ
        Vector3 camPos = arUnityCamera.transform.position;
        Quaternion camRot = arUnityCamera.transform.rotation;
        float[] T = { camPos.x, camPos.y, camPos.z };
        float[] R = { camRot.x, camRot.y, camRot.z, camRot.w };//回転を表す四元数qの各成分

        //正規化された視線ベクトルの球面座標表示φ,Θを得る
        // 視線ベクトル（Unity の演算は内部で q v q* になる）
        Vector3 forward = camRot * Vector3.forward;

        forward.Normalize();

        Debug.Log($"視線ベクトル: {forward}");

        // φ, Θ
        float phi = Mathf.Acos(forward.z);
        float theta = Mathf.Atan2(forward.y, forward.x);

        Debug.Log($"φ (rad): {phi}, Θ (rad): {theta}");

        //ゾーニングと静止画を学習データに加えるかどうかを決定する.
        var param = new ZoneingParams { 
            phi = phi, theta = theta, x = x_int, y = y_int,
            jpgBase64 = Convert.ToBase64String(jpgBytes),
            K = K,
            T = T,
            R = R
        };
        string json = JsonConvert.SerializeObject(param);

        OnZoneingEvent.Invoke(json);

        //var payload = new Dictionary<string, object>
        //{
        //    { "image" , Convert.ToBase64String(jpgBytes) },
        //    { "K" , K },
        //    { "R" , R },
        //    { "t" , T }
        //};
        /*
        var payload = new Dictionary<string, object>
        {
            {"contents", new Dictionary<string, object>{
            { "image" , Convert.ToBase64String(jpgBytes) },
            { "K" , K },
            { "R" , R },
            { "t" , T } }
            },
            {"meta", "3Ddata" }
        };
        
        string jsonStr = JsonConvert.SerializeObject(payload);
        SendQueue?.Invoke(jsonStr);
        */
        yield return null;
    }
}

