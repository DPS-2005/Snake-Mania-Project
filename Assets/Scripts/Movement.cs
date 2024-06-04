using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Photon.Pun;
using System;
using UnityEditor;

public class Movement : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5;
    public float maxSpeed;
    public float turnspeed = 180;
    private float bodyMoveSpeed;
    public int growCount;
    public GameObject bodyPrefab;
    public GameObject tailObject;
    public int gap;
    public List<GameObject> BodyList = new List<GameObject>();
    public List<Tuple<Vector3, Quaternion>> TransformHistory = new List<Tuple<Vector3, Quaternion>>();
    public int lastLength = 0;
    public int ll = 0;
    public bool increased = false;

    // for wriggling
    // public GameObject cam;
    // public int turnLimit = 50;
    // private int meowmeow = 0;
    // private bool turnflag = false;
    void Start()
    {
        AddTail();
        //for (int i = 0; i <= ll; i++)
        //{
        //    IncreaseLength();
        //}
    }

    void Update()
    {
        //Head Movement
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(transform.up, turnspeed * turn * Time.deltaTime);
        TransformHistory.Insert(0, new Tuple<Vector3,Quaternion>(transform.position, transform.rotation));
        if(TransformHistory.Count >= 1000)
            TransformHistory.RemoveAt(TransformHistory.Count - 1);
        // For Wriggling. Not perfect yet. Causes jitter. Needs better solution
        // if(meowmeow>turnLimit){  turnflag = !turnflag; meowmeow = 0;}
        // Debug.Log(turnflag+" "+meowmeow);
        // meowmeow++;
        // transform.Rotate(transform.up, (int)(((turnflag?1:-1)*turnspeed * Time.deltaTime)*0.9) + turnspeed * turn * Time.deltaTime);
        // cam.transform.Rotate(transform.up, -(int)(((turnflag?1:-1)*turnspeed * Time.deltaTime)*0.6));

        RespawnAndDestroy();

        int index = 1;
        foreach (var body in BodyList)
        {
            //Debug.Log(index * gap);
            //Debug.Log(Mathf.Min(index * gap, TransformHistory.Count - 1));
            Tuple<Vector3, Quaternion> point = TransformHistory[Mathf.Min(index * gap, TransformHistory.Count - 1)];
            body.transform.position = point.Item1;     
            body.transform.rotation = point.Item2;
            index++;
        }

        if (increased)
        {
            Grow();
            moveSpeed = Mathf.Min(moveSpeed + 1, maxSpeed);
            increased = false;
        }
    }

    protected virtual void IncreaseLength()
    {
        GameObject tail = BodyList.Last();
        GameObject body = Instantiate(bodyPrefab, tail.transform.position, tail.transform.rotation);
        BodyList.Insert(BodyList.Count - 1, body);
        tail.transform.position -= tail.transform.forward * gap;
        lastLength++;
    }

    protected virtual void AddTail()
    {
        //GameObject tail = Instantiate(tailPrefab, transform.position - transform.forward*gap, transform.rotation);
        BodyList.Add(tailObject);

    }

    public void Grow()
    {
        for (int i = 0; i < growCount; i++)
        {
            IncreaseLength();
        }
    }

    protected virtual void RespawnAndDestroy()
    {

    }

}