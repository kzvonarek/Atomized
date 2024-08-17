using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // movement values
    private float horizontal;
    private float vertical;
    [SerializeField] float speed;

    // fuel variable(s) (affected by FuelManager.cs)
    public bool sufficientFuel;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // movement code, allow vertical only when sufficient fuel (if player runs out of fuel, they will fall to nearest floor)
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // left or right

        if (sufficientFuel)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed); // up or down
        }
        else
        {
            rb.gravityScale = 1;
        }
    }
}
