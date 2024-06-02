using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5;
    public float turnspeed = 180;
    private float bodyMoveSpeed;
    public int growCount;
    public GameObject bodyPrefab;
    public GameObject head;
    public GameObject tail;
    public float gap;
    public List<GameObject> BodyList = new List<GameObject>();
    
    // for wriggling
    // public GameObject cam;
    // public int turnLimit = 50;
    // private int meowmeow = 0;
    // private bool turnflag = false;
    void Start()
    {
        AddTail();
        IncreaseLength();
        
    }

    void Update()
    {
        //Head Movement
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(transform.up, turnspeed * turn * Time.deltaTime);

        // For Wriggling. Not perfect yet. Causes jitter. Needs better solution
        // if(meowmeow>turnLimit){  turnflag = !turnflag; meowmeow = 0;}
        // Debug.Log(turnflag+" "+meowmeow);
        // meowmeow++;
        // transform.Rotate(transform.up, (int)(((turnflag?1:-1)*turnspeed * Time.deltaTime)*0.9) + turnspeed * turn * Time.deltaTime);
        // cam.transform.Rotate(transform.up, -(int)(((turnflag?1:-1)*turnspeed * Time.deltaTime)*0.6));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grow(growCount);
        }

        //moving each body part to its next position
        float initial_speed = moveSpeed;
        //Debug.Log("count:"+BodyList.Count);
        for (int i = 0; i < BodyList.Count; i++)
        {
            Transform point;
            GameObject body = BodyList[i];
            if (i == 0)
                {point = transform;
                
                body.transform.GetChild(1).gameObject.SetActive(false);
                body.GetComponent<MeshRenderer>().enabled=false;}
            else
                point = BodyList[i - 1].transform;
                //Debug.Log(point.position);
            Vector3 pointDir = (point.position - body.transform.position).normalized;
            bodyMoveSpeed = Vector3.Dot(pointDir, point.forward) * initial_speed;
            initial_speed = bodyMoveSpeed;
            body.transform.position += pointDir * bodyMoveSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            if(i == 5)
            {
                //body.transform.Rotate(transform.up, turnspeed * (0.2f-Mathf.PingPong(Time.time, 0.4f)));
            }
        }
    }

    void IncreaseLength()
    {
        GameObject tail = BodyList.Last();
        GameObject body = Instantiate(bodyPrefab, tail.transform.position, tail.transform.rotation);
        BodyList.Insert(BodyList.Count - 1, body);
        tail.transform.position -= tail.transform.forward * gap;        
    }

    void AddTail()
    {   
        tail =Instantiate(bodyPrefab, transform.position, transform.rotation);
        BodyList.Add(tail);
        
    }

    void Grow(int n)
    {
        for (int i = 0; i < n; i++)
        {
            IncreaseLength();
        }
    }

}