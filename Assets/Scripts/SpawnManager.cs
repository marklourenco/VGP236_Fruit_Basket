using System.Collections.Generic;
// using Unity.Android.Types;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance = null;
    public static SpawnManager Instance { get { return instance; } }

    private List<GameObject> spawnedPrefabs = new List<GameObject>();

    [SerializeField]
    private GameObject poopPrefab = null;

    [SerializeField]
    private GameObject fruitPrefab = null;
    [SerializeField]
    private GameObject fruit1Prefab = null;
    [SerializeField]
    private GameObject fruit2Prefab = null;

    private float spawnInterval = 2f;
    private float timeSinceLastShot = 0f;

    private float speedInterval = 5f;
    private float timeSinceLastSpeedUp = 0f;
    private float speed = 1.5f;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Update()
    {
        // If playing, spawn stuff
        if(GameManager.Instance.GetGameState() == GameState.Play)
        {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= spawnInterval)
            {
                Spawn();
                timeSinceLastShot = 0f;
            }

            timeSinceLastSpeedUp += Time.deltaTime;
            if (timeSinceLastSpeedUp >= speedInterval)
            {
                speed += 0.2f;
                timeSinceLastSpeedUp = 0f;
                if(spawnInterval > 0.6f)
                {
                    spawnInterval -= 0.2f;
                }
            }
        }
    }

    void Spawn()
    {
        int random = Random.Range(0, 3);
        Vector3 randomPos = new Vector3(Random.Range(-2.5f, 2.5f), 5.6f, 0.0f);

        if (random == 0)
        {
            GameObject spawnedObject = Instantiate(poopPrefab, randomPos, Quaternion.identity);
            spawnedPrefabs.Add(spawnedObject);
        }
        if (random != 0)
        {
            random = Random.Range(0, 3);

            Debug.Log(random);

            if (random == 0)
            {
                GameObject spawnedObject = Instantiate(fruitPrefab, randomPos, Quaternion.identity);
                spawnedPrefabs.Add(spawnedObject);
            }
            else if (random == 1)
            {
                GameObject spawnedObject = Instantiate(fruit1Prefab, randomPos, Quaternion.identity);
                spawnedPrefabs.Add(spawnedObject);
            }
            else if (random == 2)
            {
                GameObject spawnedObject = Instantiate(fruit2Prefab, randomPos, Quaternion.identity);
                spawnedPrefabs.Add(spawnedObject);
            }
        }
    }

    public void DeleteAllSpawnedObjects()
    {
        foreach (GameObject prefab in spawnedPrefabs)
        {
            Destroy(prefab);
        }
        spawnedPrefabs.Clear();
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void ResetSpeed()
    {
        speed = 1.5f;
        spawnInterval = 2.0f;
    }
}
