using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviour
{
  public string nickname;
  public MultiMovement movement;
  public TextMeshPro nicknameText;
  public GameObject cam;

  public void isLocalPlayer(int lastLength = 0){
    movement.enabled = true;
    movement.isLocalPlayer = true;
    movement.ll = lastLength-2;
    cam.SetActive(true);
  }

  [PunRPC]
  public void setNickname(string _name){
    nickname = _name;
    nicknameText.text = nickname;
  }
}
