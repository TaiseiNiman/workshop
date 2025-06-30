using UnityEngine;
using UnityEngine.UI; // Text 用

public class LookDirectionVisualizer : MonoBehaviour
{
    [Header("=== 必要な参照 ===")]
    public Camera arCamera;
    public Transform arrow; // 矢印の 3D モデル

    [Header("=== HUD 表示 ===")]
    public Text hudText; // HUD に表示するテキスト

    void Update()
    {
        // === カメラ姿勢 ===
        Quaternion camRot = arCamera.transform.rotation;
        Vector3 camPos = arCamera.transform.position;

        // === カメラ視線ベクトル ===
        Vector3 forward = camRot * Vector3.forward;

        // === カメラの右方向 ===
        Vector3 right = camRot * Vector3.right;

        // === カメラの上方向 ===
        Vector3 up = camRot * Vector3.up;

        //// === 矢印の位置 ===
        //arrow.position = camPos;

        //// === 矢印の姿勢 ===
        //// LookRotation(forward, up) でロールも含める
        //arrow.rotation = Quaternion.LookRotation(forward, up);

        // === HUD 更新 ===
        if (hudText != null)
        {
            hudText.text = $"Camera Pos: {camPos:F3}\n"
                         + $"Forward: {forward:F3}\n"
                         + $"Up: {up:F3}";
        }
    }
}
