using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public Transform[] spawnPositions;
    
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPositions[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, Quaternion.identity);
    }

}
