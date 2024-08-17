using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomBonding : MonoBehaviour
{
    public bool bonded = false;
    private float distanceToPlayer;
    [SerializeField] Transform player;

    // change depending on size/type of atom
    [SerializeField] float atomSpeed;
    [SerializeField] float minDistance;


    void Update()
    {
        // find distance to player from atom
        distanceToPlayer = Vector2.Distance(this.transform.position, player.position);

        // atom follows player hydrogen when bonded, temporarily stops following when too close
        if (bonded && distanceToPlayer > minDistance)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.position, atomSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // form bonds
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.B))
        {
            bonded = true;
        }
    }
}