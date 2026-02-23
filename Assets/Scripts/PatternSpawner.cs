using Unity.VisualScripting;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnDistanceAhad = 40f;
    public float laneOffset = 1.5f;

    // instead time using distance to trigger spawns
    private float nextSpawnZ;
    private float distanceBetweenRows = 8f; // Standart gap

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistanceAhad;
    }

    void Update()
    {
        if (player.position.z > nextSpawnZ - spawnDistanceAhad)
        {
            SpawnPattern();
        }
    }

    void SpawnPattern()
    {
        int patternType = Random.Range(0, 4);

        switch (patternType)
        {
            case 0:
                SpawnObstacle(Random.Range(0, 2), 0f); 
                nextSpawnZ += distanceBetweenRows;
                break;

            case 1:
                SpawnObstacle(0, 0f); // Left
                SpawnObstacle(1, distanceBetweenRows / 2f); // Right but closer
                nextSpawnZ += distanceBetweenRows * 2;
                break;

            case 2:
                // 3 obstacles in a row on the left
                SpawnObstacle(0, 0f); 
                SpawnObstacle(0, distanceBetweenRows);
                SpawnObstacle(0, distanceBetweenRows * 2);
                nextSpawnZ += distanceBetweenRows * 3;
                break;

            case 3:
                // ZİG ZAG, Left -> Right -> Left
                SpawnObstacle(0, 0f); // Left
                SpawnObstacle(1, distanceBetweenRows * 0.8f); // Slightly tighter gap
                SpawnObstacle(1, distanceBetweenRows * 1.6f);
                nextSpawnZ += distanceBetweenRows * 3;
                break;
        }
    }

    void SpawnObstacle(int laneIndex, float zOffset)
    {
        GameObject obj = ObjectPooler.Instance.GetPooledObject();

        if (obj != null)
        {
            float xPos = (laneIndex == 0) ? -laneOffset : laneOffset;

            Vector3 pos = new Vector3(xPos, 0.5f, nextSpawnZ + zOffset);

            obj.transform.position = pos;
            obj.transform.rotation = Quaternion.identity;
            obj.SetActive(true);
        }

        if (Random.value > 0.5f)
        {
            int coinLane = 1- laneIndex; // Opposite lane
            float coinX = (coinLane == 0) ? -laneOffset : laneOffset;

            Vector3 coinPos =new Vector3(coinX, 1.0f, nextSpawnZ + zOffset);
            Instantiate(coinPrefab, coinPos, Quaternion.Euler(90, 0, 0));
        }
    }
}
