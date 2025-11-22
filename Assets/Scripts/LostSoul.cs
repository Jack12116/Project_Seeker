using UnityEngine;

public class LostSoul : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private bool engaged;
    private Vector2 currentDistance;
    public float engageDistance;
    private SpriteRenderer spriteRenderer;
    private float timer1;
    private float timer2;
    public float resetTimer1;
    public float resetTimer2;
    private bool animComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        engaged = false;
        player = GameObject.Find("The Seeker");
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer1 = resetTimer1;
        timer2 = 0;
        animComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = transform.position - player.transform.position;
        if (!engaged)
        {
            if (currentDistance.x <= engageDistance && transform.position.x > player.transform.position.x ||
                currentDistance.x >= engageDistance * -1 && transform.position.x < player.transform.position.x)
            {
                engaged = true;
            }
        }
        else
        {
            timer2 -= Time.deltaTime;
            if (!animator.GetBool("Disappear") && !animComplete && timer2 <= 0) 
            {
                animator.SetBool("Disappear", true);
            }
            else if (animComplete && timer1 > 0)
            {
                timer1 -= Time.deltaTime;
            }
            else if (animComplete && timer1 <= 0)
            {
                spriteRenderer.enabled = true;
                animator.SetBool("Reappear", true);
                timer1 = resetTimer1;
            }
        }
    }

    public void teleportEnd()
    {
        spriteRenderer.enabled = false;
        animComplete = true;
        animator.SetBool("Disappear", false);
    }

    public void reappearEnd()
    {
        animComplete = false;
        animator.SetBool("Reappear", false);
        timer2 = resetTimer2;
    }
}
