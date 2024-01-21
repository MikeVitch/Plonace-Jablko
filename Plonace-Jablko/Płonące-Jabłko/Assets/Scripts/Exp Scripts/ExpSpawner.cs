using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExpSpawner : MonoBehaviour
{
    public GameObject expCollectiblePrefab; 
    public int numberOfExpToSpawn = 10;

    private bool isDestroyed = false;

    private void OnDestroy()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            SpawnExpCollectibles(transform.position);
        }
    }

    public void SpawnExpCollectibles(Vector3 spawnPosition)
    {
        if (expCollectiblePrefab == null)
        {
            return;
        }

        for (int i = 0; i < numberOfExpToSpawn; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
            Vector3 finalSpawnPosition = spawnPosition + randomOffset;

            if (expCollectiblePrefab != null)
            {
                GameObject exp = Instantiate(expCollectiblePrefab, finalSpawnPosition, Quaternion.identity);
            }
        }
    }
}
