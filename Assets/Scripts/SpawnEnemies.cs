using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float startTimeBtwSpawn;
    public Transform[] enemySpawnPoints;
    float timeBtwSpawn;

    // Start is called before the first frame update
    void Start()
    {
        timeBtwSpawn = startTimeBtwSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        //checking it master client and player count
        if (PhotonNetwork.IsMasterClient == false || PhotonNetwork.CurrentRoom.PlayerCount != 2)
            return;

        //spawning enemies will be done master client
        if (timeBtwSpawn <= 0)
        {
            Vector3 spawnPosition = enemySpawnPoints[Random.Range(0, enemySpawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemyPrefab.name, spawnPosition, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
