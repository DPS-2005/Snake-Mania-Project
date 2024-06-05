using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Unity.VisualScripting;
using TMPro;

public class RoomList : MonoBehaviourPunCallbacks
{

    public GameObject roomManagerGO;
    public RoomManager roomManager;

    public static RoomList Instance ;
    [Header("UI")] public Transform roomListParent;
    public GameObject roomListItemPrefab;

    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    private void Awake(){
        Instance = this;
    }


    public void ChangeRoomToCreateName(string _roomname){
        roomManager.RoomtoJoin = _roomname;
        // roomManagerGO.SetActive(true);
    }


// RoomOptions options = new RoomOptions();
        // options.MaxPlayers = 2;
        // PhotonNetwork.JoinOrCreateRoom(RoomtoJoin, options, null);
        // PhotonNetwork.JoinOrCreateRoom(RoomtoJoin, null, null);

    IEnumerator Start()
    {
        if (PhotonNetwork.InRoom){
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }
        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }


     public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList){
        if(cachedRoomList.Count <=0){
            cachedRoomList = roomList;
        }
        else{
            foreach( var room in roomList){
                for(int i =0; i<cachedRoomList.Count; i++){
                    if(cachedRoomList[i].Name == room.Name){
                        List<RoomInfo> newList = cachedRoomList;
                        //add the two player criteria here maybe
                        
                        if(room.RemovedFromList){
                            newList.Remove(newList[i]);
                        }
                        
                        else{
                            newList[i] = room;
                        }
                    
                        cachedRoomList = newList;
                    }
                }
            }
        }
        updateUI();
    }

    void updateUI(){
        foreach(Transform roomitem in roomListParent){
            Destroy(roomitem.gameObject);
        }
        foreach(var room in cachedRoomList){
            GameObject roomitem =Instantiate(roomListItemPrefab,roomListParent);
            roomitem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name + " (" + room.PlayerCount + "/" + room.MaxPlayers + ")";
            roomitem.GetComponent<RoomItemButton>().playcount = room.PlayerCount;
            roomitem.GetComponent<RoomItemButton>().RoomName = room.Name;
        }
    }

    public void JoinRoomByName(string _name){
        
        roomManager.RoomtoJoin = _name;
        roomManagerGO.SetActive(true);
        gameObject.SetActive(false);

    }
}
