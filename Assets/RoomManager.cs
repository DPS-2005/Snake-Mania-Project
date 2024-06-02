using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{    
    public GameObject Player;
    public Transform SpawnPoint;
    
    
    void Start()
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

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined Room");
        GameObject _player = PhotonNetwork.Instantiate(Player.name, SpawnPoint.position, SpawnPoint.rotation);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
    }
}
