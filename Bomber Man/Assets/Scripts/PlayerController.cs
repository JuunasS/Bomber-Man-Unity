using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float speed = 5.0f;
    Rigidbody2D rb2D;
    Vector2 movement;

    // Player status variables
    public int playerHealth = 3;
    public bool isAlive;
    public float vulnerabilityCD = 1.0f;
    private float vulnerabilityCDCounter = 0;
    public bool isInvulnerableCD = false;

    // Bomb placement variables
    public Tilemap tilemap;
    public GameObject bombPrefab;
    public KeyCode bombKey;

    // Bomb placement cooldown variables
    public float bombCD = 2.0f;
    private float bombCDCounter = 0;
    public bool isBombCD = false;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            // Movement inputs
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            // Bomb placement cooldown timer
            if (isBombCD)
            {
                bombCDCounter -= Time.deltaTime;
            }
            if (Input.GetKeyDown(bombKey) && bombCDCounter <= 0)
            {
                Vector3Int cell = tilemap.WorldToCell(gameObject.transform.position);
                Vector3 cellCenterpos = tilemap.GetCellCenterWorld(cell);

                Instantiate(bombPrefab, cellCenterpos, Quaternion.identity);
                bombCDCounter = bombCD;
                isBombCD = true;
            }

            // Player vulnerability timer
            if (isInvulnerableCD)
            {
                if (vulnerabilityCDCounter > 0)
                {
                    vulnerabilityCDCounter -= Time.deltaTime;
                }
            }
        }
    }
    
    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * speed * Time.deltaTime);
    }

    public int GetHealth()
    {
        return playerHealth;
    }

    // Takes health from the player if they do not have invulnerability
    public void TakeDamage(int dmg)
    {
        if((playerHealth - dmg) <= 0)
        {
            Debug.Log("Player has died.");
            GameManager.manager.CheckGameState();
        }

        if (vulnerabilityCDCounter <= 0)
        {
            Debug.Log("Player has lost " + dmg + " health.");
            playerHealth -= dmg;
            vulnerabilityCDCounter = vulnerabilityCD;
            isInvulnerableCD = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Debug.Log("Player hit by explosion!");
            GameManager.manager.CheckGameState();
            TakeDamage(1);
        } 
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by enemy!");
            TakeDamage(1);
        }
    }
}
