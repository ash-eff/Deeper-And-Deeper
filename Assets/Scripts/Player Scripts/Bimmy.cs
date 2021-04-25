using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bimmy : PlayerCharacter
{
    public bool isHoldingBody;
    public Zombie zombie;
    public Plot currentPlot;
    public bool canPlaceBody;
    public Transform carryPosition;
    public GameObject rightMouseIndicator;
    public bool isShooting = false;
    public float rateOfFire;
    public float lastShot = 0;
    public GameObject bulletPrefab;
    public Transform muzzlePosition;
    private static readonly int IsCarrying = Animator.StringToHash("IsCarrying");

    public override void Update()
    {
        base.Update();
        if (isShooting)
        {
            FireWeapon(angleToCursor);
        }
    }
    
    public override void ActionOne()
    {
        if (isSelected && !isHoldingBody)
        {
            isShooting = true;
        }
    }

    public override void ActionOneCancelled()
    {
        isShooting = false;
    }

    public override void ActionTwo()
    {
        if (gameController.canTakeACtion)
        {
            if (isSelected)
            {
                if (isHoldingBody)
                {
                    if (canPlaceBody)
                    {
                        isHoldingBody = false;
                        anim.SetBool(IsCarrying, false);
                        zombie.transform.position = currentPlot.transform.position;
                        zombie.transform.parent = null;
                        zombie.Deactivate();
                        currentPlot.PlaceBodyInHole(zombie);
                        zombie = null;
                    }
                    else
                    {
                        isHoldingBody = false;
                        anim.SetBool(IsCarrying, false);
                        zombie.transform.position = transform.position;
                        zombie.transform.parent = null;
                    }
                }
                else
                {
                    if (zombie != null && zombie.isDead)
                    {
                        rightMouseIndicator.SetActive(false);
                        zombie.transform.position = carryPosition.position;
                        zombie.transform.parent = carryPosition;
                        isHoldingBody = true;
                        anim.SetBool(IsCarrying, true);
                    }
                }
            }
        }
        
    }
    
    private void FireWeapon(float rot)
    {
        if (Time.time > rateOfFire + lastShot && !isHoldingBody)
        {
            //cam.CameraShake();
            GameObject obj = Instantiate(bulletPrefab, muzzlePosition.position, Quaternion.Euler(0,0, rot));
            //obj.GetComponent<Bullet>().rot = rot;
            lastShot = Time.time;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isHoldingBody)
        {
            if (other.CompareTag("Plot"))
            {
                currentPlot = other.gameObject.GetComponent<Plot>();
                if (!currentPlot.hasBody && currentPlot.hasBeenDug)
                {
                    canPlaceBody = true;
                    rightMouseIndicator.SetActive(true);
                }
            }
        }
        else if (other.CompareTag("Zombie"))
        {
            zombie = other.gameObject.GetComponent<Zombie>();
            if(zombie.isDead)
                rightMouseIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plot"))
        {
            canPlaceBody = false;
            rightMouseIndicator.SetActive(false);
        }
        
        if (other.CompareTag("Zombie"))
        {
            if (!isHoldingBody)
            {
                zombie = null;
                rightMouseIndicator.SetActive(false);
            }
                
        }
    }
}
