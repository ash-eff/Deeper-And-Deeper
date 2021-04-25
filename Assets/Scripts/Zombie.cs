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
    private float currentSpeed;
    public PlayerController playerController;
    public GameObject directionIndicator;
    private Material matWhite;
    private Material matDefault;
    public Animator anim;
    private static readonly int IsDead = Animator.StringToHash("isDead");
    private Vector3 currentTarget;
    private Vector2 closestEntrance;
    private bool inGraveyard = false;
    private ZombieSpawner zombieSpawner;
    private bool canAttack = true;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material; 
        matDefault = spr.material;
        zombieSpawner = FindObjectOfType<ZombieSpawner>();
        currentSpeed = speed * 3;

    }

    private void Start()
    {
        closestEntrance = FindClosestEntrance();
    }

    private void Update()
    {
        if (isDead)
        {
            directionIndicator.SetActive(false);
            return;
        }

        if (!inGraveyard)
            currentTarget = closestEntrance;
        else
            currentTarget = playerController.busyCharacter.transform.position;
        
        var direction = (currentTarget - transform.position).normalized;
        
        Debug.DrawLine(transform.position, currentTarget, Color.red);
        
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
        Vector2 newPosition = Vector2.MoveTowards(transform.position, currentTarget, Time.deltaTime * currentSpeed);
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
            anim.SetBool(IsAttacking, true);
            PlayerCharacter player = other.gameObject.GetComponentInParent<PlayerCharacter>();
            StartCoroutine(Attack(player));
        }

        if (other.CompareTag("Enterance"))
        {
            currentSpeed = speed;
            inGraveyard = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canAttack = false;
        }
    }

    private Vector2 FindClosestEntrance()
    {
        var closestDist = 10000f;
        Vector2 closestPos = Vector2.zero;
        
        foreach (Transform t in zombieSpawner.entrances)
        {
            var distance = (t.position - transform.position).magnitude;
            if (distance < closestDist)
            {
                closestDist = distance;
                closestPos = t.position;
            }
        }

        return closestPos;
    }

    public void Deactivate()
    {
        spr.enabled = false;
    }

    public void Reanimate()
    {
        spr.enabled = true;
        health = 3;
        isDead = false;
        anim.SetBool(IsDead, false);
    }

    private void TakeDamage()
    {
        if (!inGraveyard) return;
        
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

    IEnumerator Attack(PlayerCharacter player)
    {
        while (canAttack)
        {
            // do damage
            player.TakeDamage();
            // wait
            yield return new WaitForSeconds(1.5f);
        }

        canAttack = true;
        anim.SetBool(IsAttacking, false);
    }

    private void SwapMaterialToDefault()
    {
        spr.material = matDefault;
    }
}
