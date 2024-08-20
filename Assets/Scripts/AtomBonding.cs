using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomBonding : MonoBehaviour
{
    private Rigidbody2D rb;
    private float distanceToPlayer;
    private Transform playerPos;
    private GameObject playerObj;

    // script from PlayerBonding.cs
    private PlayerBonding pBscript;

    // change(s) depending on size/type of atom
    [SerializeField] float atomSpeed;
    [SerializeField] float minDistance;

    // true if player/atom is bonded, false is player is not bonded with atom
    public bool atomBonded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform;
        pBscript = playerObj.GetComponent<PlayerBonding>();
    }

    void Update()
    {
        // change color of atom to signify it is bonded or not with player
        if (atomBonded) // bonded = green
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0, 255, 0, 255);
        }
        else // not bonded = deafult
        {
            this.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }

        // find distance to player from atom
        distanceToPlayer = Vector2.Distance(this.transform.position, playerPos.position);

        // atom stops following when too close (moves to current position constantly)
        if (atomBonded && distanceToPlayer < minDistance)
        {
            rb.MovePosition(transform.position);
        }

        // atom follows player when bonded
        if (atomBonded && distanceToPlayer > minDistance)
        {
            Vector2 direction = playerPos.position - transform.position;
            Vector2 newPosition = rb.position + direction * atomSpeed * Time.deltaTime;

            rb.MovePosition(newPosition);
        }

        // break all bonds when B key is pressed and there is a current bond, freeze position of atom(s)
        if (Input.GetKeyDown(KeyCode.B) && atomBonded)
        {
            atomBonded = false;

            // clear list of atoms (inventory)
            pBscript.currAtoms.Clear();

            // default camera zoom back to 4.0
            pBscript.mainCamera.m_Lens.OrthographicSize = 4f;

            // set player speed back to default (12f)
            playerObj.GetComponent<PlayerMovement>().speed = 12f;

            // set atom counts back to default (0)
            pBscript.totalOxygen = 0;
            pBscript.totalHydrogen = 0;
            pBscript.totalHelium = 0;
            pBscript.totalNitrogen = 0;
            pBscript.totalCarbon = 0;
        }
    }
}