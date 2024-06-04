using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Photon.Pun;

public class MultiMovement : Movement
{

    public bool isLocalPlayer = false;

    protected override void IncreaseLength()
    {
        GameObject tail = BodyList.Last();
        GameObject body = PhotonNetwork.Instantiate(bodyPrefab.name, tail.transform.position, tail.transform.rotation);
        BodyList.Insert(BodyList.Count - 1, body);
        tail.transform.position -= tail.transform.forward * gap;    
        lastLength++;    
    }

    protected override void AddTail()
    {   
        GameObject tail =PhotonNetwork.Instantiate(bodyPrefab.name, transform.position, transform.rotation);
        BodyList.Add(tail);  
    }


    void destroyParts(){
        for (int i = 0; i < BodyList.Count; i++)
        {
            GameObject body = BodyList[i];
            PhotonNetwork.Destroy(body);
        }
    }

    protected override void RespawnAndDestroy()
    {
        //check if photonnewtowrk destroy is needed
        if (Input.GetKeyDown(KeyCode.Escape) && isLocalPlayer)
        {
            RoomManager.instance.RespawnPlayer(lastLength);
            destroyParts();
            PhotonNetwork.Destroy(gameObject);

        }
    }

}