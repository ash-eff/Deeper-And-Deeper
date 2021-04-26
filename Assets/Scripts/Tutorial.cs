using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject[] tutPics;
    public GameObject tutHolder;
    public int tutIndex = 0;

    public void OpenTutorial()
    {
        tutHolder.SetActive(true);
        tutPics[0].SetActive(true);
    }

    public void CloseTutorial()
    {
        foreach (GameObject obj in tutPics)
        {
            obj.SetActive(false);
        }

        tutHolder.SetActive(false);
    }

    public void CycleRight()
    {
        tutPics[tutIndex].SetActive(false);
        var tempIndex = tutIndex++;
        if (tutIndex > tutPics.Length - 1)
            tutIndex = 0;
        
        tutPics[tutIndex].SetActive(true);
    }

    public void CycleLeft()
    {
        tutPics[tutIndex].SetActive(false);
        var tempIndex = tutIndex--;
        if (tutIndex < 0)
            tutIndex = tutPics.Length - 1;
        
        tutPics[tutIndex].SetActive(true);
    }

    public void CheckIndex()
    {
        
    }
}
