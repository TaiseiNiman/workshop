using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

// JSON送信用のシリアライズ可能なクラス
[Serializable]
public class ImagePayload
{
    public string image;
    public string K;
    public string R;
    public string t;
}

public class CameraImageSender : MonoBehaviour
{
    public Camera arUnityCamera; // Unity上のARカメラ
    public Action<string> SendQueue; // JSONを送信するイベント

    IEnumerator SendImageAndCameraParams(XRCpuImage image)
    {
        // --- 画像変換パラメータ設定 ---
        var conversionParams = new XRCpuImage.ConversionParams
        {
            inputRect = new RectInt(0, 0, image.width, image.height),
            outputDimensions = new Vector2Int(image.width, image.height),
            outputFormat = TextureFormat.RGBA32,
            transformation = XRCpuImage.Transformation.None
        };

        // --- 画像データを取得し、JPGへエンコード ---
        var rawData = new NativeArray<byte>(image.GetConvertedDataSize(conversionParams), Allocator.Temp);
        image.Convert(conversionParams, rawData);
        image.Dispose();

        Texture2D tex = new Texture2D(image.width, image.height, TextureFormat.RGBA32, false);
        tex.LoadRawTextureData(rawData);
        tex.Apply();
        rawData.Dispose();

        byte[] jpgBytes = tex.EncodeToJPG(50);
        Destroy(tex);

        // --- 内部パラメータ行列（K） ---
        float fx = arUnityCamera.projectionMatrix[0, 0];
        float fy = arUnityCamera.projectionMatrix[1, 1];
        float cx = arUnityCamera.pixelWidth / 2f;
        float cy = arUnityCamera.pixelHeight / 2f;

        string K = $"{fx},{0},{cx};{0},{fy},{cy};{0},{0},{1}";

        // --- 外部パラメータ（カメラ位置と姿勢） ---
        Vector3 camPos = arUnityCamera.transform.position;
        Quaternion camRot = arUnityCamera.transform.rotation;

        string T = $"{camPos.x},{camPos.y},{camPos.z}";
        string R = $"{camRot.x},{camRot.y},{camRot.z},{camRot.w}";

        // --- JSON形式で送信 ---
        ImagePayload payload = new ImagePayload
        {
            image = Convert.ToBase64String(jpgBytes),
            K = K,
            R = R,
            t = T
        };

        string jsonStr = JsonUtility.ToJson(payload);
        Debug.Log("送信JSON: " + jsonStr); // 確認用ログ
        SendQueue?.Invoke(jsonStr);

        yield return null;
    }
}
