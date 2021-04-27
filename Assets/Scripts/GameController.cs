using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public bool gameOver = false;
    private int waveNumber = 0;
    public int waveWinNumber = 2;
    public GameObject gameWonPanel;
    public GameObject wavePanel;
    public TextMeshProUGUI waveInd;
    
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

    public void GameOver()
    {
        gameOver = true;
        LevelLoader levelLoader = FindObjectOfType<LevelLoader>();
        levelLoader.LoadNextLevel();
    }

    IEnumerator AfterWave()
    {
        canTakeACtion = false;
        waveInd.text = "";
        wavePanel.SetActive(true);
        waveInd.text = "WAVE COMPLETE";
        yield return new WaitForSeconds(2f);

        if (waveNumber != waveWinNumber)
        {
            waveInd.text = "";
            waveInd.text = "GET READY";

            StartCoroutine(BetweenWavecounter());
        }
        else
        {
            GameWon();
        }
        
        wavePanel.SetActive(false);
    }

    IEnumerator BetweenWavecounter()
    {
        wavePanel.SetActive(true);
        waveNumber++;
        yield return new WaitForSeconds(2f);
        waveInd.text = "";
        waveInd.text = "wAVE " + waveNumber;
        yield return new WaitForSeconds(2f);
        waveInd.text = "";
        waveInd.text = "3";
        yield return new WaitForSeconds(1f);
        waveInd.text = "";
        waveInd.text = "2";
        yield return new WaitForSeconds(1f);
        waveInd.text = "";
        waveInd.text = "1";
        yield return new WaitForSeconds(1f);
        waveInd.text = "";
        waveInd.text = "GO";
        canTakeACtion = true;
        yield return new WaitForSeconds(1f);
        wavePanel.SetActive(false);
        waveInd.text = "";
        SpawnNextWave();
    }

    private void SpawnNextWave()
    {
        currentZombiesSpawned = initialNumberToSpawn + Mathf.RoundToInt(waveNumber / 2f);
        numberOfZombiesStillAlive = currentZombiesSpawned;
        zombieSpawner.StartSpawning(currentZombiesSpawned);
    }

    private void GameWon()
    {
        Time.timeScale = 0;
        gameWonPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameWonPanel.SetActive(false);
        StartCoroutine(BetweenWavecounter());
    }
}

    
