using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerIndicator : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 targetPosition = target.position;
        GetComponent<RectTransform>().position = targetPosition+new Vector2(1.5f, 1);
    }
}
