using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;
using Photon.Realtime;


public class PlayerControllerMultiplayer : MonoBehaviourPunCallbacks
{
    // Movement variables
    public float speed = 5.0f;
    Rigidbody2D rb2D;
    Vector2 movement;
    public Animator animator;


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
        isAlive = true;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (isAlive)
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
                    photonView.RPC("SetBomb", RpcTarget.All);
                    
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
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * speed * Time.deltaTime);
    }

    [PunRPC]
    private void SetBomb()
    {
        Vector3Int cell = tilemap.WorldToCell(gameObject.transform.position);
        Vector3 cellCenterpos = tilemap.GetCellCenterWorld(cell);

        GameObject bomb = Instantiate(bombPrefab, cellCenterpos, Quaternion.identity);
        bomb.GetComponent<Bomb>().SetPlayer(this.gameObject);
        bombCDCounter = bombCD;
        isBombCD = true;
    }

    public int GetHealth()
    {
        return playerHealth;
    }

    [PunRPC]
    // Takes health from the player if they do not have invulnerability
    public void TakeDamage(int dmg)
    {
        if ((playerHealth - dmg) <= 0)
        {
            Debug.Log("Player has died.");
            isAlive = false;
            GameManager.manager.CheckMultiplayerState();
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
        if (collision.CompareTag("Explosion") && photonView.IsMine)
        {
            Debug.Log("Player hit by explosion!");
            GameManager.manager.CheckGameState();
            TakeDamage(1);
        }

        if (collision.CompareTag("Boost-1") && photonView.IsMine)
        {
            photonView.RPC("Boost1", RpcTarget.All);
            collision.gameObject.GetComponent<DestroyPowerup>().Destroy();
        }

        if (collision.CompareTag("Boost-2") && photonView.IsMine)
        {
            photonView.RPC("Boost2", RpcTarget.All);
            collision.gameObject.GetComponent<DestroyPowerup>().Destroy();
        }

        if (collision.CompareTag("Boost-3") && photonView.IsMine)
        {
            photonView.RPC("Boost3", RpcTarget.All);
            collision.gameObject.GetComponent<DestroyPowerup>().Destroy();
        }
    }

   [PunRPC]
    private void Boost1()
    {
        // Lower cd of bomb placement for 5 seconds
        Debug.Log("Bomb cd boost activated");
        StartCoroutine(BombCdBoost(5));
    }

    [PunRPC]
    private void Boost2()
    {
        // Make player invulnerable for 5 seconds
        vulnerabilityCDCounter = 5.0f;
        isInvulnerableCD = true;
    }

    [PunRPC]
    private void Boost3()
    {
        // Set explosion distance to 5
        Debug.Log("Explosion distance boost activated");
        StartCoroutine(BombDistanceBoost(5));
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
}
