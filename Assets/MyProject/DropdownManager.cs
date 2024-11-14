using UnityEngine;
using TMPro;
using System.Linq;

public class TMPDropdownManager : MonoBehaviour
{
    public TMP_Dropdown tmpDropdown; // TextMeshPro�̃h���b�v�_�E���̎Q��

    // �I�v�V������ǉ����郁�\�b�h
    public void AddOption(string optionText)
    {
        if (tmpDropdown != null)
        {
            // �����̃I�v�V�����ɓ����e�L�X�g�����邩�m�F
            bool optionExists = tmpDropdown.options.Any(option => option.text == optionText);

            if (!optionExists)
            {
                // �V�����I�v�V������ǉ�
                TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData(optionText);
                tmpDropdown.options.Add(newOption);
                Debug.Log("�I�v�V�������ǉ�����܂���: " + optionText);
            }
            else
            {
                Debug.LogWarning("�����e�L�X�g�̃I�v�V���������ɑ��݂��܂�: " + optionText);
            }

            // �h���b�v�_�E���̕\�����X�V
            tmpDropdown.RefreshShownValue();
        }//ssaaajj
        else
        {
            Debug.LogWarning("�h���b�v�_�E�����ݒ肳��Ă��܂���");
        }
    }

    // �w�肳�ꂽ����������I�v�V�������폜���郁�\�b�h
    public void RemoveOption(string optionText)
    {
        if (tmpDropdown != null)
        {
            // �I�v�V�����̃��X�g����Y������I�v�V����������
            var option = tmpDropdown.options.FirstOrDefault(o => o.text == optionText);

            if (option != null)
            {
                // �Y������I�v�V�������폜
                tmpDropdown.options.Remove(option);
                Debug.Log("�I�v�V�������폜����܂���: " + optionText);
            }
            else
            {
                Debug.LogWarning("�w�肳�ꂽ������̃I�v�V������������܂���: " + optionText);
            }

            // �h���b�v�_�E���̕\�����X�V
            tmpDropdown.RefreshShownValue();
        }
        else
        {
            Debug.LogWarning("�h���b�v�_�E�����ݒ肳��Ă��܂���");
        }
    }

    // �w�肳�ꂽ����������I�v�V�����̃C���f�b�N�X�Ƀo�����[�l��ύX���郁�\�b�h
    public void SetDropdownValue(string optionText)
    {
        if (tmpDropdown != null)
        {
            int index = tmpDropdown.options.FindIndex(option => option.text == optionText);

            if (index != -1)
            {
                tmpDropdown.value = index;
                Debug.Log("�h���b�v�_�E���̃o�����[�l���ύX����܂���: " + optionText);
            }
            else
            {
                Debug.LogWarning("�w�肳�ꂽ������̃I�v�V������������܂���: " + optionText);
            }
        }
        else
        {
            Debug.LogWarning("�h���b�v�_�E�����ݒ肳��Ă��܂���");
        }
    }
}
