using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementH : MonoBehaviour
{
    //need to add json or other format to save some datapoints
    private struct CurrentModel{
        public string ModelName;
        public int ModelGap;
        public float interSpeed;
    }
    CurrentModel Duck{
        get{return new CurrentModel{ModelName = "Duck", ModelGap = 36, interSpeed = 0.134f};}
    }

    private Vector3 lastpos = new Vector3(0, 0, 0);
    public float moveSpeed = 5;
    public float turnspeed = 180;
    public float bodyMoveSpeed =5;
    public GameObject bodyPrefab;
    public GameObject TailPrefab;
    public int gap = 100;
    public List<GameObject> BodyList = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();
    
    // for wriggling
    // public GameObject cam;
    // public int turnLimit = 50;
    // private int meowmeow = 0;
    // private bool turnflag = false;
    // Start is called before the first frame update
    void Start()
    {
        AddTail();
        IncreaseLength();
        IncreaseLength();
        IncreaseLength();
        IncreaseLength();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        float turn = Input.GetAxis("Horizontal");
        // Debug.Log(turn);
        transform.Rotate(transform.up, turnspeed * turn * Time.deltaTime);

        // For Wriggling. Not perfect yet. Causes jitter. Needs better solution
        // if(meowmeow>turnLimit){  turnflag = !turnflag; meowmeow = 0;}
        // Debug.Log(turnflag+" "+meowmeow);
        // meowmeow++;
        // transform.Rotate(transform.up, (int)(((turnflag?1:-1)*turnspeed * Time.deltaTime)*0.9) + turnspeed * turn * Time.deltaTime);
        // cam.transform.Rotate(transform.up, -(int)(((turnflag?1:-1)*turnspeed * Time.deltaTime)*0.6));
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncreaseLength();
        }
        positionHistory.Insert(0, transform.position);

        // for keeping the list to only a certain length
        // Debug.Log(positionHistory.Count + " " + positionHistory[0] + " " + positionHistory[positionHistory.Count-1]);
        // if(positionHistory.Count > 1000){positionHistory.RemoveRange(1000,1);}
        int index = 0;
        int ans = new int();
        foreach (GameObject body in BodyList)
        {
            ans = index * gap;
            // Debug.Log("ans "+ans);

            Vector3 point = positionHistory[Mathf.Min(ans, positionHistory.Count - 1)];
            Vector3 pointDir = (point - body.transform.position).normalized;
            // Debug.Log(index + " " + point);


            //########## One Method
            // body.transform.position += pointDir * bodyMoveSpeed * Time.deltaTime;
            // body.transform.LookAt(point);


            //########  Second Method
            float interpolationSpeed = Duck.interSpeed; // Adjust this value to control the smoothness of the movement
            
            Vector3 targetPosition = point + pointDir * gap; // Calculate the target position for the body object

            body.transform.position = Vector3.Lerp(body.transform.position, targetPosition, interpolationSpeed * Time.deltaTime); // Smoothly move the body object towards the target position
        
            Quaternion targetRotation = point == body.transform.position ?Quaternion.identity:Quaternion.LookRotation(point - body.transform.position);
            body.transform.rotation = Quaternion.Slerp(body.transform.rotation, targetRotation, 60000 * Time.deltaTime);
            //second method end

            index++;

        }
        lastpos = positionHistory[Mathf.Min(ans, positionHistory.Count - 1)];

        // foreach(var pos in positionHistory){
        //     Debug.Log(pos);
        // }


    }



    void IncreaseLength()
    {
        GameObject body = Instantiate(bodyPrefab);
        body.transform.position = lastpos;
        BodyList.Insert(BodyList.Count - 1, body);
    }
    void AddTail()
    {
        GameObject body = Instantiate(TailPrefab);
        body.transform.position = lastpos;
        BodyList.Add(body);
    }
}