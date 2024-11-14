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
    //�N���b�N���̃��\�b�h
    public void ServerSettingsIniFileOnClick() {
        string Path = FileSelector.OpenFileDialog();
        if (Path != "") {
            var inifile = new IniFile(Path);//ini�t�@�C�����擾
            iniText.text = inifile.ReadAll();//ini�t�@�C���S�̂𕶎���Ƃ��Ď擾
        }
    } 
}
