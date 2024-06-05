using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemButton : MonoBehaviour
{
 public string RoomName;
 [HideInInspector] public int playcount;
 public void OnButtonPressed(){
   if(playcount<2){
    RoomList.Instance.JoinRoomByName(RoomName);}
 }

}
