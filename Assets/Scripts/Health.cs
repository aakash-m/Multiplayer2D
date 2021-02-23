using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public int health = 10;
    public TextMeshProUGUI healthDisplay;
    public GameObject gameOverGO;


    PhotonView photonView;


    // Start is called before the first frame update
    void Start()
    {
        healthDisplay.text = health.ToString();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        photonView.RPC("TakeDamageRPC", RpcTarget.All); 
    }

    [PunRPC]
    void TakeDamageRPC()
    {
        health--;

        if (health <= 0)
        {
            gameOverGO.SetActive(true);
        }

        healthDisplay.text = health.ToString();
    }

}
