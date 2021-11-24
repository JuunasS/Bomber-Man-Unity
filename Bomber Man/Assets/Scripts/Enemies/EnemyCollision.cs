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
            Destroy(gameObject);
            new WaitForSeconds(1);
            GameManager.manager.CheckGameState();
        }
    }
}
