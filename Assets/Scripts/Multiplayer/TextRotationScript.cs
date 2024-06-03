using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotationScript : MonoBehaviour
{

    //Kya hi chal raha hai life me

    void Update()
    {
       transform.LookAt(Camera.main.transform); 
    }
}
