using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class OnButtonWithErrorMessage : MonoBehaviour
{
    public ExampleScript Example;

    private UnityEvent<int> move = new UnityEvent<int>();
    public UnityEvent<string> MoveSceneNumberChanged;
    private string _MoveSceneNumber;
    public string MoveSceneNumber
    {
        get
        {
            return _MoveSceneNumber;
        }
        set
        {
            _MoveSceneNumber = value;
            MoveSceneNumberChanged?.Invoke(value);
        }
    }//
    public GameObject SceneObject;//
    private bool isResult = true;
    //public GameObject ActiveScreenObject;
    private UnityEvent<string> statusSubmitted = new UnityEvent<string>();
    public KitakuSenniInitialize parent;
    public string TestMoveSceneNumber;

    [SerializeField]
    public UnityEvent<string> TestOnFault;
    [SerializeField]
    public UnityEvent<string> TestOnSuccess;

    private float seconds;

    void Start()
    {
        //リスナーの設定
        move.AddListener(parent.parent.KitakuSenniUpdate);
        //リスナーの設定
        statusSubmitted.AddListener(parent.parent.simulation.SendText);
        parent.parent.simulation.onMessage1.AddListener(MessagesReserve);
        //プロパティの初期化
        seconds = 0f;

    }
    private void OnDestroy()
    {
        //リスナーの破棄
        parent.parent.simulation.onMessage1.RemoveListener(MessagesReserve);

    }
    void Update()
    {
        seconds += Time.deltaTime;
        if (seconds >= 1.0f)
        {
            for (var i = 0; i < 6; i++)
            {
                StatusSubmit("TEST", parent.parent.userId, parent.parent.KitakuStateId + $"{i + 1}");
            }
            seconds = 0;
        }


    }

    public void StatusSubmit(string type, string id, string status)
    {
        statusSubmitted?.Invoke($"{type}:{id}:{status}");
    }

    public void MessagesReserve(string messages)
    {
        string result = messages.Split(':')[0];
        string before = messages.Split(':')[2];
        string update = messages.Split(':')[3];
        if (before == parent.parent.userId)
        {
            //ユーザーが一致するときのみ処理を行う
            switch (result)
            {
                case "your id is incorrect":
                    break;
                case "your simulation status is incorrect":
                    break;
                case "your selected simulation status is incorrect":
                    {
                        //TEST失敗時の処理
                        TestOnFault?.Invoke(update[update.Length - 1].ToString());
                        break;
                    }
                case "your selected simulation status is correct":
                    {
                        //TEST成功時の処理
                        TestMoveSceneNumber = update[update.Length - 1].ToString();
                        TestOnSuccess?.Invoke(update[update.Length - 1].ToString());
                        break;
                    }
                case "your selected simulation status was updated":
                    {
                        //ACTION成功時の処理
                        MoveSceneNumber = update[update.Length - 1].ToString();
                        break;
                    }
                case "your selected simulation status was cleared":
                    {
                        //クリア時
                        MoveSceneNumber = null;
                        break;
                    }
                case "Messages are incorrect please <TEST|ACTION>":
                    break;
                default:
                    break;
            }
        }
        else
        {
            //処理を行わない
        }
    }

    public void OnButtonClick()
    {
        int num;
        foreach (char c in SceneObject.name)
        {
            if (c != '1')
            {
                isResult = false;
            }
        }
        // int.TryParseを使用する方法
        if (MoveSceneNumber != null)
        {
            if (MoveSceneNumber == "1")
            {
                move?.Invoke(0);
            }
            else
            {
                if (int.TryParse(MoveSceneNumber, out num))
                {
                    Debug.Log("int.TryParse: " + num);
                    move?.Invoke(num);//
                }
                else
                {
                    Debug.Log("変換に失敗しました");
                }
            }
        }

    }
}