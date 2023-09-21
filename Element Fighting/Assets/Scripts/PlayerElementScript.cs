using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerElementScript : MonoBehaviour
{
    public string playerElement;

    // This method allows external scripts (eg. ElementSpawnerScript) to change the player's element
    public void ChangeElement(string newElement)
    {
        playerElement = newElement;

        // - Changing the player's appearance based on the new element
        // - Modifying player abilities or stats based on the element
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