using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!");
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            // Player dies
        }
    }
}
