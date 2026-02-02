using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private GameObject tilemap;
    private CompositeCollider2D CompositeCollider2D;
    public GameObject rotatePoint;
    private Rigidbody2D rb;
    private PlayerAim playerAim;
    private bool facingRight;
    private float horizontal;
    public float speed;
    public float jump;
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
        playerAim = rotatePoint.GetComponent<PlayerAim>();
        facingRight = true;
        land = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Gets movement from the keyboard arrows or A and D
        horizontal = Input.GetAxis("Horizontal");
        //Checks if player is in the air
        if (rb.linearVelocity.y != 0 && land == false)
        {
            animator.SetBool("walking", false);
            animator.SetBool("jump", true);
            transform.Translate(new Vector2(horizontal, 0) * speed * Time.deltaTime);
        }
        //Moves player if input is detected
        else if (horizontal != 0)
        {
            animator.SetBool("walking", true);
            transform.Translate(new Vector2(horizontal, 0) * speed * Time.deltaTime);
            animator.SetBool("jump", false);
            land = false;
        }
        //Return to idle state if not moving
        else {
            animator.SetBool("walking", false);
            animator.SetBool("jump", false);
            land = false;
        }
        //Code to change direction player is facing
        if (horizontal < 0 && facingRight)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            rotatePoint.transform.localScale = new Vector3(rotatePoint.transform.localScale.x * -1, 1, 1);
            rotatePoint.transform.rotation = Quaternion.Euler(0, 0, playerAim.rotZ);
            facingRight = false;
        }
        else if (horizontal > 0 && !facingRight) {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            rotatePoint.transform.localScale = new Vector3(rotatePoint.transform.localScale.x * -1, 1, 1);
            rotatePoint.transform.rotation = Quaternion.Euler(0, 0, playerAim.rotZ);
            facingRight = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        rb.linearVelocityY = jump;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Detect if player lands after going airborne
        if (collision.gameObject.tag.Equals("Floor"))
        {
            land = true;
        }
    }
}
