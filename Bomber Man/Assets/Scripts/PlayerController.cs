using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float speed = 5.0f;
    Rigidbody2D rb2D;
    Vector2 movement;
    public Animator animator;

    // Player status variables
    public int playerHealth = 3;
    public Text healthText;
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
    public int explosionDistance = 2;
    private float bombCDCounter = 0;
    public bool isBombCD = false;

    private void Awake()
    {
        if (tilemap == null)
        {
            Debug.Log("Setting tilemap");
            tilemap = GameObject.FindGameObjectWithTag("Gameplay_Tilemap").GetComponent<Tilemap>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = playerHealth.ToString();
        isAlive = true;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && !GameManager.manager.gameOver)
        {
            // Movement inputs
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            // Set animator values
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            // Bomb placement cooldown timer
            if (isBombCD)
            {
                bombCDCounter -= Time.deltaTime;
            }
            if (Input.GetKeyDown(bombKey) && bombCDCounter <= 0)
            {
                Vector3Int cell = tilemap.WorldToCell(gameObject.transform.position);
                Vector3 cellCenterpos = tilemap.GetCellCenterWorld(cell);
                GameObject bomb = Instantiate(bombPrefab, cellCenterpos, Quaternion.identity);
                bomb.GetComponent<Bomb>().SetPlayer(this.gameObject);
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
        if ((playerHealth - dmg) <= 0)
        {
            isAlive = false;
            Debug.Log("Player has died.");
            GameManager.manager.gameOver = true;
            GameManager.manager.isGameWon = false;
            GameManager.manager.CheckSingleplayerState();
            this.gameObject.SetActive(false);
        }
        if (vulnerabilityCDCounter <= 0)
        {
            Debug.Log("Player has lost " + dmg + " health.");
            playerHealth -= dmg;
            healthText.text = playerHealth.ToString();
            vulnerabilityCDCounter = vulnerabilityCD;
            isInvulnerableCD = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            Debug.Log("Player hit by explosion!");
            TakeDamage(1);
        }

        if (collision.CompareTag("Boost-1"))
        {
            // Lower cd of bomb placement for 5 seconds
            Debug.Log("Bomb cd boost activated");
            StartCoroutine(BombCdBoost(5));
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Boost-2"))
        {
            // Make player invulnerable for 5 seconds
            vulnerabilityCDCounter = 5.0f;
            isInvulnerableCD = true;
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Boost-3"))
        {
            // Set explosion distance to 5
            Debug.Log("Explosion distance boost activated");
            StartCoroutine(BombDistanceBoost(5));
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator BombCdBoost(float duration)
    {
        bombCD = 0;
        Debug.Log("Bomb cd set to 0");
        yield return new WaitForSeconds(duration);
        bombCD = 2.0f;
        Debug.Log("Bomb cd set to 2.0f");
    }

    private IEnumerator BombDistanceBoost(float duration)
    {
        explosionDistance = 5;
        Debug.Log("Explosion distance set to 5");
        yield return new WaitForSeconds(duration);
        explosionDistance = 2;
        Debug.Log("Explosion distance set to 2");
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