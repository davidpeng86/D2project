using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class child
{
    public child(string s, Transform t)
    {
        direction = s;
        spawn_position = t;
    }
    public string direction { get; set; }
    public Transform spawn_position { get; set; }
}

public class spawn : MonoBehaviour
{
    public Transform prefab;
    bool is_spawning;
    public bool cube_exist = false;
    List<child> history;
    public GameObject[] gen;
    public GameObject player;
    public LayerMask cubeLayer;
    int cubeCount =0;
    bool Xcheck=true;
    bool CanMoveCheck =false;
    bool CanRelease;
    public bool released;
    public bool right;
    public bool left;
    public bool up;
    public bool down;
    public bool test;
    [Range(0, 2.0f)]
    [Header("感應四周距離")]
    public float distance;
    [Header("偵測四周射線起點")]
    public Transform upCubeCheck;
    public Transform downCubeCheck;
    public Transform leftCubeCheck;
    public Transform rightCubeCheck;
    bool leftCheck
    {
        get
        {
            Vector2 start = leftCubeCheck.position;
            Vector2 end = new Vector2(start.x - distance, start.y);
            Debug.DrawLine(start, end, Color.blue);
            left = Physics2D.Linecast(start, end, cubeLayer);
            return left;
        }
    }
    bool rightCheck
    {
        get
        {
            Vector2 start = rightCubeCheck.position;
            Vector2 end = new Vector2(start.x + distance, start.y);
            Debug.DrawLine(start, end, Color.blue);
            right = Physics2D.Linecast(start, end, cubeLayer);
            return right;
        }
    }
    bool upCheck
    {
        get
        {
            Vector2 start = upCubeCheck.position;
            Vector2 end = new Vector2(start.x, start.y + distance);
            Debug.DrawLine(start, end, Color.blue);
            up = Physics2D.Linecast(start, end, cubeLayer);
            return up;
        }
    }
    bool downCheck
    {
        get
        {
            Vector2 start = downCubeCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            down = Physics2D.Linecast(start, end, cubeLayer);
            return down;
        }
    }









    void Start()
    {
        is_spawning = false;
        history = new List<child>();
        history.Add(new child("empty", null));
    }

    // Update is called once per frame
    void Update()
    {
        Spawncube();
        PlayerMovetoLast();




    }




