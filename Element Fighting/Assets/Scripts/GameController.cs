using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public StepSpawnerScript stepSpawner;
    public ElementSpawnerScript elementSpawner;
    public GameObject player1;
    public GameObject player2;

    // Dictionary to track relationships between steps and their spawned elements
    private Dictionary<GameObject, GameObject> stepElementPairs = new Dictionary<GameObject, GameObject>();

    // Offset to position the element above the step
    private float offset = 0.5f; 


    void Awake()
    {
        Debug.Log("GameController - Awake");
    }

    void OnEnable()
    {
        Debug.Log("GameController - OnEnable");
    }

    private void HandleStepSpawned(GameObject step)
    {
        // Get the currently assigned player elements
        List<string> playerElements = new List<string>
        {
            player1.GetComponent<PlayerElementScript>().playerElement,
            player2.GetComponent<PlayerElementScript>().playerElement
        };

        // Ask the ElementSpawnerScript to try adding an element to this step
        GameObject spawnedElement = elementSpawner.TrySpawnElementOnStep(step, playerElements);

        // If an element was spawned, register the relationship
        if (spawnedElement != null)
        {
            RegisterStepElementPair(step, spawnedElement);
        }
    }

    // Update the position of all elements based on their associated steps
    private void UpdateElementPositions()
    {
        List<GameObject> stepsToRemove = new List<GameObject>();

        foreach (var pair in stepElementPairs)
        {
            GameObject step = pair.Key;
            GameObject element = pair.Value;

            if (step == null) // if the step was destroyed or is null
            {
                Destroy(element); // destroy the associated element
                stepsToRemove.Add(step); // mark this step for removal from the dictionary
                continue;
            }

            // Update the element's position based on the step's position
            element.transform.position = step.transform.position + Vector3.up * offset;
        }

        // Remove any steps marked for removal from the dictionary
        foreach (var step in stepsToRemove)
        {
            stepElementPairs.Remove(step);
        }
    }

    public void RegisterStepElementPair(GameObject step, GameObject element)
    {
        stepElementPairs.Add(step, element);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameController - Start");
        stepSpawner.OnStepSpawned += HandleStepSpawned;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateElementPositions();
    }
}
