using UnityEngine;
using System.Collections;

public class UniversalSpawner : MonoBehaviour
{
    [Header("Biler der kan spawn")]
    public GameObject[] carPrefabs;

    [Header("Spawnpoints")]
    public Transform[] spawnPoints;

    [Header("Spawn interval")]
    public float minInterval = 1f;
    public float maxInterval = 3f;

    [Header("Speed (fast variabel)")]
    public int speed = 5; // Sæt hastigheden i Inspector

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnCar();
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnCar()
    {
        if (carPrefabs == null || carPrefabs.Length == 0 || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Ingen prefabs eller spawnpoints sat!");
            return;
        }

        // Vælg tilfældig prefab
        GameObject chosenPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

        // Vælg tilfældig spawnpoint
        Transform chosenSpawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiér bilen
        GameObject newCar = Instantiate(chosenPrefab, chosenSpawn.position, chosenSpawn.rotation);

        // Sæt parent til spawnpointens parent (valgfrit)
        newCar.transform.SetParent(chosenSpawn.parent, true);

        // Sæt skala til prefabens skala
        newCar.transform.localScale = chosenPrefab.transform.localScale;

        // Sæt speed på CarMovement scriptet
        CarMovement carMovement = newCar.GetComponent<CarMovement>();
        if (carMovement != null)
        {
            carMovement.speed = speed; // Fast speed fra Inspector
        }
    }
}
