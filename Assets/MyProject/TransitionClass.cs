using System.Collections;
using System.Collections.Generic;

namespace MyProject{
    public class TransitionClass
    {
        //各帰宅困難状況の状態を表すプロパティを設定
        private readonly int[,] trasitionArray = new int[,] {
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
            {1, 2, 3, 4},
        };
        public TransitionClass()//時刻と帰宅手段によって分岐する帰宅困難状況に応じたインスタンスを生成する.
        {
            
        }
    } 
}


