using UnityEngine;

public class Background_Parallax : MonoBehaviour
{
    public float parallaxEffect;
    private float startPosX;
    private float startPosY;
    private float width;
    private float height;
    public Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceX = cam.transform.position.x * parallaxEffect;
        float distanceY = cam.transform.position.y * parallaxEffect;
        float movementX = cam.transform.position.x * (1 - parallaxEffect);
        float movementY = cam.transform.position.y * (1 - parallaxEffect);

        transform.position = new Vector2(startPosX + distanceX, startPosY + distanceY);

        if (movementX > startPosX + width)
        {
            startPosX += width;
        }

        else if (movementX < startPosX - width)
        {
            startPosX -= width;
        }

        if (movementY > startPosY + height)
        {
            startPosY += height;
        }

        else if (movementY < startPosY - height)
        {
            startPosY -= height;
        }
    }
}
