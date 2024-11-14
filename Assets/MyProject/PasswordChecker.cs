using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PasswordChecker : MonoBehaviour
{
    public TMP_InputField inputField; // テキストフィールド
    public string correctPassword; // 予め与えておいたパスワード
    [SerializeField]
    public UnityEvent correctM;//パスワード入力成功時に実行するメソッドを追加

    void Start()
    {
        // テキストフィールドにリスナーを追加
        inputField.onValueChanged.AddListener(CheckPassword);
    }

    void CheckPassword(string input)
    {
        // 入力されたテキストがパスワードと一致するか確認
        if (input == correctPassword)
        {
            ExecuteMethod();
        }
    }

    void ExecuteMethod()
    {
        // 任意のメソッドを実行
        Debug.Log("パスワードが一致しました！");
        // ここに実行したい処理を記述
        correctM?.Invoke();
        
    }
}