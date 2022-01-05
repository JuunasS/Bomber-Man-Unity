using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f;
    public MapDestroyer mapDestroyer;
    public GameObject player;

    void Start()
    {
        // Find MapDestroyer script through Map
        mapDestroyer = GameObject.Find("Map").transform.GetChild(0).gameObject.GetComponent<MapDestroyer>();
    }


    [PunRPC]
    // Update is called once per frame
    void Update()
    {
        // Countdown until explosion.
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Debug.Log("Explosion");
            mapDestroyer.Explode(transform.position, this.player);
            Destroy(this.gameObject);
        }
        
    }

    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
