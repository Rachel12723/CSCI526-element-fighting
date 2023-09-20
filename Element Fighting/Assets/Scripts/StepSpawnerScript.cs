using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSpawnerScript : MonoBehaviour
{
    public GameObject stepPrefab; // Drag step prefab here in the inspector
    public float spawnRate = 2f; // Time interval between spawns
    public float stepSpawnWidth; // Width in which steps can spawn
    public float stepSpeed = 2f; // Speed at which steps move up
    public event Action<GameObject> OnStepSpawned;

    private float nextSpawnTime;
    private float stepHalfWidth; // Half width of the step

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the screen width in world units
        Camera mainCamera = Camera.main;
        stepSpawnWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;

        // Calculate half width of the step so that step won't appear cutoff at edge
        stepHalfWidth = stepPrefab.GetComponent<SpriteRenderer>().bounds.size.x / 2;

        // Initialize first steps
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
        // - stepHalfWidth so that step won't appear cutoff at edge
        float randomX = UnityEngine.Random.Range(-stepSpawnWidth / 2 + stepHalfWidth, stepSpawnWidth / 2 - stepHalfWidth);
        Vector2 spawnPosition = new Vector2(randomX, transform.position.y);
        InstantiateStepAtPosition(spawnPosition);
    }

    private void InstantiateStepAtPosition(Vector2 position)
    {
        GameObject step = Instantiate(stepPrefab, position, Quaternion.identity);

        // Assign the speed to the step so it starts moving up
        step.GetComponent<Rigidbody2D>().velocity = new Vector2(0, stepSpeed);

        // Notify that a step was spawned
        OnStepSpawned?.Invoke(step);
    }
}
