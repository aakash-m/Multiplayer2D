using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Score : MonoBehaviour
{
    int score = 0;
    PhotonView photonView;
    public TextMeshProUGUI scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        photonView.RPC("AddScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    private void AddScoreRPC()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }

}
