using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 2.0f;

    private BoxCollider2D boxCollider2D;
    public float rayLength = 1.0f;

    public Vector2 lastDirection;
    Vector2 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        lastDirection = (Vector2)transform.position;
        newPosition = (Vector2)transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        // Check directions after reaching new position
        if (Vector2.Distance(transform.position, newPosition) < 0.01f)
        {
            ArrayList viableDirections = checkDirections();
            Vector2 direction = randomizeDirection(viableDirections, lastDirection);
            lastDirection = (Vector2)transform.position;

            newPosition = (Vector2)transform.position + direction;
            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }
    }

    // Checks directions and retuns and array of viable directions (Vector2D) 
    private ArrayList checkDirections()
    {
        // Directions
        Vector2 horizontalDir = transform.TransformDirection(Vector2.right) * 1f;
        Vector2 verticalDir = transform.TransformDirection(Vector2.up) * 1f;

        // Check directions (Right, Left, Up, Down)
        RaycastHit2D rayRight = Physics2D.Raycast(boxCollider2D.bounds.center, horizontalDir, 1, LayerMask.GetMask("Wall"));
        RaycastHit2D rayLeft = Physics2D.Raycast(boxCollider2D.bounds.center, -horizontalDir, 1, LayerMask.GetMask("Wall"));
        RaycastHit2D rayUp = Physics2D.Raycast(boxCollider2D.bounds.center, verticalDir, 1, LayerMask.GetMask("Wall"));
        RaycastHit2D rayDown = Physics2D.Raycast(boxCollider2D.bounds.center, -verticalDir, 1, LayerMask.GetMask("Wall"));

        // Add viable directions into an ArrayList and return it
        ArrayList viableDirections = new ArrayList();

        Debug.Log("Checking directions.");
        if (!rayRight)
        {
            viableDirections.Add(new Vector2(1, 0));
            //Debug.Log("Can move to the right");
        }
        if (!rayLeft)
        {
            viableDirections.Add(new Vector2(-1, 0));
            //Debug.Log("Can move to the left");
        }
        if (!rayUp)
        {
            viableDirections.Add(new Vector2(0, 1));
            //Debug.Log("Can move up");
        }
        if (!rayDown)
        {
            viableDirections.Add(new Vector2(0, -1));
            //Debug.Log("Can move down");
        }

        return viableDirections;
    }


    private Vector2 randomizeDirection(ArrayList directions, Vector2 lastDirection)
    {
        /*
        if (directions.Contains(lastDirection))
        {
            directions.Remove(lastDirection);
        } 
        */

        int dirIndex = Random.Range(0, directions.Count - 1);

        return (Vector2)directions[dirIndex];
    }
}


