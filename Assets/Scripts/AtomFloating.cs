using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomFloating : MonoBehaviour
{
    private Rigidbody2D rb;
    public float FloatStrength;
    private float randomNumber;
    private float maxVelocity = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // move atom in random direction based on range
        randomNumber = Random.Range(1, 5);
        if (randomNumber == 1)
        {
            // move player in direction multiplied by Float Strength
            rb.AddForce(Vector3.up * FloatStrength);
        }
        else if (randomNumber == 2)
        {
            rb.AddForce(Vector3.down * FloatStrength);
        }
        else if (randomNumber == 3)
        {
            rb.AddForce(Vector3.right * FloatStrength);
        }
        else if (randomNumber == 4)
        {
            rb.AddForce(Vector3.left * FloatStrength);
        }

        // if an atom is moved too fast (by colliding with player or other atom), slow it down
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }
}
