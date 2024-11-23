using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class SpriteLoader : MonoBehaviour
{
    private string basePath = "image"; // ��{�p�X
    public Image targetImage; // �X�v���C�g��\������Image�R���|�[�l���g
    public GameObject kitakuScene;

    void Start()
    {
        string inputString = kitakuScene.name.ToString(); // ��Ƃ��Ă̓��͕�����
        Sprite targetSprite = GetTargetSprite(inputString);
        if (targetSprite != null)
        {
            Debug.Log("�擾�����X�v���C�g: " + targetSprite.name);
            SetImageSprite(targetSprite);
        }
        else
        {
            Debug.LogWarning("�X�v���C�g��������܂���ł����B");
        }
    }

    Sprite GetTargetSprite(string input)
    {
        // ���͕����񂩂�ő�̐������擾
        char maxChar = input.Max();
        int maxInt = int.Parse(maxChar.ToString());

        // '1'�̐��𐔂���
        int countOfOnes = input.Count(c => c == '1');

        // �p�X���\�z
        string path = $"{basePath}/{maxInt}/{countOfOnes}";
        Debug.Log("Loading sprites from path: " + path);

        // �t�H���_�[���̂��ׂẴX�v���C�g�����[�h
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);

        if (sprites.Length == 0)
        {
            Debug.LogWarning("�w�肳�ꂽ�p�X�ɃX�v���C�g��������܂���ł���: " + path);
            return null;
        }

        // �X�v���C�g���̖����̐��������Ń\�[�g
        List<Sprite> sortedSprites = sprites.OrderBy(sprite => ExtractNumber(sprite.name)).ToList();

        // �C�ӂ̐����̐��𐔂���
        int countOfMaxInt = input.Count(c => c == maxChar);

        // (countOfMaxInt - 1)�Ԗڂ̃X�v���C�g���擾
        int targetIndex = countOfMaxInt - 1;
        if (targetIndex >= 0 && targetIndex < sortedSprites.Count)
        {
            return sortedSprites[targetIndex];
        }
        else
        {
            Debug.LogWarning("�w�肳�ꂽ�C���f�b�N�X�ɃX�v���C�g�����݂��܂���: " + targetIndex);
            Debug.LogWarning("�V�~�����[�V�������I�������܂�");
            return Resources.LoadAll<Sprite>("Image/SimulationEndSprites")[0];
        }
    }

    int ExtractNumber(string name)
    {
        // ���O�̖����̐��������𒊏o
        Match match = Regex.Match(name, @"\d+$");
        return match.Success ? int.Parse(match.Value) : 0;
    }

    void SetImageSprite(Sprite sprite)
    {
        if (targetImage != null)
        {
            targetImage.sprite = sprite;

            // RectTransform�̐ݒ�
            RectTransform rt = targetImage.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(1, 1);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }
        else
        {
            Debug.LogWarning("targetImage���ݒ肳��Ă��܂���B");
        }
    }
}