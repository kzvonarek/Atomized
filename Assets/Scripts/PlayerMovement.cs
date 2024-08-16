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

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // movement code
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // left or right
        rb.velocity = new Vector2(rb.velocity.x, vertical * speed); // up or down
    }
}
