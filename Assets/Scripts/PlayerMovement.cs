using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private GameObject tilemap;
    private CompositeCollider2D CompositeCollider2D;
    private Camera cam;
    public GameObject rotatePoint;
    private Rigidbody2D rb;
    private bool facingRight;
    private float horizontal;
    public float speed;
    public float camX;
    public float camY;
    private bool land;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        tilemap = GameObject.FindWithTag("Floor");
        CompositeCollider2D = tilemap.GetComponent<CompositeCollider2D>();
        cam = Camera.main;
        facingRight = true;
        land = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (rb.linearVelocity != new Vector2(0, 0) && land == false)
        {
            animator.SetBool("walking", false);
            animator.SetBool("jump", true);
        }
        else if (horizontal != 0)
        {
            rb.linearVelocity = new Vector2(0, 0);
            animator.SetBool("walking", true);
            transform.Translate(new Vector2(horizontal, 0) * speed * Time.deltaTime);
            animator.SetBool("jump", false);
            land = false;
        }
        else {
            rb.linearVelocity = new Vector2(0, 0);
            animator.SetBool("walking", false);
            animator.SetBool("jump", false);
            land = false;
        }

        if (horizontal < 0 && facingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            rotatePoint.transform.localScale = new Vector3(rotatePoint.transform.localScale.x * -1, 1, 1);
            facingRight = false;
        }
        else if (horizontal > 0 && !facingRight) {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            rotatePoint.transform.localScale = new Vector3(rotatePoint.transform.localScale.x * -1, 1, 1);
            facingRight = true;
        }
        cam.transform.position = new Vector3(transform.position.x + camX, transform.position.y + camY, -10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor"))
        {
            land = true;
        }
    }
}
