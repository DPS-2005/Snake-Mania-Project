using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{    void Start()
    {
        Debug.Log("Starting Connection");
        PhotonNetwork.ConnectUsingSettings();
        
    }

   public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();
    }

    override public void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Joined Lobby");
        PhotonNetwork.JoinOrCreateRoom("TestingPhaseRoom", null, null);
    }
}
