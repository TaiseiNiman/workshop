using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using Unity.Collections;
using System;
using System.Collections;
using UnityEngine.Events;


public class CameraImageSender : MonoBehaviour
{
    public Camera arUnityCamera;
    public ARCameraManager cameraManager;
    [SerializeField]
    public UnityEvent<string> SendQueue;
    private void Start()
    {
        StartCoroutine(CaptureAndSendLoop());
    }

    IEnumerator CaptureAndSendLoop()
    {
        while (true)
        {
            if (cameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                yield return StartCoroutine(SendImageAndCameraParams(image));
            }
            yield return new WaitForSeconds(2.0f); // 1秒おき
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
        string K = $"{fx},0,{cx};0,{fy},{cy};0,0,1";

        // カメラ外部パラメータ
        Vector3 camPos = arUnityCamera.transform.position;
        Quaternion camRot = arUnityCamera.transform.rotation;
        string T = $"{camPos.x},{camPos.y},{camPos.z}";
        string R = $"{camRot.x},{camRot.y},{camRot.z},{camRot.w}";

        ImagePayload payload = new ImagePayload
        {
            image = Convert.ToBase64String(jpgBytes),
            K = K,
            R = R,
            t = T
        };

        string jsonStr = JsonUtility.ToJson(payload);
        SendQueue?.Invoke(jsonStr);

        yield return null;
    }
}

