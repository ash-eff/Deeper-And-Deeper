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
    [SerializeField] private GameObject fillIndicator;
    [SerializeField] private GameObject noFillIndicator;
    [SerializeField] private Image progressBar;
    [SerializeField] private float actionLength;
    [SerializeField] private bool canFillHole;
    [SerializeField] private GameObject holePrefab;
    [SerializeField] private GameObject moundPrefab;

    private Hole currentHole;


    public override void ActionOne()
    {
        if (isSelected && !isBusy)
            StartCoroutine(DigAHole());
    }
    
    public override void ActionTwo()
    {
        if (isSelected && !isBusy && canFillHole)
        {
            StartCoroutine(FillAHole());
        }
    }

    IEnumerator DigAHole()
    {
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

        Instantiate(holePrefab, transform.position, quaternion.identity);
    }
    
    IEnumerator FillAHole()
    {
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

        var currentHolePos = currentHole.transform.position;
        Destroy(currentHole.gameObject);
        currentHole = null;
        Instantiate(moundPrefab, currentHolePos, quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isBusy)
        {
            fillIndicator.SetActive(false);
            noFillIndicator.SetActive(false);
            return;
        }
        
        if (other.CompareTag("Hole"))
        {
            currentHole = other.gameObject.GetComponent<Hole>();
            if (currentHole.hasBody)
            {
                canFillHole = true;
                fillIndicator.SetActive(true);
            }
        }
        
        if (other.CompareTag("Mound"))
        {
            noFillIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isBusy)
        {
            fillIndicator.SetActive(false);
            noFillIndicator.SetActive(false);
            return;
        }
        
        if (other.CompareTag("Hole"))
        {
            currentHole = null;
            canFillHole = false;
            fillIndicator.SetActive(false);
        }
        
        if (other.CompareTag("Mound"))
        {
            noFillIndicator.SetActive(false);
        }
    }
}
