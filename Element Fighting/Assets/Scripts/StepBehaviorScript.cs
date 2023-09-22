using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBehaviorScript : MonoBehaviour
{
    private Camera mainCamera;

    void Awake()
    {
        Debug.Log("StepBehavior - Awake");
    }

    void OnEnable()
    {
        Debug.Log("StepBehavior - OnEnable");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("StepBehavior - Start");
        mainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        // Check if the step has gone off the top of the screen
        if (transform.position.y > mainCamera.orthographicSize + transform.localScale.y)
        {
            Destroy(gameObject);
        }
    }
}
