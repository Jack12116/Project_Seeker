using System.Threading;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Camera cam;
    private Vector3 cursor;
    private Vector3 rotation;
    private float rotZ;
    private float timer;
    public float force;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Animator playerAnimator;
    private Rigidbody2D rb;
    public GameObject player;
    public GameObject aimProjectile;
    public GameObject iceShard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        animator = aimProjectile.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        spriteRenderer = aimProjectile.GetComponent<SpriteRenderer>();
        rb = player.GetComponent<Rigidbody2D>();
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Mouse1) && timer <= 0 && !playerAnimator.GetBool("jump"))
        {
            animator.SetBool("aim", true);
            cursor = cam.ScreenToWorldPoint(Input.mousePosition);
            rotation = cursor - transform.position;
            rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

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

            if (spriteRenderer.sprite != null)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && spriteRenderer.sprite.name.Equals("ice_shard_3"))
                {
                    Instantiate(iceShard, aimProjectile.transform.position, Quaternion.identity);
                    rb.linearVelocity = new Vector2(rotation.x, rotation.y).normalized * force;
                    timer = .5F;
                    animator.SetBool("aim", false);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("aim", false);
        }
    }
}
