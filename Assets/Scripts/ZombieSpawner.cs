using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private int waveNumber = 0;

    private ObjectPooler pool;
    private GameController gameController;
    public Transform[] entrances;
    private int numberToSpawn;

    private void Awake()
    {
        pool = FindObjectOfType<ObjectPooler>();
        gameController = FindObjectOfType<GameController>();
    }

    public void StartSpawning(int _numberTopSpawn)
    {
        numberToSpawn = _numberTopSpawn;
        StartCoroutine(IStartSpawning());
    }

    IEnumerator IStartSpawning()
    {
        string[] enemies = { "BoyZombie", "GirlZombie" };

        while (numberToSpawn > 0)
        {
            Vector2 legalPos = GetLegalSpawnPoint();
            GameObject obj = pool.SpawnFromPool(enemies[Random.Range(0, enemies.Length)], legalPos, Quaternion.identity);
            numberToSpawn--;
            yield return new WaitForSeconds(Random.Range(1f,2f));
        }
    }

    Vector2 GetLegalSpawnPoint()
    {
        float halfMapSafeWidth = gameController.HalfMapWidth();
        float halfMapSafeHeight = gameController.HalfMapHeight();

        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        int chosenDirection = Random.Range(0, directions.Length);
        Vector2 directionToSpawn = Vector2.zero;
        switch (chosenDirection)
        {
            case 0:
                directionToSpawn = new Vector2(Random.Range(-halfMapSafeWidth, halfMapSafeWidth), halfMapSafeHeight + 2f);
                break;
            case 1:
                directionToSpawn = new Vector2(halfMapSafeWidth + 2f, Random.Range(-halfMapSafeHeight, halfMapSafeHeight));
                break;
            case 2:
                directionToSpawn = new Vector2(Random.Range(-halfMapSafeWidth, halfMapSafeWidth), -halfMapSafeHeight + 2f);
                break;
            case 3:
                directionToSpawn = new Vector2(-halfMapSafeWidth + 2f, Random.Range(-halfMapSafeHeight, halfMapSafeHeight));
                break;
            default:
                Debug.LogWarning("DIRECTION ERROR");
                break;
        }

        return directionToSpawn;
    }
}
