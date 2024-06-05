using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Photon.Pun;
using System;
using UnityEditor;
using UnityEngine.UIElements;

public class Movement : MonoBehaviourPunCallbacks
{
    public float moveSpeed = 5;
    public float deltaSpeed;
    public float maxSpeed;
    public float turnspeed = 180;
    private float bodyMoveSpeed;
    public int growCount;
    public GameObject bodyPrefab;
    public GameObject tailObject;
    public float headGap;
    public float gap;
    public List<GameObject> BodyList = new List<GameObject>();
    public List<Vector3> Segments = new List<Vector3>();
    public int lastLength = 0;
    public int ll = 0;
    public bool increased = false;
    public bool isTerrain = false;
    private CharacterController controller;

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
        Vector3 prev = transform.position;

        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        float turn = Input.GetAxis("Horizontal");
        transform.Rotate(transform.up, turnspeed * turn * Time.deltaTime);
        Segments.Insert(0, transform.position - prev);
        if (Segments.Count >= 10000)
            Segments.RemoveAt(Segments.Count - 1);
        RespawnAndDestroy();

        int ind = 0;
        Vector3 point = transform.position;
        int i = 0;
        foreach(var body in BodyList)
        {
            var temp = gap;
            if(i == 0)
            {
                gap = headGap;
            }
            float dist = 0;
            while (ind < Segments.Count && (dist+Vector3.Magnitude(Segments[ind])) <= gap)
            {
                dist += Vector3.Magnitude(Segments[ind]);
                point -= Segments[ind];
                ind++;
            }
            if(ind == Segments.Count)
            {
                point -= Segments[ind - 1].normalized * (gap-dist);
                body.transform.position = point;
                body.transform.forward = Segments[ind - 1].normalized;
            }
            else
            {
                point -= Segments[ind].normalized * (gap - dist);
                body.transform.position = point;
                body.transform.forward = Segments[ind].normalized;
            }
            gap = temp;
            i++;
        }
        

        if (increased)
        {
            Grow();
            moveSpeed = Mathf.Min(moveSpeed + deltaSpeed, maxSpeed);
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