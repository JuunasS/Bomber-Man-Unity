using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float countdown = 2f;
    private MapDestroyer mapDestroyer;

    void Start()
    {
        // Find MapDestroyer script through Map
        mapDestroyer = GameObject.Find("Map").transform.GetChild(0).gameObject.GetComponent<MapDestroyer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        // Countdown until explosion.
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Debug.Log("Explosion");
            mapDestroyer.Explode(transform.position);
            Destroy(gameObject);
        }
        
    }
}
