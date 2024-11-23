using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SpriteLoader : MonoBehaviour
{
    private string basePath = "image"; // 基本パス
    public Image targetImage; // スプライトを表示するImageコンポーネント
    public GameObject kitakuScene;

    void Start()
    {
        string inputString = kitakuScene.name.ToString(); // 例としての入力文字列
        Sprite targetSprite = GetTargetSprite(inputString);
        if (targetSprite != null)
        {
            Debug.Log("取得したスプライト: " + targetSprite.name);
            SetImageSprite(targetSprite);
        }
        else
        {
            Debug.LogWarning("スプライトが見つかりませんでした。");
        }
    }

    Sprite GetTargetSprite(string input)
    {
        // 入力文字列から最大の整数を取得
        char maxChar = input.Max();
        int maxInt = int.Parse(maxChar.ToString());

        // '1'の数を数える
        int countOfOnes = input.Count(c => c == '1');

        // パスを構築
        string path = $"{basePath}/{maxInt}/{countOfOnes}";
        Debug.Log("Loading sprites from path: " + path);

        // フォルダー内のすべてのスプライトをロード
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);

        if (sprites.Length == 0)
        {
            Debug.LogWarning("指定されたパスにスプライトが見つかりませんでした: " + path);
            return null;
        }

        // スプライト名の末尾の整数部分でソート
        List<Sprite> sortedSprites = sprites.OrderBy(sprite => ExtractNumber(sprite.name)).ToList();

        // 任意の整数の数を数える
        int countOfMaxInt = input.Count(c => c == maxChar);

        // (countOfMaxInt - 1)番目のスプライトを取得
        int targetIndex = countOfMaxInt - 1;
        if (targetIndex >= 0 && targetIndex < sortedSprites.Count)
        {
            return sortedSprites[targetIndex];
        }
        else
        {
            Debug.LogWarning("指定されたインデックスにスプライトが存在しません: " + targetIndex);
            Debug.LogWarning("シミュレーションを終了させます");
            return Resources.LoadAll<Sprite>("Image/SimulationEndSprites")[0];
        }
    }

    int ExtractNumber(string name)
    {
        // 名前の末尾の整数部分を抽出
        Match match = Regex.Match(name, @"\d+$");
        return match.Success ? int.Parse(match.Value) : 0;
    }

    void SetImageSprite(Sprite sprite)
    {
        if (targetImage != null)
        {
            targetImage.sprite = sprite;

            // RectTransformの設定
            RectTransform rt = targetImage.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(1, 1);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }
        else
        {
            Debug.LogWarning("targetImageが設定されていません。");
        }
    }
}