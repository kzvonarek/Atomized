using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
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
        // find distance to player from atom
        distanceToPlayer = Vector2.Distance(this.transform.position, playerPos.position);

        // temporarily stops following when too close
        if (atomBonded && distanceToPlayer < minDistance)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        // atom follows player hydrogen when bonded
        else if (atomBonded && distanceToPlayer > minDistance)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            Vector2 direction = playerPos.position - transform.position;
            Vector2 newPosition = rb.position + direction * atomSpeed * Time.deltaTime;

            rb.MovePosition(newPosition);
        }

        // break bond when N key is pressed and there is a current bond, freeze position of atom(s)
        if (Input.GetKeyDown(KeyCode.N) && atomBonded)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            atomBonded = false;

            // clear list of atoms (inventory)
            pBscript.currAtoms.Clear();

            // default camera zoom back to 4.0
            pBscript.mainCamera.m_Lens.OrthographicSize = 4f;

            // deactivate the UI icons of elements
            pBscript.hydrogenIcon.SetActive(false);
            pBscript.heliumIcon.SetActive(false);
            pBscript.carbonIcon.SetActive(false);
            pBscript.nitrogenIcon.SetActive(false);
            pBscript.oxygenIcon.SetActive(false);

            // set player speed back to default (8f)
            playerObj.GetComponent<PlayerMovement>().speed = 8f;
        }
    }
}