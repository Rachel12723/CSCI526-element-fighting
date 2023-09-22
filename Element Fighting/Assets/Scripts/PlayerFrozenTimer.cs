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
        Vector2 targetPosition = target.position;

        GetComponent<RectTransform>().position = targetPosition + new Vector2(0, 0.7f);
        if (freezeLeft >= 0)
        {
            freezeLeft -= Time.deltaTime;
            GetComponent<Slider>().value = freezeLeft;
        }
    }
}
