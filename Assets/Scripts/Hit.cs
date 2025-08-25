using UnityEngine;

public class Hit : MonoBehaviour
{
    public float health;
    public bool hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage (float damage) 
    { 
        health -= damage;
        if (health <= 0 )
        {
            Destroy(this.gameObject);
        }
        hit = true;
    }
}
