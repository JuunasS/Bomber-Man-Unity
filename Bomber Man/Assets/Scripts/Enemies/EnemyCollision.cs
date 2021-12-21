using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Checking enemy hits");
        if (collision.CompareTag("Explosion"))
        {
            Debug.Log("Enemy hit by explosion!");
            gameObject.tag = "Destroyed";
            Destroy(gameObject);
            GameManager.manager.CheckGameState();
        }
    }
}
