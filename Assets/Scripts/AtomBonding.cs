using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class AtomBonding : MonoBehaviour
{
    private Rigidbody2D rb;
    private float distanceToPlayer;
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject playerObj;

    // change(s) depending on size/type of atom
    [SerializeField] float atomSpeed;
    [SerializeField] float minDistance;

    // true if player/atom is bonded, false is player is not bonded with atom
    public bool atomBonded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // find distance to player from atom
        distanceToPlayer = Vector2.Distance(this.transform.position, playerPos.position);

        if (atomBonded && distanceToPlayer < minDistance)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // atom follows player hydrogen when bonded, temporarily stops following when too close
        else if (atomBonded && distanceToPlayer > minDistance)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            Vector2 direction = playerPos.position - transform.position;
            Vector2 newPosition = rb.position + direction * atomSpeed * Time.deltaTime;

            rb.MovePosition(newPosition);
        }

        // break bond when N key is pressed and there is a current bond, freeze pos. of atom(s)
        if (Input.GetKeyDown(KeyCode.N) && atomBonded)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            atomBonded = false;
        }
    }
}