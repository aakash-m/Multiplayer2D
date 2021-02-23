using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Enemy : MonoBehaviour
{
    PlayerController[] players;
    PlayerController nearestPlayer;

    [SerializeField] float speed = 3f;
    [SerializeField] GameObject deathVFX;

    private Animator anim;
    private Score scoreGo;
    PhotonView photonView;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        players = FindObjectsOfType<PlayerController>();
        //anim = GetComponent<Animator>();
        scoreGo = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer1 = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceToPlayer2 = Vector2.Distance(transform.position, players[1].transform.position);

        if (distanceToPlayer1 < distanceToPlayer2)
        {
            nearestPlayer = players[0];
        }
        else
        {
            nearestPlayer = players[1];
        }

        if (nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
            //anim.SetBool("isWalking", true);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.CompareTag("GoldenRay"))
            {
                scoreGo.AddScore();
                photonView.RPC("SpawnParticleRPC", RpcTarget.All);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    [PunRPC]
    private void SpawnParticleRPC()
    {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
    }


}
