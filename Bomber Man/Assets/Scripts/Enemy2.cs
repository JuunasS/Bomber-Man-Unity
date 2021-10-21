using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 2.0f;

    private BoxCollider2D boxCollider2D;
    public float rayLength = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.Raycast(transform.position, Vector2.right, 1.0f, LayerMask.GetMask("Wall"));

        Vector3 checkPos = transform.position + new Vector3(0.5f, 0, 0);

        Debug.DrawLine(boxCollider2D.bounds.center, checkPos, Color.red);  

        //transform.Translate(Vector2.right * Time.deltaTime);
    }

    private void checkDirections()
    {
        // Check directions for walls, return list of possible directions
    }

    private void randomizeDirection(Transform[] directions)
    {
        // Randomizes direction to move to
    }
}
