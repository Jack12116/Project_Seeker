using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerFire : MonoBehaviour
{
    public float timer;
    public GameObject aimProjectile;
    private Animator animator;
    public float speed;
    public float recoil;
    private bool contact;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contact = false;
        animator = GetComponent<Animator>();
        aimProjectile = GameObject.Find("AimProjectile");
        //Set the rotation of the projectile
        transform.rotation = aimProjectile.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the gameobject and destroys when enough time passes
        if (!contact)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    //Code for when projectile hits something
    public void OnTriggerEnter2D(Collider2D collision)
    {
        String tag = collision.gameObject.tag;
        if (!tag.Equals("Player"))
        {
            animator.SetBool("collide", true);
            contact = true;
        }
        if (tag.Equals("Enemy"))
        {
            collision.GetComponent<Hit>().takeDamage(1);
        }
    }
    //Code to delete projectile
    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
