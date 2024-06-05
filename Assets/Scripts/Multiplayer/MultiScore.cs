using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Pun.UtilityScripts;

public class MultiScore : MonoBehaviour
{
    public GameObject PlayersHolder;
    public TextMeshProUGUI Player1name;
    public TextMeshProUGUI Player2name;
    public TextMeshProUGUI Player1Score;
    public TextMeshProUGUI Player2Score;
    [Header("Options")] 
    public float refreshrate = 1f;


    private void Start()
    {
        InvokeRepeating(nameof(Refresh), 1f, refreshrate);
    }

    public void Refresh(){
            
            var sortedPlayerList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();
            if(sortedPlayerList.Count < 1) return;
            Player1name.text = sortedPlayerList[0].NickName.ToString();  
            Debug.Log(sortedPlayerList[0].NickName);
            Player1Score.text = sortedPlayerList[0].GetScore().ToString();
            Player2name.text = "";
            Player2Score.text = "";
            if(sortedPlayerList.Count < 2) return;
            
            Player2name.text = sortedPlayerList[1].NickName;
            Player2Score.text = sortedPlayerList[1].GetScore().ToString();
            
    }

}
  