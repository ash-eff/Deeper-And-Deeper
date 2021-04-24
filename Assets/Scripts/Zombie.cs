using System;
using System.Collections;
using System.Collections.Generic;
using Ash.MyUtils;
using UnityEngine;
using UnityEngine.Timeline;

public class Zombie : MonoBehaviour
{
    public bool isDead = false;
    public Color aliveSprite ,deadSprite;
    public SpriteRenderer spr;
    public Rigidbody2D rb2d;
    public int health = 3;
    public float speed = 3f;
    public PlayerController playerController;
    public GameObject directionIndicator;

    private void Awake()
    {
        spr.color = aliveSprite;
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (isDead)
        {
            directionIndicator.SetActive(false);
            return;
        }

        var currentTarget = playerController.busyCharacter.transform.position;
        var direction = currentTarget - transform.position;
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
        health--;
        if (health <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
        spr.color = deadSprite;
    }

    private void Attack()
    {
        
    }
}
