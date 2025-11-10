using System;
using System.Diagnostics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScorpionAttack : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private Rigidbody2D rb;
    public float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        player = GameObject.FindWithTag("Player");

        //Fire the projectile at the player
        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2 (direction.x, direction.y).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Code to destroy projectile after time has passed
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    //Code for when the projectile hits something
    public void OnTriggerEnter2D(Collider2D collision)
    {
        String tag = collision.gameObject.tag;
        if (!tag.Equals("Enemy"))
        {
            Destroy(this.gameObject);
        }
        if (tag.Equals("Player"))
        {
            collision.GetComponent<PlayerTakeDamage>().takeDamage(1);
        }
    }
}
