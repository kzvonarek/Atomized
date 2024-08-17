using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBonding : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        // form bonds on contact with Atom and B key is pressed
        if (other.gameObject.CompareTag("Atom") && Input.GetKeyDown(KeyCode.B))
        {
            // let Atom game object know bonded occured
            other.gameObject.GetComponent<AtomBonding>().atomBonded = true;
        }
    }
}
