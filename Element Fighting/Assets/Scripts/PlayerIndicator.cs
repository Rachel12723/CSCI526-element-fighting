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
        // 获取目标物体的世界坐标
        Vector2 targetPosition = target.position;

        // 将目标物体的世界坐标转换为屏幕坐标
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);

        // 更新UI Text的位置
        textMeshPro.rectTransform.position = screenPosition + new Vector2(80,40);
    }
}