    //方塊丟出.伸出.收回.防回堵
    void Spawncube()
    {



        //伸出方塊 同時判定周邊是否可以伸出方塊 且伸出時刪除既有丟出方塊
        if (Input.GetKey(KeyCode.Z) && player.GetComponent<Player>().grounded || Input.GetKey(KeyCode.Z) &&CanRelease)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow) && is_spawning == false && upCheck == false)
            {   
                if (released)
                {
                    Destroy(GameObject.Find("blocks"));
                }
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player.transform;
                StartCoroutine(Move(child, "up"));
                Move_self("up");
                if (cube_exist == false)
                {
                    record2history("up", child);
                }


            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && is_spawning == false && downCheck == false)
            {
                if (released)
                {
                    Destroy(GameObject.Find("blocks"));
                }
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player.transform;
                StartCoroutine(Move(child, "down"));
                Move_self("down");
                if (cube_exist == false)
                {
                    record2history("down", child);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && is_spawning == false && leftCheck == false)
            {
                if (released)
                {
                    Destroy(GameObject.Find("blocks"));
                }
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player.transform;
                StartCoroutine(Move(child, "left"));
                Move_self("left");
                if (cube_exist == false)
                {
                    record2history("left", child);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && is_spawning == false && rightCheck == false)
            {
                if (released)
                {
                    Destroy(GameObject.Find("blocks"));
                }
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player.transform;
                StartCoroutine(Move(child, "right"));
                Move_self("right");
                if (cube_exist == false)
                {
                    record2history("right", child);
                }
            }







            //回收方塊

            if (Input.GetKeyDown(KeyCode.UpArrow) && is_spawning == false && upCheck && history.ElementAt(history.Count - 1).direction == "down")
            {
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player.transform;
                StartCoroutine(Move(child, "up"));
                Move_self("up");
                if (cube_exist == false)
                {
                    record2history("up", child);
                }

            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && is_spawning == false && downCheck && history.ElementAt(history.Count - 1).direction == "up")
            {
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player.transform;
                StartCoroutine(Move(child, "down"));
                Move_self("down");
                if (cube_exist == false)
                {
                    record2history("down", child);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && is_spawning == false && leftCheck && history.ElementAt(history.Count - 1).direction == "right")
            {
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player.transform;
                StartCoroutine(Move(child, "left"));
                Move_self("left");
                if (cube_exist == false)
                {
                    record2history("left", child);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && is_spawning == false && rightCheck && history.ElementAt(history.Count - 1).direction == "left")
            {
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player.transform;
                StartCoroutine(Move(child, "right"));
                Move_self("right");
                if (cube_exist == false)
                {
                    record2history("right", child);
                }
            }
        }


        //判定是否有方塊伸出，可不可以丟出來
        if (history.Count - 1 > 0)
        {
            CanRelease = true;
        }
        else
        {
            CanRelease = false;
        }

        //方塊丟出
        if (Input.GetKeyDown(KeyCode.F) && CanRelease)
        {
            released = true;
            gen = GameObject.FindGameObjectsWithTag("generated");
            GameObject group = new GameObject("blocks");
            for (int i = 0; i < gen.Length; i++)
            {
                gen[i].transform.parent = group.transform;
            }
            group.AddComponent<Rigidbody2D>();
            group.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,2)*2,ForceMode2D.Impulse);
            for (int i = history.Count - 1; i > 0; i--)
            {
                history.RemoveAt(i);
            }
            transform.position = transform.parent.position;
        }

    }






    //紀錄list
    void record2history(string direction, Transform child_position)
    {
        history.Add(new child(direction, child_position));
    }





    //generator位置控制
    void Move_self(string dir)
    {
        Vector3 position = transform.position;
        switch (dir)
        {
            case "up":
                position.y += 1.35f;
                break;

            case "down":
                position.y -= 1.35f;
                break;

            case "left":
                position.x -= 1.35f;
                break;

            case "right":
                position.x += 1.35f;
                break;
        }
        transform.position = position;
    }














    //玩家移動至最後一個方塊
    void PlayerMovetoLast()
    {   
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (history.Count-1 > 0)
        {
            Xcheck = true;
        }
        else
        {
            Xcheck = false;
        }
        if (Input.GetKeyDown(KeyCode.X)&&Xcheck)
        {
            CanMoveCheck = true;
            Xcheck = false;
            cubeCount = 0;
            rb.gravityScale = 0;
            gen = GameObject.FindGameObjectsWithTag("generated");
            for (int i = 0; i < gen.Length; i++)
            {
                gen[i].transform.parent = null;
                gen[i].GetComponent<Collider2D>().isTrigger = true;
            }

            
            transform.position = player.transform.position;
        }
        if (cubeCount == history.Count-1)
        {
            cubeCount = 0;
           rb.gravityScale = 2;
           CanMoveCheck = false;
           for (int i = history.Count - 1; i > 0;i-- )
           {
               history.RemoveAt(i);
           }
        }
        if (CanMoveCheck)
        {
           StartCoroutine(MoveToLast(history.ElementAt(cubeCount+1).direction, cubeCount));
        }

    }











    //生成方塊移動控制
    IEnumerator Move(Transform T, string I)
    {
        float F = 0f;
        float spawn_speed = 0.09f;
        int n = history.Count - 1;
        Vector3 V3 = Vector3.zero;
        switch (I)
        {
            case "up":
                V3 = Vector3.up * spawn_speed;
                if (n != 0 && history.ElementAt(n).direction == "down") //destroy last cube
                {
                    Destroy(history.ElementAt(n).spawn_position.gameObject);
                    history.RemoveAt(n);
                    cube_exist = true;
                }
                break;

            case "down":
                V3 = Vector3.down * spawn_speed;
                if (n != 0 && history.ElementAt(n).direction == "up")
                {
                    Destroy(history.ElementAt(n).spawn_position.gameObject);
                    history.RemoveAt(n);
                    cube_exist = true;
                }


                break;

            case "left":
                V3 = Vector3.left * spawn_speed;
                if (n != 0 && history.ElementAt(n).direction == "right")
                {
                    Destroy(history.ElementAt(n).spawn_position.gameObject);
                    history.RemoveAt(n);
                    cube_exist = true;
                }

                break;

            case "right":
                V3 = Vector3.right * spawn_speed;
                if (n != 0 && history.ElementAt(n).direction == "left")
                {
                    Destroy(history.ElementAt(n).spawn_position.gameObject);
                    history.RemoveAt(n);
                    cube_exist = true;
                }

                break;
        }
        while (F < 1.35f)
        {
            F += spawn_speed;
            T.position += V3;
            yield return new WaitForSeconds(0.01f);
        }

        T.position = transform.position;
        is_spawning = false;

        if (cube_exist == true)
        {
            Destroy(T.gameObject);
            cube_exist = false;
        }

    }









    //移動至最後一個方塊



    IEnumerator MoveToLast(string direction,int cube)
    {
        CanMoveCheck = false;
        float moveTime = 0;
        float moveSpeed = 0.09f;
        Vector3 directionCheck=Vector3.zero;
        switch (direction)
        {
            case "up":
                directionCheck = Vector3.up * moveSpeed;
                break;
            case "down":
                directionCheck = Vector3.down * moveSpeed;
                break;
            case "left":
                directionCheck = Vector3.left * moveSpeed;
                break;
            case "right":
                directionCheck = Vector3.right * moveSpeed;
                break;
        }
        while (moveTime < 1.35f)
        {
            moveTime += moveSpeed;
            player.transform.Translate(directionCheck);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gen[cubeCount]);
        CanMoveCheck = true;
        cubeCount += 1;
        
       
       
    }















}
