using UnityEngine;

public class WatchPlayer : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderder;
    public float closeDistance;
    private float currentDistance;
    public Sprite scorpionHole0;
    public Sprite scorpionHole1;
    public Sprite scorpionHole2;
    public Sprite scorpionHole3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderder = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = transform.position.x - player.transform.position.x;
        if (transform.position.x > player.transform.position.x && currentDistance > closeDistance)
        {
            spriteRenderder.sprite = scorpionHole0; 
        }
        else if (transform.position.x > player.transform.position.x && currentDistance <= closeDistance)
        {
            spriteRenderder.sprite = scorpionHole1;
        }
        else if (transform.position.x < player.transform.position.x && -currentDistance <= closeDistance)
        {
            spriteRenderder.sprite = scorpionHole2;
        }
        else if (transform.position.x < player.transform.position.x && -currentDistance > closeDistance)
        {
            spriteRenderder.sprite = scorpionHole3;
        }
    }
}
