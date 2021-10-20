using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public float countdown = 2f;

    // Update is called once per frame
    void Update()
    {
        // Countdown until explosion.
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Debug.Log("Explosion");
            MapDestroyer.mapDestroyer.Explode(transform.position);
            Destroy(gameObject);
        }
        
    }
}
