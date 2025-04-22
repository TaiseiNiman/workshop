using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using System.Collections;
using System.Text;
using System.IO;
using WebSocketSharp;
using Unity.Collections;
using UnityEngine.Events;

public class WebSocketImageSender : MonoBehaviour
{
    public ARCameraManager arCameraManager;
    public Camera arUnityCamera;
    public float sendInterval = 1.0f; // 秒間隔で送信
    private float timer = 0f;

    [SerializeField]
    public UnityEvent<string> SendQueue;

    void Start()
    {
        //ws = new WebSocket("ws://YOUR_SERVER_IP:PORT");
        //ws.OnOpen += (sender, e) => Debug.Log("WebSocket Connected");
        //ws.OnError += (sender, e) => Debug.Log("WebSocket Error: " + e.Message);
        //ws.OnClose += (sender, e) => Debug.Log("WebSocket Closed");
        //ws.Connect();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= sendInterval)
        {
            timer = 0f;
            if (arCameraManager.TryAcquireLatestCpuImage(out XRCpuImage image))
            {
                StartCoroutine(SendImageAndCameraParams(image));
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

        // --- 内部パラメータ行列（K） ---
        float fx = arUnityCamera.projectionMatrix[0, 0];
        float fy = arUnityCamera.projectionMatrix[1, 1];
        float cx = arUnityCamera.pixelWidth / 2f;
        float cy = arUnityCamera.pixelHeight / 2f;

        string K = $"{fx},{0},{cx};{0},{fy},{cy};{0},{0},{1}";

        // --- 外部パラメータ行列（R, t） ---
        Matrix4x4 worldToCam = arUnityCamera.worldToCameraMatrix;
        Vector3 camPos = arUnityCamera.transform.position;
        Quaternion camRot = arUnityCamera.transform.rotation;

        string T = $"{camPos.x},{camPos.y},{camPos.z}";
        string R = $"{camRot.x},{camRot.y},{camRot.z},{camRot.w}";

        // --- JSON形式でまとめて送信 ---
        var json = new
        {
            image = Convert.ToBase64String(jpgBytes),
            K = K,
            R = R,
            t = T
        };

        string jsonStr = JsonUtility.ToJson(json);
        SendQueue?.Invoke(jsonStr);

        yield return null;
    }

}
