using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class utility
{
    public static int TransformAndSum(int[,] matrix)//帰宅手段の選択推移行列をint値にユニーク変換する.ハッシュではない.
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
        //一次元配列を数値に変換する
        if (result[0] == 0)//先頭が0だとint値に変換できないので条件分岐
        {
            return 0;
        }
        else
        {
            // 配列の要素を文字列として連結
            string concatenatedString = string.Join("", result.Select(n => n.ToString()));
            //数値に変換
            return int.Parse(concatenatedString);
        }
    }
    public static bool AreArraysEqual(int[,] array1, int[,] array2)//２次元配列が一致しているか調べる.
    {
        // 配列のサイズを比較
        if (array1.GetLength(0) != array2.GetLength(0) || array1.GetLength(1) != array2.GetLength(1))
        {
            return false;
        }

        // 各要素を比較
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
