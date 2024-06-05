using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{    
    public List<GameObject> Player;
    public int playermodel = 0;
    public Transform[] SpawnPoints;
   [Space]
   public GameObject LoadCam;
   public GameObject nameScreen;
   public GameObject playerSelectionScreen;
   public static RoomManager instance;
   private string nickname = "AnjaanVyakti";
    
    public void SetNickName(string _name){
        nickname = _name;
    }


    //Neend aa rahi hai

    public void JoinButtonPressed(){
        Debug.Log("Starting Connection");
        PhotonNetwork.ConnectUsingSettings();
        nameScreen.SetActive(false);
        LoadCam.SetActive(true);
    }

    void Awake(){
        instance = this;
    }

    void Start()
    {
       
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
        Transform SpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
        LoadCam.SetActive(false);
        base.OnJoinedRoom();
        Debug.Log("Joined Room");
        GameObject _player = PhotonNetwork.Instantiate(Player[playermodel].name, SpawnPoint.position, SpawnPoint.rotation);
        _player.GetComponent<PlayerSetup>().isLocalPlayer();
        _player.GetComponent<PhotonView>().RPC("setNickname", RpcTarget.All, nickname);
        PhotonNetwork.LocalPlayer.NickName = nickname;
        
    }


    public void RespawnPlayer(int lastLength){
        // destroy the current player in player script. Also remeber to take into account currrrent score and use it to spawn the player with corresponding length
        Transform SpawnPoint = SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length)];
        GameObject _player = PhotonNetwork.Instantiate(Player[playermodel].name, SpawnPoint.position, SpawnPoint.rotation);
        _player.GetComponent<PlayerSetup>().isLocalPlayer(lastLength);
        _player.GetComponent<PhotonView>().RPC("setNickname", RpcTarget.AllBuffered, nickname);
        PhotonNetwork.LocalPlayer.NickName = nickname;
    }


    //Honestly theres gotta be a better way to do it. but i am sleepy right now, cant be bothered to think too much
    public void onPLayerModelDuck(){
        playermodel =0;
        playerSelectionScreen.SetActive(false);
        nameScreen.SetActive(true);

    }
    public void onPLayerModelBase(){
        playermodel =2;
        playerSelectionScreen.SetActive(false);
        nameScreen.SetActive(true);

    }
    public void onPLayerModelRobot(){
        playermodel =3;
        playerSelectionScreen.SetActive(false);
        nameScreen.SetActive(true);

    }
   public void onPLayerModelBone(){
        playermodel =1;
        playerSelectionScreen.SetActive(false);
        nameScreen.SetActive(true);

    }
    public void onPLayerModelSpir(){
        playermodel =4;
        playerSelectionScreen.SetActive(false);
        nameScreen.SetActive(true);

    }
}
