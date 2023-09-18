using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBehaviorScript : MonoBehaviour
{
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        // Check if the step has gone off the top of the screen
        if (transform.position.y > mainCamera.orthographicSize + transform.localScale.y / 2)
        {
            Destroy(gameObject);
        }
    }
}
