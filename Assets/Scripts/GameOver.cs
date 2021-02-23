using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;


public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public GameObject restartButton;
    public GameObject waitingText;

    PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        //finalScoreText.text = FindObjectOfType<Score>().score.ToString();
        photonView = GetComponent<PhotonView>();

        finalScoreText.text = FindObjectOfType<Score>().scoreDisplay.text;

        if (!PhotonNetwork.IsMasterClient)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
        }
        else
        {
            restartButton.SetActive(true);
            waitingText.SetActive(false);
        }

    }

    public void RestartGame()
    {
        photonView.RPC("RestartGameRPC", RpcTarget.All);
    }

    [PunRPC]
    private void RestartGameRPC()
    {
        PhotonNetwork.LoadLevel("Game");
    }


}
