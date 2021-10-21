using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailEnemy : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D rb2d;

    public float stopDuration;

    public Transform[] wayPoints;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }
}
