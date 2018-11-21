using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public GameObject Generator;

    public float maxSpeedX ;
    [Header("右:true左:false")]
    public bool direction;
    [Header("施力大小")]
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
            Debug.DrawLine(start, end, Color.red);
            if(Generator.GetComponent<spawn>().history.Count>1)
            {
                for(int i = 0; i <= Generator.GetComponent<spawn>().history.Count-2;i++)
                {
                    if(Generator.GetComponent<spawn>().SpawningCube[i].Upwallchecker)
                    {
                        return true;
                    }
                }
            }
            
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
            Debug.DrawLine(start, end, Color.red);
            if(Generator.GetComponent<spawn>().history.Count>1)
            {
                for(int i = 0; i <= Generator.GetComponent<spawn>().history.Count-2;i++)
                {
                    if(Generator.GetComponent<spawn>().SpawningCube[i].Downwallchecker)
                    {
                        return true;
                    }
                }
            }
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
            Vector2 start = new Vector2(this.transform.position.x-0.6f,this.transform.position.y+0.57f);
            Vector2 end = new Vector2(start.x-0.05f, start.y);
            Debug.DrawLine(start, end, Color.red);
			Vector2 start2 = new Vector2(this.transform.position.x-0.6f,this.transform.position.y-0.57f);
            Vector2 end2 = new Vector2(start2.x - 0.05f, start2.y);
            Debug.DrawLine(start2, end2, Color.red);
            if(Generator.GetComponent<spawn>().history.Count>1)
            {
                for(int i = 0; i <= Generator.GetComponent<spawn>().history.Count-2;i++)
                {
                    if(Generator.GetComponent<spawn>().SpawningCube[i].Leftwallchecker)
                    {
                        return true;
                    }
                }
            }
            if (Physics2D.Linecast(start2, end2, groundLayer)||Physics2D.Linecast(start, end, groundLayer))
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
			Vector2 start = new Vector2(this.transform.position.x+0.6f,this.transform.position.y-0.57f);
            Vector2 end = new Vector2(start.x+0.05f, start.y);
            Debug.DrawLine(start, end, Color.red);
            Vector2 start2 = new Vector2(this.transform.position.x+0.6f,this.transform.position.y+0.57f);
            Vector2 end2 = new Vector2(start2.x + 0.05f, start2.y);
            Debug.DrawLine(start2, end2, Color.red);
            if(Generator.GetComponent<spawn>().history.Count>1)
            {
                for(int i = 0; i <= Generator.GetComponent<spawn>().history.Count-2;i++)
                {
                    if(Generator.GetComponent<spawn>().SpawningCube[i].Rightwallchecker)
                    {
                        return true;
                    }
                }
            }
            if (Physics2D.Linecast(start, end, groundLayer)||Physics2D.Linecast(start2, end2, groundLayer))
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
            Vector2 start = Vector2.zero;
            Vector2 start2 = Vector2.zero;
            Vector2 end = Vector2.zero;
            Vector2 end2 = Vector2.zero;
            if (rb.gravityScale > 0)
            {
                start = new Vector2(this.transform.position.x - 0.6f, this.transform.position.y - 0.61f);
                start2 = new Vector2(this.transform.position.x + 0.6f, this.transform.position.y - 0.61f);
                end = new Vector2(start.x, start.y - distance);
                end2 = new Vector2(start2.x, start.y - distance);
            }
            else
            {
                start = new Vector2(this.transform.position.x - 0.6f, this.transform.position.y + 0.61f);
                start2 = new Vector2(this.transform.position.x + 0.6f, this.transform.position.y + 0.61f);
                end = new Vector2(start.x, start.y + distance);
                end2 = new Vector2(start2.x, start.y + distance);
            }
            Debug.DrawLine(start, end, Color.yellow);
            Debug.DrawLine(start2, end2, Color.yellow);
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
            Vector2 Down = Vector2.zero;
            Vector2 end = Vector2.zero;
            if(rb.gravityScale >0)
            {
                Down = new Vector2(this.transform.position.x,this.transform.position.y-0.64f);
                end = new Vector2(Down.x, Down.y - distance);
            }
            else
            {
                Down = new Vector2(this.transform.position.x,this.transform.position.y+0.64f);
                end = new Vector2(Down.x, Down.y + distance);
            }
            
            if (Generator.GetComponent<spawn>().cubeCheck && Isground == false ||Generator.GetComponent<spawn>().cubeCheck && Physics2D.Linecast(Down, end, cubeLayer))
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
            Vector2 right = Vector2.zero;
            Vector2 Down1 = Vector2.zero;
            Vector2 end = Vector2.zero;
            Vector2 end2= Vector2.zero;
            Vector2 left = Vector2.zero;
            Vector2 Down2 = Vector2.zero;
            Vector2 end3 = Vector2.zero;
            Vector2 end4 = Vector2.zero;
            if(rb.gravityScale >0)
            {
                right = new Vector2(this.transform.position.x+0.61f,this.transform.position.y-0.57f);
                Down1 = new Vector2(this.transform.position.x+0.6f,this.transform.position.y-0.61f);
                end = new Vector2(right.x + distance, right.y);
                end2= new Vector2(Down1.x, Down1.y - distance);
                Debug.DrawLine(right, end, Color.green);
                Debug.DrawLine(Down1, end2, Color.green);
                left = new Vector2(this.transform.position.x-0.62f,this.transform.position.y-0.57f);
                Down2 =new Vector2(this.transform.position.x-0.6f,this.transform.position.y-0.61f);
                end3 = new Vector2(left.x - distance, left.y);
                end4 = new Vector2(Down2.x, Down2.y - distance);
                Debug.DrawLine(left, end3, Color.green);
                Debug.DrawLine(Down2, end4, Color.green);
            }
            else if(rb.gravityScale < 0)
            {
                right = new Vector2(this.transform.position.x+0.61f,this.transform.position.y+0.57f);
                Down1 = new Vector2(this.transform.position.x+0.6f,this.transform.position.y+0.61f);
                end = new Vector2(right.x + distance, right.y);
                end2= new Vector2(Down1.x, Down1.y + distance);
                Debug.DrawLine(right, end, Color.green);
                Debug.DrawLine(Down1, end2, Color.green);
                left = new Vector2(this.transform.position.x-0.62f,this.transform.position.y+0.57f);
                Down2 =new Vector2(this.transform.position.x-0.6f,this.transform.position.y+0.61f);
                end3 = new Vector2(left.x - distance, left.y);
                end4 = new Vector2(Down2.x, Down2.y + distance);
                Debug.DrawLine(left, end3, Color.green);
                Debug.DrawLine(Down2, end4, Color.green);
            }

            if (Physics2D.Linecast(right, end, cubeLayer) && Physics2D.Linecast(Down1, end2, cubeLayer) || Physics2D.Linecast(right, end, cubeLayer) && Physics2D.Linecast(Down2, end4, cubeLayer))
            {
                return 1;
            }
            else if (Physics2D.Linecast(left, end3, cubeLayer) && Physics2D.Linecast(Down1, end2, cubeLayer) || Physics2D.Linecast(left, end3, cubeLayer) && Physics2D.Linecast(Down2, end4, cubeLayer))
            {
                return 2;
            }
            else if (Physics2D.Linecast(left, end3, cubeLayer) || Physics2D.Linecast(right, end, cubeLayer))
            {
                return 3;
            }
            else
            {
                return 4;
            }
		}
	}
    void Start()
    {
        anim_ = GetComponent<Animator>();
        _state = PlayerState.s_idle;
        rb = GetComponent<Rigidbody2D>();
    }
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
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
            case PlayerState.s_groundedHoldingidle:
                MovementX();
                jump();
                break;

            case PlayerState.s_movingTolastCube:
                break;

        }
        if(rb.velocity.x!=0)
        {

            anim_.SetBool("Move",true);
        }
        else
        {
            anim_.SetBool("Move",false);
        }
        Debug.Log(_state);
    }

   void jump()
    {
        
        if (Isground && Input.GetKeyDown(KeyCode.Space)&& rb.gravityScale > 0)
        {
            FindObjectOfType<AudioManager>().play("jump");
            rb.AddForce(Vector2.up * yForce, ForceMode2D.Impulse);
        }
        else if (Isground && Input.GetKeyDown(KeyCode.Space) && rb.gravityScale < 0)
        {
            FindObjectOfType<AudioManager>().play("jump");
            rb.AddForce(Vector2.down * yForce, ForceMode2D.Impulse);
        }
    }

    void MovementX()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if (Leftwallchecker == true && horizontalDirection == 1 || OnBlocksCheck == 2 && horizontalDirection == 1)
        {
            rb.velocity = new Vector2(maxSpeedX * horizontalDirection , rb.velocity.y);
        }
        else if (Rightwallchecker == true && horizontalDirection == -1 || OnBlocksCheck == 1 && horizontalDirection == -1)
        {
            rb.velocity = new Vector2(maxSpeedX * horizontalDirection, rb.velocity.y);
        }
        else if (OnBlocksCheck == 3 && Isground)
        {
            rb.velocity = new Vector2(maxSpeedX * horizontalDirection, rb.velocity.y);
        }
        else if(!Generator.GetComponent<spawn>().MovingTolastCubeGroundCheck)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if(OnBlocksCheck == 3 && Generator.GetComponent<spawn>().cubeCheck)
        {
            rb.velocity = new Vector2(maxSpeedX * horizontalDirection, rb.velocity.y);
        }
        else if (Rightwallchecker == false && Leftwallchecker == false && OnBlocksCheck == 4)
        {
            rb.velocity = new Vector2(maxSpeedX * horizontalDirection, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }		
        directionCheck();
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }


    void directionCheck()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && Generator.GetComponent<spawn>().history.Count <=1)
        {
                direction = true;
                transform.localScale =new Vector3(1, transform.localScale.y, 1);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && Generator.GetComponent<spawn>().history.Count <=1)
        {
                direction = false;
                transform.localScale =new Vector3(-1, transform.localScale.y, 1);
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
        if (Holdingbool==true)
        {
            _state = PlayerState.s_Holdingidle;
        }
        if (rb.velocity.y!=0)
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
