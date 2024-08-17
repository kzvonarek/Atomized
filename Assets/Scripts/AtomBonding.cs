using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS.Xcode;
using UnityEngine;

public class AtomBonding : MonoBehaviour
{
    private float distanceToPlayer;
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject playerObj;

    // change(s) depending on size/type of atom
    [SerializeField] float atomSpeed;
    [SerializeField] float minDistance;

    // true if player/atom is bonded, false is player is not bonded with atom
    public bool atomBonded;

    void Update()
    {
        // find distance to player from atom
        distanceToPlayer = Vector2.Distance(this.transform.position, playerPos.position);

        // atom follows player hydrogen when bonded, temporarily stops following when too close
        if (atomBonded && distanceToPlayer > minDistance)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, playerPos.position, atomSpeed * Time.deltaTime);
        }

        // break bond when N key is pressed and there is a current bond
        if (Input.GetKeyDown(KeyCode.N) && atomBonded)
        {
            atomBonded = false;
        }
    }
}