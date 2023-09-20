using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSpawnerScript : MonoBehaviour
{
    public GameObject stepPrefab; // Drag step prefab here in the inspector
    public float spawnRate = 1f; // Time interval between spawns
    public float stepSpawnWidth; // Width in which steps can spawn
    public float stepSpeed = 2f; // Speed at which steps move up
    //public Vector2[] initialSpawnPositions = { new Vector2(-2, 0), new Vector2(2, 0) };// Array to store initial spawn positions
    private float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the screen width in world units
        Camera mainCamera = Camera.main;
        stepSpawnWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;

        // Instantiate steps at the initial positions
        //foreach (Vector2 position in initialSpawnPositions)
        //{
        //    InstantiateStepAtPosition(position);
        //}
        InstantiateStepAtPosition(new Vector2(-2, 0));
        InstantiateStepAtPosition(new Vector2(2, 0));
    }

    private void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnStep();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void SpawnStep()
    {
        float randomX = Random.Range(-stepSpawnWidth / 2, stepSpawnWidth / 2);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);
        InstantiateStepAtPosition(spawnPosition);
    }

    private void InstantiateStepAtPosition(Vector2 position)
    {
        GameObject step = Instantiate(stepPrefab, position, Quaternion.identity);

        // Assign the speed to the step so it starts moving up
        step.GetComponent<Rigidbody2D>().velocity = new Vector2(0, stepSpeed);
    }
}
