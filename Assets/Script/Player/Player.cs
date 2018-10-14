using System.Collections;
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
    public LayerMask groundLayer;
    public LayerMask cubeLayer;
    [Header("玩家狀態")]
    public PlayerState _state;
    Rigidbody2D rb;
    float horizontalDirection;
    Animator anim_;
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
			Vector2 start = new Vector2(this.transform.position.x,this.transform.position.y+0.6f);
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
			Vector2 start = new Vector2(this.transform.position.x,this.transform.position.y-0.6f);
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
			Vector2 start2 = new Vector2(this.transform.position.x-0.6f,this.transform.position.y);
            Vector2 end2 = new Vector2(start2.x - 0.05f, start2.y);
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
			Vector2 start = new Vector2(this.transform.position.x+0.6f,this.transform.position.y);
            Vector2 end = new Vector2(start.x+0.05f, start.y);
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
			Vector2 start = new Vector2(this.transform.position.x-0.6f,this.transform.position.y-0.61f);
			Vector2 start2 = new Vector2(this.transform.position.x+0.6f,this.transform.position.y-0.61f);
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
    bool Holdingbool
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
			Vector2 Top =new Vector2 (this.transform.position.x,this.transform.position.y+0.7f);
			Vector2 end4 = new Vector2(Top.x, Top.y + distance);
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
			else if(Physics2D.Linecast(Top, end4, cubeLayer) && Isground == false)
			{
				return true;
			}
            else
            {
                return false;
            }
        }
    }
	int OnBlocksCheck
	{
		get
		{
			Vector2 right =new Vector2(this.transform.position.x+0.61f,this.transform.position.y-0.57f);
			Vector2 Down1 =new Vector2(this.transform.position.x+0.6f,this.transform.position.y-0.61f);
			Vector2 end = new Vector2(right.x + distance, right.y);
			Vector2 end2= new Vector2(Down1.x, Down1.y - distance);
			Debug.DrawLine(right, end, Color.yellow);
			Debug.DrawLine(Down1, end2, Color.yellow);
			Vector2 left = new Vector2(this.transform.position.x-0.62f,this.transform.position.y-0.57f);
			Vector2 Down2 =new Vector2(this.transform.position.x-0.6f,this.transform.position.y-0.61f);
			Vector2 end3 = new Vector2(left.x - distance, left.y);
			Vector2 end4 = new Vector2(Down2.x, Down2.y - distance);
			Debug.DrawLine(left, end3, Color.yellow);
			Debug.DrawLine(Down2, end4, Color.yellow);
			if (Physics2D.Linecast (right, end, cubeLayer) &&Physics2D.Linecast (Down1, end2, cubeLayer)||Physics2D.Linecast (right, end, cubeLayer) &&Physics2D.Linecast (Down2, end4, cubeLayer) ) {
				return 1;
			} 
			else if(Physics2D.Linecast (left, end3, cubeLayer) &&Physics2D.Linecast (Down1, end2, cubeLayer)||Physics2D.Linecast (left, end3, cubeLayer) &&Physics2D.Linecast (Down2, end4, cubeLayer))
			{
				return 2;
			}
			else
			{
				return 3;
			}
		}
	}
    // Use this for initialization
    void Start()
    {
        anim_ = GetComponent<Animator>();
        _state = PlayerState.s_idle;
        rb = GetComponent<Rigidbody2D>();
    }
 

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(Isground);
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
            FindObjectOfType<AudioManager>().play("jump");
            rb.AddForce(Vector2.up * yForce, ForceMode2D.Impulse);
			/*if (Mathf.Abs (rb.velocity.x) > 3) {
				rb.velocity=new Vector2
			}*/
        }
    }

    void MovementX()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if(horizontalDirection !=0)
        {
            anim_.SetBool("Move",true);
        }
        else
        {
            anim_.SetBool("Move",false);
        }
        if(rb.velocity.y==0)//防止滑行
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
		if (Leftwallchecker == true && horizontalDirection == 1 |OnBlocksCheck == 2 && horizontalDirection == 1)
        {
            rb.velocity = new Vector2(5*horizontalDirection,rb.velocity.y);
        }
		else if (Rightwallchecker == true && horizontalDirection == -1 ||OnBlocksCheck == 1 && horizontalDirection == -1)
        {
            rb.velocity = new Vector2(5*horizontalDirection,rb.velocity.y);
        }
		else if (Rightwallchecker == false && Leftwallchecker == false && OnBlocksCheck == 3)
		{
			rb.velocity = new Vector2(5*horizontalDirection,rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(0,rb.velocity.y);
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
        if (Input.GetAxisRaw("Horizontal") > 0 && Generator.GetComponent<spawn>().history.Count <=1)
        {
                direction = true;
                transform.localScale =new Vector3(1,1,1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && Generator.GetComponent<spawn>().history.Count <=1)
        {
                direction = false;
                
                transform.localScale =new Vector3(-1,1,1);

                
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
        if (Generator.GetComponent<spawn>().cubeCheck == true)
        {
            _state = PlayerState.s_groundedHoldingidle;
        }
        if (Generator.GetComponent<spawn>().cubeCheck == true&& Holdingbool==true)
        {
            _state = PlayerState.s_Holdingidle;
        }
        if (Isground==false && rb.velocity.y!=0)
        {
            _state = PlayerState.s_jumping;
        }
		if (Input.GetKey(KeyCode.Z) && Generator.GetComponent<spawn>().spawnCheck||Generator.GetComponent<spawn>().is_spawning )
		{
			_state = PlayerState.s_spawning;
		}
        if (Generator.GetComponent<spawn>().movingTolastCube == true)
        {
            _state = PlayerState.s_movingTolastCube;
        }
    }

}
