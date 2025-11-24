using UnityEngine;

public class LostSoul : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private bool engaged;
    private Vector2 currentDistance;
    public float engageDistance;
    private SpriteRenderer spriteRenderer;
    private float timer1;
    private float timer2;
    public float resetTimer1;
    public float resetTimer2;
    public float teleportDistance;
    private bool animComplete;
    private bool faceRight;
    private int randomNumber;

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
        boxCollider = GetComponent<BoxCollider2D>();
        faceRight = false;
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
            changeAxis();
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
                boxCollider.enabled = true;
                randomNumber = Random.Range(0, 2);
                if (randomNumber == 0)
                    randomNumber = -1;
                transform.position = new Vector3(player.transform.position.x + teleportDistance * randomNumber, transform.position.y, 0);
                animator.SetBool("Reappear", true);
                timer1 = resetTimer1;
                animComplete = false;
            }
        }
    }

    public void teleportEnd()
    {
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        animComplete = true;
        animator.SetBool("Disappear", false);
    }

    public void reappearEnd()
    {
        animator.SetBool("Reappear", false);
        animator.SetBool("Attack", true);
        timer2 = resetTimer2;
    }

    public void attackEnd()
    {
        animator.SetBool("Attack", false);
    }

    public void changeAxis()
    {
        if (transform.position.x > player.transform.position.x && faceRight) 
        {
            faceRight = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
        else if (transform.position.x <= player.transform.position.x && !faceRight)
        {
            faceRight = true;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
    }
}
