using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime);
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
