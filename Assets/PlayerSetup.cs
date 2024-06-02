using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
  public Movement1 movement;

  public void isLocalPlayer(){
    movement.enabled = true;
  }
}
