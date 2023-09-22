using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public StepSpawnerScript stepSpawner;
    public ElementSpawnerScript elementSpawner;
    public GameObject player1;
    public GameObject player2;
    public GameObject playerText;
	

    // Dictionary to track relationships between steps and their spawned elements
    private Dictionary<GameObject, GameObject> stepElementPairs = new Dictionary<GameObject, GameObject>();
	// Store elements' positions in the screen view
	private List<Transform> elementPositions = new List<Transform>();

    // Offset to position the element above the step
    private float offset = 0.7f; 
	private float disappearDistance = 0.9f;

    public GameObject menu;
    private bool gameOver = false;
    private bool gameStart = false;
    private KeyCode startKey = KeyCode.Return;

    private void allStart()
    {
        gameStart = true;
        gameOver = false;

        // Activate Players & Init Players
        PlayerController playerController;
        player1.SetActive(true);
        player1.GetComponent<PlayerController>().init();
        player1.transform.position = new Vector3(-2.5f, 3, 0);
        int n1 = UnityEngine.Random.Range(0, 3);
        playerController = player1.GetComponent<PlayerController>();
        if (n1 == 0)
        {
            playerController.changeElement(Element.Fire);
        }
        else if (n1 == 1)
        {
            playerController.changeElement(Element.Wood);
        }
        else if (n1 == 2)
        {
            playerController.changeElement(Element.Water);
        }

        player2.SetActive(true);
        player2.GetComponent<PlayerController>().init();
        player2.transform.position = new Vector3(2.5f, 3, 0);
        int n2 = UnityEngine.Random.Range(0, 3);
        while (n2 == n1)
        {
            n2 = UnityEngine.Random.Range(0, 3);
        }
        if (n2 == 0)
        {
            player2.GetComponent<PlayerController>().changeElement(Element.Fire);
        }
        else if (n2 == 1)
        {
            player2.GetComponent<PlayerController>().changeElement(Element.Wood);
        }
        else if (n2 == 2)
        {
            player2.GetComponent<PlayerController>().changeElement(Element.Water);
        }

        // Activate PlayerText
        playerText.SetActive(true);

        // Destroy the platforms and elements
        GameObject[] platform = GameObject.FindGameObjectsWithTag("Platform");
        foreach(GameObject p in platform)
        {
            Destroy(p);
        }
        GameObject[] element = GameObject.FindGameObjectsWithTag("Element");
        foreach (GameObject e in element)
        {
            Destroy(e);
        }
        stepElementPairs.Clear();

        // Init Platforms
        stepSpawner.InstantiateStepAtPosition(new Vector2(-2.5f, 0));
        stepSpawner.InstantiateStepAtPosition(new Vector2(2.5f, 0));

    }

    private void isOver()
    {
        if (player1.transform.position.y>=5.6|| player1.transform.position.y <= -5.6 || player2.transform.position.y >= 5.6 || player2.transform.position.y <= -5.6)
        {
            // Deactivate Players & Init Players
            player1.SetActive(false);
            player1.transform.position = new Vector3(-2.5f, 10, 0);
            player2.SetActive(false);
            player2.transform.position = new Vector3(2.5f, 10, 0);
            // Deactivate PlayerText
            playerText.SetActive(false);

            // Active Menu and present result
            if (player1.transform.position.y >= 5.6 || player1.transform.position.y <= -5.6)
            {
                menu.transform.GetChild(1).GetComponent<TMP_Text>().text = "Game Over!\nPlayer2 Wins!";
            }
            else
            {
                menu.transform.GetChild(1).GetComponent<TMP_Text>().text = "Game Over!\nPlayer1 Wins!";
            }
            menu.SetActive(true);

            gameOver = true;
            gameStart = false;
        }

    }

    private void HandleStepSpawned(GameObject step)
    {
        // Get the currently assigned player elements
        List<Element> playerElements = new List<Element>
        {
            player1.GetComponent<PlayerController>().element,
            player2.GetComponent<PlayerController>().element
        };

        if (stepElementPairs.Count == 0)
        {
            // Ask the ElementSpawnerScript to try adding an element to this step
            GameObject spawnedElement = elementSpawner.TrySpawnElementOnStep(step, playerElements);

            // If an element was spawned, register the relationship
            if (spawnedElement != null)
            {
                RegisterStepElementPair(step, spawnedElement);
            }
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
			// elementPositions,add(element.transform);
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
        menu.transform.GetChild(1).GetComponent<TMP_Text>().text = "Element Fighting";
        menu.SetActive(true);
        stepSpawner.OnStepSpawned += HandleStepSpawned;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart)
        {
            if (Input.GetKeyDown(startKey))
            {
                menu.SetActive(false);
                allStart();
            }
        }
        UpdateElementPositions();
		foreach (var pair in stepElementPairs)
        {
			GameObject step = pair.Key;
            GameObject element = pair.Value;
			
			// calculate the distance between players and each element
            float distance1 = Vector3.Distance(player1.transform.position, element.transform.position);
			float distance2 = Vector3.Distance(player2.transform.position, element.transform.position);
			
			if (distance1 < disappearDistance || distance2 < disappearDistance)
			{
				// destroy the one that is near to the player and update the stepElementPairs
                if (distance1 < disappearDistance)
                {
                    player1.GetComponent<PlayerController>().changeElement(element.GetComponent<ElementScript>().elementType);
                }
                else if(distance2 < disappearDistance)
                {
                    player2.GetComponent<PlayerController>().changeElement(element.GetComponent<ElementScript>().elementType);
                }
				stepElementPairs.Remove(step);
				Destroy(element);
            }
		}
        if (gameStart && !gameOver)
        {
            isOver();
        }
    }
}
