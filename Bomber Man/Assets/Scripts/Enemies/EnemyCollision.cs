using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Debug.Log("Enemy hit by explosion!");
            Destroy(gameObject);
            // Check enemy count from gamemanager FIX
        }
    }
}
