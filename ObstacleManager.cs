using UnityEngine;

public class ObsManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 1.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefabs.Length == 0 || spawnPoints.Length == 0)
            return;

        int obsIndex = Random.Range(0, obstaclePrefabs.Length);
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(obstaclePrefabs[obsIndex], 
                    spawnPoints[spawnIndex].position, 
                    Quaternion.identity);
    }
}
