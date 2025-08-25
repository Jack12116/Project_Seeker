using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Camera cam;
    public GameObject rotatePoint;
    private Rigidbody2D rb;
    private bool facingRight;
    private float horizontal;
    public float speed;
    public float camX;
    public float camY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(horizontal, 0) * speed * Time.deltaTime);

        if (horizontal != 0)
        {
            animator.SetBool("walking", true);
        }
        else {
            animator.SetBool("walking", false);
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
        if (rb.linearVelocity.y != 0)
        {
            animator.SetBool("walking", false);
            animator.SetBool("jump", true);
        }
        else 
        {
            animator.SetBool("jump", false);
        }
    }
}
