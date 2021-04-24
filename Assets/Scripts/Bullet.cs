using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float speed;
    public float rot;

    private void Awake()
    {
        //var offset = Random.Range(-4, 4);
        //rot += offset;
        //transform.rotation = Quaternion.Euler(0,0, rot);
    }

    private void Start()
    {
        rb2d.velocity = transform.right * speed;
    }
}
