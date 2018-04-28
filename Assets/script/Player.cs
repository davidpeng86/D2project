using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walk_speed = 100.0f;
    bool b_spawn;
    float horizontalDirection;
    public float maxSpeedX = 10.0f;
    float maxSpeedY;
    public float speedX;
    public float speedY;
    [Range(400, 600)]
    public float xForce;
    [Range(0, 200)]
    public float yForce;
    Rigidbody2D rb;
    [Range(0, 0.5f)]
    [Header("感應地板距離")]
    public float distance;
    [Header("偵測地板射線起點")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool grounded;
    bool Isground
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            grounded = Physics2D.Linecast(start, end, groundLayer);
            return grounded;
        }
    }
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void jump()
    {
        if (Isground && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * yForce, ForceMode2D.Impulse);
        }
    }

    void MovementX()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            b_spawn = true;
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            b_spawn = false;
        }
        horizontalDirection = Input.GetAxis("Horizontal");
        if (b_spawn == false)
        {
            rb.AddForce(new Vector2(horizontalDirection * xForce * Time.deltaTime, 0));
        }
    }
    public void ControlSpeed()
    {
        speedX = rb.velocity.x;
        speedY = rb.velocity.y;
        float newspeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        rb.velocity = new Vector2(newspeedX, speedY);
    }

    // Update is called once per frame
    void Update()
    {

        //move control
        MovementX();
        ControlSpeed();
        jump();


    }
}
