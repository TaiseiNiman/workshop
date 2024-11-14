using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyProject;

public class ServerSettingsIniFileInput : MonoBehaviour
{
    public TMP_InputField iniText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //クリック時のメソッド
    public void ServerSettingsIniFileOnClick() {
        string Path = FileSelector.OpenFileDialog();
        if (Path != "") {
            var inifile = new IniFile(Path);//iniファイルを取得
            iniText.text = inifile.ReadAll();//iniファイル全体を文字列として取得
        }
    } 
}
