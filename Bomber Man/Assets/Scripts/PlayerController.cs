using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody2D rb2D;

    Vector2 movement;

    public Tilemap tilemap;
    public GameObject bombPrefab;

    public KeyCode bombKey;

    public float bombCooldown = 2.0f;
    private float bombCooldownCounter = 0;
    public bool isCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(isCooldown)
        {
            bombCooldownCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(bombKey) && bombCooldownCounter <= 0)
        {
            Vector3Int cell = tilemap.WorldToCell(gameObject.transform.position);
            Vector3 cellCenterpos = tilemap.GetCellCenterWorld(cell);

            Instantiate(bombPrefab, cellCenterpos, Quaternion.identity);
            bombCooldownCounter = bombCooldown; 
            isCooldown = true;
            
        }

    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * speed * Time.deltaTime);
    }
}
