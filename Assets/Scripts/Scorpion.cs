using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Scorpion : MonoBehaviour
{
    private bool engaged;
    private float timer;
    private Vector2 currentDistance;
    public float engageDistance;
    public float speed;
    public GameObject player;
    public GameObject firePoint;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public GameObject stinger;
    private TakeDamage takeDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        engaged = false;
        animator = GetComponent<Animator>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
        takeDamage = GetComponent<TakeDamage>();
        timer = 4;
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = transform.position - player.transform.position;
        if (!engaged)
        {
            engaged = takeDamage.hit;
            if (currentDistance.x <= engageDistance && transform.position.x > player.transform.position.x)
            {
                engaged = true;
            }
            else if (currentDistance.x >= engageDistance * -1 && transform.position.x < player.transform.position.x) 
            {
                engaged = true;
            }
        }
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
            else if (timer >= 4)
            {
                animator.SetBool("walking", false);
                animator.SetBool("attack", true);
                timer = 0;
            }
            else
            {
                animator.SetBool("walking", false);
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
}
