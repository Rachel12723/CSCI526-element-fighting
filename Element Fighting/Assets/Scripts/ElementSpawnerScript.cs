using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawnerScript : MonoBehaviour
{
    public List<GameObject> allElementPrefabs; // Drag all your element prefabs here
    public float elementSpawnProbability = 0.1f;

    void Awake()
    {
        Debug.Log("ElementSpawnerScript - Awake");
    }

    void OnEnable()
    {
        Debug.Log("ElementSpawnerScript - OnEnable");
    }

    // This method will receive the elements currently assigned to the players
    public GameObject TrySpawnElementOnStep(GameObject step, List<Element> playerElements)
    {
        // configure at inspector: drag all element prefab here
        //List<GameObject> availableElements = new List<GameObject>(allElementPrefabs);


        // Remove the elements that are currently assigned to players
        /* for (int i = availableElements.Count - 1; i >= 0; i--)
        {
            if (playerElements.Contains(availableElements[i].))
            {
                availableElements.RemoveAt(i);
            }
        } */


        List<Element> availableElements = new List<Element>();
        foreach(Element element in Element.GetValues(typeof(Element)))
        {
            if (!playerElements.Contains(element))
            {
                availableElements.Add(element);
            }
        }

        GameObject spawnedElement = null;

        // From the remaining elements, pick one at random and spawn it
        if (UnityEngine.Random.value < elementSpawnProbability && availableElements.Count > 0)
        {
            // Not useful rn, implemented for potentially adding more element, but plz dont do it!!!
            int randomIndex = UnityEngine.Random.Range(0, availableElements.Count);
            GameObject elementToSpawn = allElementPrefabs[(int)availableElements[randomIndex]];

            float stepColliderHeight = step.GetComponent<BoxCollider2D>().size.y;
            float elementColliderRadius = elementToSpawn.GetComponent<CircleCollider2D>().radius;

            Vector3 spawnPosition = step.transform.position + new Vector3(0, stepColliderHeight / 2 + elementColliderRadius, 0);
            spawnedElement = Instantiate(elementToSpawn, spawnPosition, Quaternion.identity);
        }

        return spawnedElement;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ElementSpawnerScript - Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}