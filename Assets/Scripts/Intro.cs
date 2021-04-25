using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public AudioSource shovelOne;
    public AudioSource shovelTwo;
    public AudioSource gunCock;
    public AudioSource voices;
    public AudioSource baDing;


    
    public void PlayShovelOne()
    {
        shovelOne.Play();
    }
    
    public void PlayShovelTwo()
    {
        shovelTwo.Play();
    }
    
        
    public void PlayGunCock()
    {
        gunCock.Play();
    }

    public void PlayVoices()
    {
        voices.Play();
    }
    
    public void PlayBaDing()
    {
        baDing.Play();
    }
}
