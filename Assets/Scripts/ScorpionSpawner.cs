using UnityEngine;

public class ScorpionSpawner : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderder;
    public float closeDistance;
    private float currentDistance;
    private bool spawned;
    public GameObject scorpion;
    public Sprite scorpionHole0;
    public Sprite scorpionHole1;
    public Sprite scorpionHole2;
    public Sprite scorpionHole3;
    public Sprite scorpionHole4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawned = false;
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderder = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
        {
            currentDistance = transform.position.x - player.transform.position.x;
            if (transform.position.x > player.transform.position.x && currentDistance > closeDistance)
            {
                spriteRenderder.sprite = scorpionHole0;
            }
            else if (transform.position.x > player.transform.position.x && currentDistance <= closeDistance)
            {
                GameObject newScorpion = Instantiate(scorpion, transform.position, Quaternion.identity);
                spawned = true;
                spriteRenderder.sprite = scorpionHole4;
            }
            else if (transform.position.x < player.transform.position.x && -currentDistance <= closeDistance)
            {
                GameObject newScorpion = Instantiate(scorpion, transform.position, Quaternion.identity);
                spawned = true;
                spriteRenderder.sprite = scorpionHole4;
            }
            else if (transform.position.x < player.transform.position.x && -currentDistance > closeDistance)
            {
                spriteRenderder.sprite = scorpionHole3;
            }
        }
    }
}
