using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 2.0f;

    private BoxCollider2D boxCollider2D;
    public float rayLength = 1.0f;

    private float turnTime;
    public float startTurnTime = 0.5f;

    public Transform[] sides;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        checkDirections();

    }

    private void checkDirections()
    {
        // Checks if enemy is at an intersection
        Vector2 horizontalDir = transform.TransformDirection(Vector2.right) * 1f;
        Vector2 verticalDir = transform.TransformDirection(Vector2.up) * 1f;

        RaycastHit2D rayRight = Physics2D.Raycast(sides[0].transform.position, horizontalDir, 2, LayerMask.GetMask("Wall"));
        RaycastHit2D rayLeft = Physics2D.Raycast(sides[1].transform.position, -horizontalDir, 2, LayerMask.GetMask("Wall"));
        RaycastHit2D rayUp = Physics2D.Raycast(sides[2].transform.position, verticalDir, 2, LayerMask.GetMask("Wall"));
        RaycastHit2D rayDown = Physics2D.Raycast(sides[3].transform.position, -verticalDir, 2, LayerMask.GetMask("Wall"));

        Debug.DrawRay(sides[0].transform.position, horizontalDir, Color.red);
        Debug.DrawRay(sides[1].transform.position, -horizontalDir, Color.red);
        Debug.DrawRay(sides[2].transform.position, verticalDir, Color.red);
        Debug.DrawRay(sides[3].transform.position, -verticalDir, Color.red);

        if (turnTime <= 0)
        {
            if (!(rayRight && rayLeft && rayUp && rayDown))
            {
                ArrayList viableDirections = new ArrayList();

                Debug.Log("Intersection reached.");
                if(!rayRight)
                {
                    viableDirections.Add(rayRight);
                }
                if (!rayLeft)
                {
                    viableDirections.Add(rayLeft);
                }
                if (!rayUp)
                {
                    viableDirections.Add(rayUp);
                }
                if (!rayDown)
                {
                    viableDirections.Add(rayDown);
                }
                // Randomize direction
                turnTime = startTurnTime;
            }
        }
        else
        {
            turnTime -= Time.deltaTime;
        }
    }

    private void randomizeDirection(ArrayList directions)
    {
        foreach(RaycastHit2D rayHit2D in directions) {

        }
    }
}
