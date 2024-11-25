using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using MyProject;

public class PasswordChecker : MonoBehaviour
{
    public TMP_InputField inputField; // テキストフィールド
    public string correctPassword; // 予め与えておいたパスワード
    public GameObject KitakuSceneObject;
    public GameObject PasswordObject;
    [SerializeField]
    public UnityEvent correctM;//パスワード入力成功時に実行するメソッドを追加

    void Start()
    {
        //パスワードをiniファイルから読み込む
        IniReader iniReader = new IniReader();
        iniReader.LoadIniFile("workshopSimulationPassword");
        switch ((int) char.GetNumericValue(PasswordObject.name[PasswordObject.name.Length-1]))
        {
            case 1:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length+10).ToString(), "会社");
                break;
            case 2:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "JR");
                break;
            case 3:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "地下鉄+ゆとりーとライン+徒歩");
                break;
            case 4:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "名鉄+タクシー");
                break;
            case 5:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "地下鉄+タクシー");
                break;
            case 6:
                correctPassword = iniReader.GetIniValue((KitakuSceneObject.name.Length + 10).ToString(), "社用車");
                break;
            default:
                Debug.Log("パスワード設定オブジェクトの名前が不正です.");
                break;

        }
        
        Debug.Log($"correctPassword: {correctPassword}");
        // テキストフィールドにリスナーを追加
        inputField.onValueChanged.AddListener(CheckPassword);
        //パスワードがないか確認
        CheckPassword(string.Empty);//パスワードが設定されてない時のため
    }

    void Update(){
        
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