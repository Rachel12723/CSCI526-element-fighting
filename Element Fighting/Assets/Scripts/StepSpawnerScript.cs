using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSpawnerScript : MonoBehaviour
{
    public GameObject stepPrefab; // Drag step prefab here in the inspector
    public float spawnRate = 2f; // Time interval between spawns
    public float stepSpawnWidth; // Width in which steps can spawn
    public float stepSpeed = 2f; // Speed at which steps move up

    private float nextSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the screen width in world units
        Camera mainCamera = Camera.main;
        stepSpawnWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
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
        GameObject step = Instantiate(stepPrefab, spawnPosition, Quaternion.identity);

        // Assign the speed to the step so it starts moving up
        step.GetComponent<Rigidbody2D>().velocity = new Vector2(0, stepSpeed);
    }
}
