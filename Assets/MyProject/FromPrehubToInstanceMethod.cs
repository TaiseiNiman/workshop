using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public class FromPrehubToInstanceMethod : MonoBehaviour
{
    /*
     * ���̃X�N���v�g���A�^�b�`����v���n�u�̓^�O�l�[����ݒ肵�Ȃ���΂Ȃ�Ȃ�.
     * �^�O�l�[���̓C���X�y�N�^�[�㕔�̃^�O�}�l�[�W���[�ɑ��݂���^�O�l�[���̂����ꂩ��ݒ肵�Ȃ���΂Ȃ�Ȃ�
     * 
     */
    [SerializeField]
    private string _tagName;

    public string tagName //�����ŃC���X�y�N�^�[�ォ��^�O�l�[����ݒ肷�邱��
    {
        get { return _tagName; }
        set
        {
            _tagName = value;
            gameObject.tag = _tagName;
        }
    }
    //�C���X�^���X������������.
    [SerializeField]
    public UnityEvent<GameObject> initialized;
    public void Initialized(GameObject InstanceObject, GameObject param)
    {
        InspectEvent(initialized, InstanceObject, param);
    }
    //���̃v���n�u����ǉ����ꂽ�C���X�^���X�̈��Ńh���b�O���ꂽ������,����Ă���΃C�x���g���X�i�[�����s
    [SerializeField]
    public UnityEvent<GameObject> onDropInstance;
    public void PrehubInstanceTrigger(GameObject dub, Canvas canvas, RectTransform dubRect) {

        GameObject[] instances = GameObject.FindGameObjectsWithTag(gameObject.tag);

        // ���ʂ����O�ɏo��
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
        // UnityEvent�̓����t�B�[���h���擾
        var field = typeof(UnityEventBase).GetField("m_Calls", BindingFlags.NonPublic | BindingFlags.Instance);
        var invocationList = field.GetValue(unityEvent);

        // ���X�i�[�̃��X�g���擾
        var persistentCalls = invocationList.GetType().GetField("m_PersistentCalls", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(invocationList);
        var calls = persistentCalls.GetType().GetField("m_Calls", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(persistentCalls) as System.Collections.IList;

        foreach (var call in calls)
        {
            // ���X�i�[�̃��\�b�h���ƃR���|�[�l���g�����擾
            var methodName = call.GetType().GetField("m_MethodName", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(call) as string;
            var target = call.GetType().GetField("m_Target", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(call) as MonoBehaviour;

            if (target != null)
            {
                Debug.Log("Method: " + methodName + ", Component: " + target.GetType().Name);

                // �R���|�[�l���g�̌^���擾
                var componentType = target.GetType();

                // �C���X�^���X�I�u�W�F�N�g����R���|�[�l���g���擾
                var component = instanceObject.GetComponent(componentType);

                if (component != null)
                {
                    // ���\�b�h���擾���ČĂяo��
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
