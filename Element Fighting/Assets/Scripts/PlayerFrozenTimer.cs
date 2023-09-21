using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrozenTimer : MonoBehaviour
{
    public Transform target;
    public float freezeLeft = 0;


    void Start()
    {

    }

    void Update()
    {
        // 获取目标物体的世界坐标
        Vector2 targetPosition = target.position;

        // 将目标物体的世界坐标转换为屏幕坐标
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(targetPosition);

        GetComponent<RectTransform>().position = screenPosition + new Vector2(0, 35);
        if (freezeLeft > 0)
        {
            freezeLeft -= Time.deltaTime;
            GetComponent<Slider>().value = freezeLeft;
        }
    }
}
