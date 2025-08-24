using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    public float health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
