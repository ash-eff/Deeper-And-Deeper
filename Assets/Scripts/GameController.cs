using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ZombieSpawner zombieSpawner;
    [SerializeField] private float mapSizeX;
    [SerializeField] private float mapSizeY;

    public int initialNumberToSpawn;
    private int currentZombiesSpawned;
    private int numberOfZombiesStillAlive;
    public bool canTakeACtion = false;

    private int waveNumber = 0;

    [SerializeField] private MessageSystem messageSystem;

    private void Awake()
    {
        zombieSpawner = FindObjectOfType<ZombieSpawner>();
    }

    private void Start()
    {
        StartCoroutine(BetweenWavecounter());
    }

    public float HalfMapWidth()
    {
        return mapSizeX / 2;
    }

    public float HalfMapHeight()
    {
        return mapSizeY / 2;
    }
    
    public void UpdateZombieCount()
    {
        numberOfZombiesStillAlive--;

        if (numberOfZombiesStillAlive <= 0)
        {
            StartCoroutine(AfterWave());
        }
    }

    IEnumerator AfterWave()
    {
        canTakeACtion = false;
        yield return new WaitForSeconds(2f);
        messageSystem.StopMessage();
        messageSystem.DisplayMessage("WAVE COMPLETE" , 2);
        yield return new WaitForSeconds(2f);
        messageSystem.StopMessage();
        messageSystem.DisplayMessage("GET READY" , 2);

        StartCoroutine(BetweenWavecounter());
    }

    IEnumerator BetweenWavecounter()
    {
        waveNumber++;
        yield return new WaitForSeconds(2f);
        messageSystem.StopMessage();
        messageSystem.DisplayMessage("WAVE " + waveNumber, 2);
        yield return new WaitForSeconds(2f);
        messageSystem.StopMessage();
        messageSystem.DisplayMessage("THREE", 1);
        yield return new WaitForSeconds(1f);
        messageSystem.StopMessage();
        messageSystem.DisplayMessage("TWO", 1);
        yield return new WaitForSeconds(1f);
        messageSystem.StopMessage();
        messageSystem.DisplayMessage("ONE", 1);
        yield return new WaitForSeconds(1f);
        messageSystem.StopMessage();
        messageSystem.DisplayMessage("GO", 1);
        canTakeACtion = true;
        SpawnNextWave();
    }

    private void SpawnNextWave()
    {
        currentZombiesSpawned = initialNumberToSpawn + Mathf.RoundToInt(waveNumber / 2f);
        numberOfZombiesStillAlive = currentZombiesSpawned;
        zombieSpawner.StartSpawning(currentZombiesSpawned);
    }
}

    
