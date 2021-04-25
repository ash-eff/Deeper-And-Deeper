using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Jimmy : PlayerCharacter
{
    [SerializeField] private GameObject progressBarObj;
    [SerializeField] private GameObject leftMouseIndicator;
    [SerializeField] private GameObject rightMouseIndicator;
    [SerializeField] private Image progressBar;
    [SerializeField] private float actionLength;
    [SerializeField] private bool canFillHole;

    public Plot currentPlot;
    private static readonly int IsDigging = Animator.StringToHash("IsDigging");
    private static readonly int IsFilling = Animator.StringToHash("IsFilling");


    public override void ActionOne()
    {
        if (gameController.canTakeACtion)
        {
            if (isSelected && !isBusy && currentPlot != null)
            {
                if (currentPlot.canBeDug)
                {
                    StartCoroutine(DigAHole());
                }
            }
        }
    }
    
    public override void ActionTwo()
    {
        if (gameController.canTakeACtion)
        {
            if (isSelected && !isBusy && currentPlot != null)
            {
                if (currentPlot.canBeFilled && canFillHole)
                {
                    StartCoroutine(FillAHole());
                }
            }
        }
    }

    IEnumerator DigAHole()
    {
        leftMouseIndicator.SetActive(false);
        rightMouseIndicator.SetActive(false);
        currentPlot.DigHole();
        anim.SetBool(IsDigging, true);
        isBusy = true;
        progressBar.fillAmount = 0;
        progressBarObj.gameObject.SetActive(true);
        while (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount += Time.deltaTime / actionLength;
            yield return null;
        }

        isBusy = false;
        progressBarObj.gameObject.SetActive(false);
        anim.SetBool(IsDigging, false);
        currentPlot.HoleHasBeenDug();
    }
    
    IEnumerator FillAHole()
    {
        leftMouseIndicator.SetActive(false);
        rightMouseIndicator.SetActive(false);
        currentPlot.FillHole();
        anim.SetBool(IsFilling, true);
        isBusy = true;
        progressBar.fillAmount = 0;
        progressBarObj.gameObject.SetActive(true);
        while (progressBar.fillAmount < 1)
        {
            progressBar.fillAmount += Time.deltaTime / actionLength;
            yield return null;
        }

        isBusy = false; 
        progressBarObj.gameObject.SetActive(false);
        currentPlot.HoleHasBeenFilled();
        currentPlot = null;
        anim.SetBool(IsFilling, false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBusy)
        {
            leftMouseIndicator.SetActive(false);
            rightMouseIndicator.SetActive(false);
            return;
        }

        if (gameController.canTakeACtion)
        {
            if (other.CompareTag("Plot"))
            {
                currentPlot = other.gameObject.GetComponent<Plot>();
            
                if (currentPlot.canBeDug)
                {
                    leftMouseIndicator.SetActive(true);
                }

                if (currentPlot.hasBody)
                {
                    canFillHole = true;
                    rightMouseIndicator.SetActive(true);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isBusy)
        {
            rightMouseIndicator.SetActive(false);
            leftMouseIndicator.SetActive(false);
            return;
        }
        
        if (other.CompareTag("Plot"))
        {
            if (currentPlot != null)
            {
                currentPlot = null;
            }

            canFillHole = false;
            leftMouseIndicator.SetActive(false);
            rightMouseIndicator.SetActive(false);
        }
    }
}
