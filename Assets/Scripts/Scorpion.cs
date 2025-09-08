using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Scorpion : MonoBehaviour
{
    private bool engaged;
    private float timer;
    private Vector2 currentDistance;
    public float engageDistance;
    public float speed;
    private GameObject player;
    public GameObject firePoint;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject stinger;
    private Rigidbody2D rb;
    private Hit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        engaged = false;
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
        hit = GetComponent<Hit>();
        rb = GetComponent<Rigidbody2D>();
        timer = 2;
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = transform.position - player.transform.position;

        if (animator.GetBool("crawl") && !engaged)
        {
            rb.gravityScale = 0;
            transform.Translate(new Vector2(0, -1) * speed * Time.deltaTime);
            engaged = hit.hit;
            if (engaged)
            {
                rb.gravityScale = 1;
                animator.SetBool("crawl", false);
                engaged = true;
            }
        }
        /*else if (!engaged)
        {
            
            if (currentDistance.x <= engageDistance && transform.position.x > player.transform.position.x)
            {
                engaged = true;
            }
            else if (currentDistance.x >= engageDistance * -1 && transform.position.x < player.transform.position.x) 
            {
                engaged = true;
            }
        }*/
        else
        {
            if (transform.position.x > player.transform.position.x && spriteRenderer.flipX == false)
            {
                spriteRenderer.flipX = true;
                firePoint.transform.localPosition = new Vector2(-.405F, firePoint.transform.localPosition.y);

            }
            else if (transform.position.x < player.transform.position.x && spriteRenderer.flipX == true)
            {
                spriteRenderer.flipX = false;
                firePoint.transform.localPosition = new Vector2(.414F, firePoint.transform.localPosition.y);
            }

            if (currentDistance.x > engageDistance && transform.position.x > player.transform.position.x && animator.GetBool("attack") == false)
            {
                transform.Translate(new Vector2(-1, 0) * speed * Time.deltaTime);
                animator.SetBool("walking", true);
            }
            else if (currentDistance.x < engageDistance * -1 && transform.position.x < player.transform.position.x && animator.GetBool("attack") == false)
            {
                transform.Translate(new Vector2(1, 0) * speed * Time.deltaTime);
                animator.SetBool("walking", true);
            }
            else
            {
                animator.SetBool("walking", false);
            }

            if (timer >= 4)
            {
                animator.SetBool("walking", false);
                animator.SetBool("attack", true);
                timer = 0;
            }
            
            timer += Time.deltaTime;
        }
    }

    public void fireStinger()
    {
        Instantiate(stinger, firePoint.transform.position, Quaternion.identity);
    }

    public void attackEnd() 
    {
        animator.SetBool("attack", false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile" || collision.gameObject.tag == "Floor")
        {
            rb.gravityScale = 1;
            animator.SetBool("crawl", false);
            engaged = true;
        }
    }
}
