using System.Threading;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera cam;
    private Vector3 cursor;
    private Vector3 rotation;
    public float rotZ;
    private float delayTimer1;
    public float resetTimer1;
    private float delayTimer2;
    public float resetTimer2;
    public float resetTimer3;
    public float force;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Animator playerAnimator;
    private Rigidbody2D rb;
    public GameObject player;
    public GameObject aimProjectile;
    public GameObject magicSpear;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        animator = aimProjectile.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        spriteRenderer = aimProjectile.GetComponent<SpriteRenderer>();
        rb = player.GetComponent<Rigidbody2D>();
        delayTimer1 = 0;
        delayTimer2 = resetTimer2;
    }

    // Update is called once per frame
    void Update()
    {
        //Timer in-between firing bolts
        delayTimer1 -= Time.deltaTime;
        //Player can aim if timer is up and is grounded
        if (Input.GetKey(KeyCode.Mouse1) && delayTimer1 <= 0)
        {
            //Start aiming animation
            animator.SetBool("aim", true);
            //Get the cursors position on the screen
            cursor = cam.ScreenToWorldPoint(Input.mousePosition);
            //Get the mouses position relative to the player to determine aiming bolts
            rotation = cursor - transform.position;
            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
            delayTimer2 -= Time.deltaTime;

        //Code to fire bolt
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && delayTimer2 <= 0)
        {
            Instantiate(magicSpear, aimProjectile.transform.position, Quaternion.identity);
            //rb.linearVelocity = new Vector2(rotation.x, rotation.y).normalized * force;
            delayTimer1 = resetTimer1;
            delayTimer2 = resetTimer2;
            animator.SetBool("aim", false);
        }
        //Code to stop aiming
        else if (Input.GetKeyUp(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Space) && delayTimer2 > 0)
        {
            animator.SetBool("aim", false);
            delayTimer2 = resetTimer2;
        }
    }
}
