using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Plot : MonoBehaviour
{
    [NotNull] public Sprite holeSprite, growSprite1, growSprite2, growSprite3, baseSprite;
    public bool hasBody = false;
    public bool hasBeenDug = false;
    public bool canBeDug = true;
    public bool isBeingFilled = false;
    public bool canBeFilled = false;
    public float resetTime = 6f;
    //public GameObject digIndicator;
    //public GameObject fillIndicator;
    public GameObject riseIndicator;
    public Image riseFillBar;
    public float riseTimer;
    public Zombie currentZombie;
    public BoxCollider2D collider2D;
    public GameObject jimmySelect, bimmySelect, bothSelect;
    public bool jimmyTouch, bimmyTouch;
    private GameController gameController;
    
    public SpriteRenderer spr;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        spr.sprite = baseSprite;
        //digIndicator.SetActive(true);
        //fillIndicator.SetActive(false);
    }

    private void Update()
    {
        if (gameController.canTakeACtion)
        {
            if (jimmyTouch && bimmyTouch)
            {
                bothSelect.SetActive(true);
            }
            else if (jimmyTouch)
            {
                bothSelect.SetActive(false);
                bimmySelect.SetActive(false);
                jimmySelect.SetActive(true);
            }
            else if (bimmyTouch)
            {
                bothSelect.SetActive(false);
                bimmySelect.SetActive(true);
                jimmySelect.SetActive(false);
            }
            else
            {
                bothSelect.SetActive(false);
                bimmySelect.SetActive(false);
                jimmySelect.SetActive(false);
            }
        }
        else
        {
            bothSelect.SetActive(false);
            bimmySelect.SetActive(false);
            jimmySelect.SetActive(false);
        }
    }

    public void DigHole()
    {
        
        //digIndicator.SetActive(false);
        canBeDug = false;
    }

    public void HoleHasBeenDug()
    {
        collider2D.enabled = false;
        collider2D.enabled = true;
        canBeFilled = true;
        hasBeenDug = true;
        spr.sprite = holeSprite;
    }

    public void FillHole()
    {
        canBeFilled = false;
        hasBeenDug = false;
        isBeingFilled = true;
        //fillIndicator.SetActive(false);
    }

    public void HoleHasBeenFilled()
    {
        collider2D.enabled = false;
        collider2D.enabled = true;
        isBeingFilled = false;
        spr.sprite = growSprite1;
        StartCoroutine(ResetPlot());
    }

    public void PlaceBodyInHole(Zombie zombie)
    {
        collider2D.enabled = false;
        currentZombie = zombie;
        //currentZombie.gameObject.SetActive(false);
        hasBody = true;
        collider2D.enabled = true;

        StartCoroutine(ZombieRise());

        IEnumerator ZombieRise()
        {
            riseIndicator.SetActive(true);
            riseFillBar.fillAmount = 0;
            riseFillBar.gameObject.SetActive(true);
            
            while (!isBeingFilled)
            {
                riseFillBar.fillAmount += Time.deltaTime / riseTimer;
                if (riseFillBar.fillAmount >= 1f)
                {
                    RaiseTheDead();
                    break;
                }

                yield return null;
            }
            
            riseIndicator.SetActive(false);
        }
    }

    public void RaiseTheDead()
    {
        currentZombie.Reanimate();
        hasBody = false;
        currentZombie = null;
    }
    
    IEnumerator ResetPlot()
    {
        collider2D.enabled = false;
        var waitTime = resetTime / 3;
        yield return new WaitForSeconds(waitTime);
        spr.sprite = growSprite2;
        yield return new WaitForSeconds(waitTime);
        spr.sprite = growSprite3;
        yield return new WaitForSeconds(waitTime);
        spr.sprite = baseSprite;

        hasBody = false;
        currentZombie.gameObject.SetActive(false);
        currentZombie = null;
        gameController.UpdateZombieCount();
        //digIndicator.SetActive(true);
        canBeDug = true;
        collider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jimmy"))
        {
            jimmyTouch = true;
        }

        if (other.CompareTag("Bimmy"))
        {
            bimmyTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jimmy"))
        {
            jimmyTouch = false;
        }

        if (other.CompareTag("Bimmy"))
        {
            bimmyTouch = false;
        }
    }
}
