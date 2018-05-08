using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float horizontalDirection;
    public float maxSpeedX = 5.0f;
    float maxSpeedY;
    public float speedX;
    public float speedY;
    [Range(400, 10000)]
    public float xForce;
    [Range(0, 200)]
    public float yForce;
    Rigidbody2D rb;
    public GameObject Generator;
    [Range(0, 0.5f)]
    [Header("感應地板距離")]
    public float distance;
    [Header("偵測地板射線起點")]
    public Transform groundCheck;
    public Transform groundCheck2;
    [Header("偵測左右是否貼牆的射線起點")]
    public Transform rightWallcheck;
    public Transform leftWallcheck;
    public LayerMask groundLayer;
    public LayerMask cubeLayer;
    bool StuckwallCheck;
    public bool grounded;
    static PlayerState _state;
    public enum PlayerState
    {
        s_idle,
        s_groundedHoldingidle,
        s_Holdingidle,
        s_movingTolastCube,
        s_moving,
        s_jumping,
        s_spawning,
    }

    bool wallchecker
    {
        get
        {
            Vector2 start = rightWallcheck.position;
            Vector2 start2 = leftWallcheck.position;
            Vector2 end = new Vector2(start.x+distance, start.y);
            Vector2 end2 = new Vector2(start2.x-distance, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            Debug.DrawLine(start2, end2, Color.yellow);
            if (Physics2D.Linecast(start, end, groundLayer) || Physics2D.Linecast(start2, end2, groundLayer))
            {
                StuckwallCheck = true;
            }
            else
            {
                StuckwallCheck = false;
            }
            return StuckwallCheck;
        }
    }
    bool Isground
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 start2 = groundCheck2.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Vector2 end2 = new Vector2(start2.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            Debug.DrawLine(start2, end2, Color.blue);
            if (Physics2D.Linecast(start, end, groundLayer) || Physics2D.Linecast(start2, end2, groundLayer))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
            return grounded;
        }
    }
    // Use this for initialization
    void Start()
    {
        _state = PlayerState.s_idle;
        rb = GetComponent<Rigidbody2D>();
    }
 

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        switch (_state)
        {
            case PlayerState.s_idle:
                break;
            case PlayerState.s_moving:
                MovementX();
                break;
            case PlayerState.s_jumping:
                MovementX();
                jump();
                break;
            case PlayerState.s_spawning:
                break;
            case PlayerState.s_groundedHoldingidle:
                MovementX();
                jump();
                break;
            case PlayerState.s_Holdingidle:
                break;
            case PlayerState.s_movingTolastCube:
                break;

        }
        //move control

        ControlSpeed();
        Debug.Log(_state);
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

        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if (wallchecker == true && Isground == true || wallchecker==false)
        rb.AddForce(new Vector2(horizontalDirection * xForce * Time.deltaTime,0),ForceMode2D.Force);

    }
    public void ControlSpeed()
    {
        speedX = rb.velocity.x;
        speedY = rb.velocity.y;
        float newspeedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
        rb.velocity = new Vector2(newspeedX, speedY);

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(0, speedY);
        }
    }


    void ChangeState()
    {
        if (rb.velocity == Vector2.zero)
        {
            _state = PlayerState.s_idle;
        }
        if (rb.velocity.y ==0 &&Input.GetAxisRaw("Horizontal")!=0)
        {
            _state = PlayerState.s_moving;
        }
        if (rb.velocity.y!=0|| Isground && Input.GetKeyDown(KeyCode.Space)||Isground==false)
        {
            _state = PlayerState.s_jumping;
        }

        if (Generator.GetComponent<spawn>().CanRelease == true)//上次進度 要想好這個狀態的條件
        {
            _state = PlayerState.s_groundedHoldingidle;
        }
        if (Generator.GetComponent<spawn>().CanRelease == true && Isground==false)
        {
            _state = PlayerState.s_Holdingidle;
        }
        if (Generator.GetComponent<spawn>().movingTolastCube == true)
        {
            _state = PlayerState.s_movingTolastCube;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            _state = PlayerState.s_spawning;
        }

    }
}
