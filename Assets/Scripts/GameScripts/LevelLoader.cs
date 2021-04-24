using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float sceneTransitionTime;
    private static readonly int Start = Animator.StringToHash("Start");

    public void LoadNextLevel()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (levelIndex > SceneManager.sceneCountInBuildSettings - 1)
            levelIndex = 0;

        StartCoroutine(LoadLevel(levelIndex));
    }

    public void RestartGame()
    {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger(Start);

        yield return new WaitForSeconds(sceneTransitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
