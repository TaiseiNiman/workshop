using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class expainImage : MonoBehaviour
{

    //������
    public void utilize(string imageUrl)
    {
        // Image�R���|�[�l���g���擾
        Image imageComponent = this.gameObject.GetComponent<Image>();

        // Resources�t�H���_����摜��ǂݍ���
        Sprite newSprite = Resources.Load<Sprite>(imageUrl);

        // �摜��ݒ�
        imageComponent.sprite = newSprite;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
