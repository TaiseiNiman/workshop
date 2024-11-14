using UnityEngine;
using TMPro;
using System.Linq;

public class TMPDropdownManager : MonoBehaviour
{
    public TMP_Dropdown tmpDropdown; // TextMeshProのドロップダウンの参照

    // オプションを追加するメソッド
    public void AddOption(string optionText)
    {
        if (tmpDropdown != null)
        {
            // 既存のオプションに同じテキストがあるか確認
            bool optionExists = tmpDropdown.options.Any(option => option.text == optionText);

            if (!optionExists)
            {
                // 新しいオプションを追加
                TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData(optionText);
                tmpDropdown.options.Add(newOption);
                Debug.Log("オプションが追加されました: " + optionText);
            }
            else
            {
                Debug.LogWarning("同じテキストのオプションが既に存在します: " + optionText);
            }

            // ドロップダウンの表示を更新
            tmpDropdown.RefreshShownValue();
        }//ssaaajj
        else
        {
            Debug.LogWarning("ドロップダウンが設定されていません");
        }
    }

    // 指定された文字列を持つオプションを削除するメソッド
    public void RemoveOption(string optionText)
    {
        if (tmpDropdown != null)
        {
            // オプションのリストから該当するオプションを検索
            var option = tmpDropdown.options.FirstOrDefault(o => o.text == optionText);

            if (option != null)
            {
                // 該当するオプションを削除
                tmpDropdown.options.Remove(option);
                Debug.Log("オプションが削除されました: " + optionText);
            }
            else
            {
                Debug.LogWarning("指定された文字列のオプションが見つかりません: " + optionText);
            }

            // ドロップダウンの表示を更新
            tmpDropdown.RefreshShownValue();
        }
        else
        {
            Debug.LogWarning("ドロップダウンが設定されていません");
        }
    }

    // 指定された文字列を持つオプションのインデックスにバリュー値を変更するメソッド
    public void SetDropdownValue(string optionText)
    {
        if (tmpDropdown != null)
        {
            int index = tmpDropdown.options.FindIndex(option => option.text == optionText);

            if (index != -1)
            {
                tmpDropdown.value = index;
                Debug.Log("ドロップダウンのバリュー値が変更されました: " + optionText);
            }
            else
            {
                Debug.LogWarning("指定された文字列のオプションが見つかりません: " + optionText);
            }
        }
        else
        {
            Debug.LogWarning("ドロップダウンが設定されていません");
        }
    }
}
