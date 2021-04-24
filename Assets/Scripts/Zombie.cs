using System;
using System.Collections;
using System.Collections.Generic;
using Ash.MyUtils;
using UnityEngine;
using UnityEngine.Timeline;

public class Zombie : MonoBehaviour
{
    public bool isDead = false;
    public SpriteRenderer spr;
    public Rigidbody2D rb2d;
    public int health = 3;
    public float speed = 3f;
    public PlayerController playerController;
    public GameObject directionIndicator;
    private Material matWhite;
    private Material matDefault;
    public Animator anim;
    private static readonly int IsDead = Animator.StringToHash("isDead");

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material; 
        matDefault = spr.material;
    }

    private void Update()
    {
        if (isDead)
        {
            directionIndicator.SetActive(false);
            return;
        }

        var currentTarget = playerController.busyCharacter.transform.position;
        var direction = (currentTarget - transform.position).normalized;
        
        if (direction.x < 0)
        {
            spr.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            spr.transform.localScale = new Vector3(1, 1, 1);
        }
        
        var rot = MyUtils.GetAngleFromVectorFloat(direction);
        directionIndicator.transform.rotation = Quaternion.Euler(0,0, rot);
        Vector2 newPosition = Vector2.MoveTowards(transform.position, currentTarget, Time.deltaTime * speed);
        rb2d.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead)
        {
            return;
        }
        
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            TakeDamage();
        }

        if (other.CompareTag("Player"))
        {
            Attack();
        }
    }

    private void TakeDamage()
    {
        spr.material = matWhite;
        Invoke("SwapMaterialToDefault", .1f);
        health--;
        if (health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        anim.SetBool(IsDead, true);
        isDead = true;
    }

    private void Attack()
    {
        
    }
    
    private void SwapMaterialToDefault()
    {
        spr.material = matDefault;
    }
}
