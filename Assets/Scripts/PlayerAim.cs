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
    private float bufferTimer;
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
        bufferTimer = resetTimer3;
    }

    // Update is called once per frame
    void Update()
    {
        //Timer in-between firing bolts
        delayTimer1 -= Time.deltaTime;
        bufferTimer -= Time.deltaTime;
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
            if (bufferTimer <= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
                bufferTimer = resetTimer3;
            }
            delayTimer2 -= Time.deltaTime;

            //Use cursor position to determine where to aim bolt
            /*
            if (rotZ <= 15 && rotZ > -15)
            {
                aimProjectile.transform.localPosition = new Vector2(.529F, .131F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (rotZ <= 45 && rotZ > 15)
            {
                aimProjectile.transform.localPosition = new Vector2(.392F, .361F);
                aimProjectile.transform.rotation = Quaternion.Euler(0,0,30);
            }
            else if (rotZ <=  75 && rotZ > 45)
            {
                aimProjectile.transform.localPosition = new Vector2(.225F, .639F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, 60);
            }
            else if (rotZ <= 105 && rotZ > 75)
            {
                aimProjectile.transform.localPosition = new Vector2(-.129F, .674F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (rotZ <= 135 && rotZ > 105)
            {
                aimProjectile.transform.localPosition = new Vector2(-.415F, .467F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, 120);
            }
            else if (rotZ <= 165 && rotZ > 135)
            {
                aimProjectile.transform.localPosition = new Vector2(-.477F, .216F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, 150);
            }
            else if (rotZ <= 180 && rotZ > 165 || rotZ < -165 && rotZ >= -180)
            {
                aimProjectile.transform.localPosition = new Vector2(-.517F, -.151F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (rotZ <= -135 && rotZ > -165)
            {
                aimProjectile.transform.localPosition = new Vector2(-.27F, -.31F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, -150);
            }
            else if (rotZ <= -105 && rotZ > -135)
            {
                aimProjectile.transform.localPosition = new Vector2(.039F, -.158F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, -120);
            }
            else if (rotZ <= -75 && rotZ > -105)
            {
                aimProjectile.transform.localPosition = new Vector2(.172F, -.048F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (rotZ <= -45 && rotZ > -75)
            {
                aimProjectile.transform.localPosition = new Vector2(.199F, -.003F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, -60);
            }
            else if (rotZ <= -15 && rotZ > -45)
            {
                aimProjectile.transform.localPosition = new Vector2(.397F, -.039F);
                aimProjectile.transform.rotation = Quaternion.Euler(0, 0, -30);
            }
            */

            //Code to fire bolt
            if (spriteRenderer.sprite != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && delayTimer2 <= 0)
                {
                    Instantiate(magicSpear, aimProjectile.transform.position, Quaternion.identity);
                    rb.linearVelocity = new Vector2(rotation.x, rotation.y).normalized * force;
                    delayTimer1 = resetTimer1;
                    delayTimer2 = resetTimer2;
                    animator.SetBool("aim", false);
                }
            }
        }
        //Code to stop aiming
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("aim", false);
            delayTimer2 = resetTimer2;
        }
    }
}
