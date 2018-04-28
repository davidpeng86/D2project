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
    public Transform player;
    public LayerMask cubeLayer;
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
        test = upCheck;
        test = downCheck;
        test = leftCheck;
        test = rightCheck;
        Spawncube();




    }
    void Spawncube()
    {



        //伸出方塊
        if (Input.GetKey(KeyCode.Z))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && is_spawning == false && upCheck == false)
            {
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player;
                StartCoroutine(Move(child, "up"));
                Move_self("up");
                if (cube_exist == false)
                {
                    record2history("up", child);
                }

            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && is_spawning == false && downCheck == false)
            {
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player;
                StartCoroutine(Move(child, "down"));
                Move_self("down");
                if (cube_exist == false)
                {
                    record2history("down", child);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && is_spawning == false && leftCheck == false)
            {
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player;
                StartCoroutine(Move(child, "left"));
                Move_self("left");
                if (cube_exist == false)
                {
                    record2history("left", child);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && is_spawning == false && rightCheck == false)
            {
                is_spawning = true;
                Transform child = Instantiate(prefab, this.transform.position, this.transform.rotation);
                child.transform.parent = player;
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
                child.transform.parent = player;
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
                child.transform.parent = player;
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
                child.transform.parent = player;
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
                child.transform.parent = player;
                StartCoroutine(Move(child, "right"));
                Move_self("right");
                if (cube_exist == false)
                {
                    record2history("right", child);
                }
            }





        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            gen = GameObject.FindGameObjectsWithTag("generated");
            GameObject group = new GameObject("blocks");
            for (int i = 0; i < gen.Length; i++)
            {
                gen[i].transform.parent = group.transform;
            }
            group.AddComponent<Rigidbody2D>();
            group.transform.Translate(new Vector2(1.35f, 0), Space.World);
            transform.position = transform.parent.position;
        }

    }

    void record2history(string direction, Transform child_position)
    {
        history.Add(new child(direction, child_position));
    }

    //move generator after spawning cube
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

    //move the spawned cube
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






}
