using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mound : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    
    private void Awake()
    {
        StartCoroutine(ReturnToDigableSoil());
    }
    
    IEnumerator ReturnToDigableSoil()
    {
        var timer = transitionTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        
        
        Destroy(gameObject);
    }
}
