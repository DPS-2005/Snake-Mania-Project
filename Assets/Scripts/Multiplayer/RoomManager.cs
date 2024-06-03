using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{    
    public List<GameObject> Player;
    public int playermodel = 0;
    public Transform SpawnPoint;
   [Space]
   public GameObject LoadCam;
   public static RoomManager instance;
    

    void Awake(){
        instance = this;
    }

    void Start()
    {
        Debug.Log("Starting Connection");
        PhotonNetwork.ConnectUsingSettings();
        if(playermodel>=Player.Count){playermodel=Player.Count-1;}
        
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
        LoadCam.SetActive(false);
        base.OnJoinedRoom();
        Debug.Log("Joined Room");
        GameObject _player = PhotonNetwork.Instantiate(Player[playermodel].name, SpawnPoint.position, SpawnPoint.rotation);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
    }


    public void RespawnPlayer(){
        // destroy the current player in player script. Also remeber to take into account currrrent score and use it to spawn the player with corresponding length
        GameObject _player = PhotonNetwork.Instantiate(Player[playermodel].name, SpawnPoint.position, SpawnPoint.rotation);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
    }
}
