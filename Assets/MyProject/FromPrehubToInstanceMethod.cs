using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public class FromPrehubToInstanceMethod : MonoBehaviour
{
    /*
     * このスクリプトをアタッチするプレハブはタグネームを設定しなければならない.
     * タグネームはインスペクター上部のタグマネージャーに存在するタグネームのいずれかを設定しなければならない
     * 
     */
    [SerializeField]
    private string _tagName;

    public string tagName //ここでインスペクター上からタグネームを設定すること
    {
        get { return _tagName; }
        set
        {
            _tagName = value;
            gameObject.tag = _tagName;
        }
    }
    //インスタンスを初期化する.
    [SerializeField]
    public UnityEvent<GameObject> initialized;
    public void Initialized(GameObject InstanceObject, GameObject param)
    {
        InspectEvent(initialized, InstanceObject, param);
    }
    //このプレハブから追加されたインスタンス領域上でドラッグされたか調べ,されていればイベントリスナーを実行
    [SerializeField]
    public UnityEvent<GameObject> onDropInstance;
    public void PrehubInstanceTrigger(GameObject dub, Canvas canvas, RectTransform dubRect) {

        GameObject[] instances = GameObject.FindGameObjectsWithTag(gameObject.tag);

        // 結果をログに出力
        foreach (GameObject instance in instances)
        {
            if(RectTransformUtility.RectangleContainsScreenPoint(instance.GetComponent<RectTransform>(), dubRect.position, canvas.worldCamera))
            {
                InspectEvent(onDropInstance, instance, dub);
                return;
            }
            Debug.Log("Found instance: " + instance.name + " at position " + instance.transform.position);
        }
        return;
    }



    public void InspectEvent(UnityEvent<GameObject> unityEvent, GameObject instanceObject, GameObject param)
    {
        // UnityEventの内部フィールドを取得
        var field = typeof(UnityEventBase).GetField("m_Calls", BindingFlags.NonPublic | BindingFlags.Instance);
        var invocationList = field.GetValue(unityEvent);

        // リスナーのリストを取得
        var persistentCalls = invocationList.GetType().GetField("m_PersistentCalls", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(invocationList);
        var calls = persistentCalls.GetType().GetField("m_Calls", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(persistentCalls) as System.Collections.IList;

        foreach (var call in calls)
        {
            // リスナーのメソッド名とコンポーネント名を取得
            var methodName = call.GetType().GetField("m_MethodName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(call) as string;
            var target = call.GetType().GetField("m_Target", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(call) as MonoBehaviour;

            if (target != null)
            {
                Debug.Log("Method: " + methodName + ", Component: " + target.GetType().Name);

                // コンポーネントの型を取得
                var componentType = target.GetType();

                // インスタンスオブジェクトからコンポーネントを取得
                var component = instanceObject.GetComponent(componentType);

                if (component != null)
                {
                    // メソッドを取得して呼び出す
                    var method = componentType.GetMethod(methodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (method != null)
                    {
                        method.Invoke(component, new object[] { param });
                    }
                    else
                    {
                        Debug.LogError("Method not found: " + methodName);
                    }
                }
                else
                {
                    Debug.LogError("Component not found: " + componentType.Name);
                }
            }
        }
    }
}
