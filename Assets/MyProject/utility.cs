using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class utility
{
    public static int TransformAndSum(int[,] matrix)//�A���i�̑I�𐄈ڍs���int�l�Ƀ��j�[�N�ϊ�����.�n�b�V���ł͂Ȃ�.
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        int[] result = new int[rows];

        for (int i = 0; i < rows; i++)
        {
            int rowSum = 0;
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] *= (j + 1);
                rowSum += matrix[i, j];
            }
            result[i] = rowSum;
        }
        //�ꎟ���z��𐔒l�ɕϊ�����
        if (result[0] == 0)//�擪��0����int�l�ɕϊ��ł��Ȃ��̂ŏ�������
        {
            return 0;
        }
        else
        {
            // �z��̗v�f�𕶎���Ƃ��ĘA��
            string concatenatedString = string.Join("", result.Select(n => n.ToString()));
            //���l�ɕϊ�
            return int.Parse(concatenatedString);
        }
    }
    public static bool AreArraysEqual(int[,] array1, int[,] array2)//�Q�����z�񂪈�v���Ă��邩���ׂ�.
    {
        // �z��̃T�C�Y���r
        if (array1.GetLength(0) != array2.GetLength(0) || array1.GetLength(1) != array2.GetLength(1))
        {
            return false;
        }

        // �e�v�f���r
        for (int i = 0; i < array1.GetLength(0); i++)
        {
            for (int j = 0; j < array1.GetLength(1); j++)
            {
                if (array1[i, j] != array2[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }
}
