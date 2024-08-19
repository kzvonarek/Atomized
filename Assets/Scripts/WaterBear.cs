using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBear : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float bearSpeed; // speed Waterbear moves at
    [SerializeField] Transform playerPos; // get position of player

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // water bear constantly follows player at a certain speed
        Vector2 direction = playerPos.position - transform.position;
        Vector2 newPosition = rb.position + direction * bearSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // add a rotation that will make sprite always face player
    }
}
