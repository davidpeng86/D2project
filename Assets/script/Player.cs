﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public GameObject Generator;

    public float maxSpeedX = 5.0f;
    public float speedX;
    public float speedY;
    [Header("右:true左:false")]
    public bool direction;
    [Header("施力大小")]
    [Range(400, 10000)]
    public float xForce;
    [Range(0, 200)]
    public float yForce;
    [Range(0, 0.5f)]
    [Header("感應地板距離")]
    public float distance;
    [Header("偵測地板射線起點")]
    public Transform groundCheck;
    public Transform groundCheck2;
    [Header("偵測上下左右是否貼牆的射線起點")]
    public Transform upWallcheck;
    public Transform downWallcheck;
    public Transform leftWallcheck;
    public Transform rightWallcheck;
    public LayerMask groundLayer;
    public LayerMask cubeLayer;
    [Header("玩家狀態")]
    public PlayerState _state;
    Rigidbody2D rb;
    float horizontalDirection;
    public  enum PlayerState
    {
        s_idle,
        s_groundedHoldingidle,
        s_Holdingidle,
        s_movingTolastCube,
        s_moving,
        s_jumping,
        s_spawning,
    }
    public bool Upwallchecker
    {
        get
        {
            Vector2 start = upWallcheck.position;
            Vector2 end = new Vector2(start.x, start.y+distance);
            Debug.DrawLine(start, end, Color.yellow);
            if (Physics2D.Linecast(start, end, groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    public bool Downwallchecker
    {
        get
        {
            Vector2 start = downWallcheck.position;
            Vector2 end = new Vector2(start.x, start.y-distance);
            Debug.DrawLine(start, end, Color.yellow);
            if (Physics2D.Linecast(start, end, groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    public bool Leftwallchecker
    {
        get
        {
            Vector2 start2 = leftWallcheck.position;
            Vector2 end2 = new Vector2(start2.x - distance, start2.y);
            Debug.DrawLine(start2, end2, Color.yellow);
            if (Physics2D.Linecast(start2, end2, groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
    public bool Rightwallchecker
    {
        get
        {
            Vector2 start = rightWallcheck.position;
            Vector2 end = new Vector2(start.x+distance, start.y);
            Debug.DrawLine(start, end, Color.yellow);
            if (Physics2D.Linecast(start, end, groundLayer))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
    public bool Isground
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 start2 = groundCheck2.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Vector2 end2 = new Vector2(start2.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            Debug.DrawLine(start2, end2, Color.blue);
            if (Physics2D.Linecast(start, end, groundLayer) || Physics2D.Linecast(start2, end2, groundLayer)||Physics2D.Linecast(start, end, cubeLayer) || Physics2D.Linecast(start2, end2, cubeLayer))
            {
               return true; 
            }
            else
            {
                return false;
            }
            
        }
    }
    bool groundedHoldingbool
    {
        get
        {
            Vector2 bottom =new Vector2 (this.transform.position.x,this.transform.position.y-0.7f);
            Vector2 end = new Vector2(bottom.x, bottom.y - distance);
            Debug.DrawLine(bottom, end, Color.black);
            Vector2 right = new Vector2(this.transform.position.x+0.7f, this.transform.position.y);
            Vector2 end2 = new Vector2(right.x+ distance, right.y );
            Debug.DrawLine(right, end2, Color.black);
            Vector2 left = new Vector2(this.transform.position.x-0.7f, this.transform.position.y );
            Vector2 end3 = new Vector2(left.x - distance, left.y);
            Debug.DrawLine(left, end3, Color.black);
            if (Physics2D.Linecast(bottom, end, cubeLayer))
            {
                return true;
            }
            else if (Physics2D.Linecast(right, end2, cubeLayer) && Isground == false)
            {
                return true;
            }
            else if (Physics2D.Linecast(left, end3, cubeLayer) && Isground == false)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                MovementX();
                jump();
                break;
            case PlayerState.s_moving:
                MovementX();
                jump();
                break;
            case PlayerState.s_jumping:
                MovementX();
                jump();
                break;
            case PlayerState.s_spawning:
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
            case PlayerState.s_Holdingidle:
                break;
            case PlayerState.s_groundedHoldingidle:
                MovementX();
                jump();
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
        if(rb.velocity.y==0)//防止滑行
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
        if (Rightwallchecker == false && Leftwallchecker == false)
        {
           rb.velocity = new Vector2(5*horizontalDirection,rb.velocity.y);
        }
        else if (Leftwallchecker == true && horizontalDirection == 1)
        {
            rb.velocity = new Vector2(5*horizontalDirection,rb.velocity.y);
        }
        else if (Rightwallchecker == true && horizontalDirection == -1)
        {
            rb.velocity = new Vector2(5*horizontalDirection,rb.velocity.y);
        }
        directionCheck();

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

    void directionCheck()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                direction = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = false;
            }
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
        if (Generator.GetComponent<spawn>().cubeCheck == true  )
        {
            _state = PlayerState.s_groundedHoldingidle;
        }
        if (Generator.GetComponent<spawn>().cubeCheck == true&& groundedHoldingbool==true)
        {
            _state = PlayerState.s_Holdingidle;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            _state = PlayerState.s_spawning;
        }
        if (Isground==false && rb.velocity.y!=0)
        {
            _state = PlayerState.s_jumping;
        }
        if (Generator.GetComponent<spawn>().movingTolastCube == true)
        {
            _state = PlayerState.s_movingTolastCube;
        }
    }

}
