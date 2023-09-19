using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerIndicator : MonoBehaviour
{
    public Transform target;
    private TMP_Text textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = target.position;
        // ��ȡĿ���������������
        Vector2 targetPosition = target.position;

        // ��Ŀ���������������ת��Ϊ��Ļ����
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);

        // ����UI Text��λ��
        textMeshPro.rectTransform.position = screenPosition + new Vector2(80,40);
    }
}
