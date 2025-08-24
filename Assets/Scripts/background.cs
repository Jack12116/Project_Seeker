using UnityEngine;

public class background : MonoBehaviour
{
    private Camera cam;
    private float offsetX;
    private float offsetY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        offsetX = transform.position.x;
        offsetY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
