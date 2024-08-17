using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBonding : MonoBehaviour
{
    [SerializeField] public List<GameObject> currAtoms = new List<GameObject>();
    public GameObject hydrogenIcon;
    public GameObject heliumIcon;
    public GameObject carbonIcon;
    public GameObject nitrogenIcon;
    public GameObject oxygenIcon;

    private void OnTriggerStay2D(Collider2D other)
    {
        // form bond(s) on contact with atom and when B key is pressed
        if (other.gameObject.CompareTag("Atom") && Input.GetKeyDown(KeyCode.B) && other.gameObject.GetComponent<AtomBonding>().atomBonded == false)
        {
            // let atom game object know bonded occured
            other.gameObject.GetComponent<AtomBonding>().atomBonded = true;

            // add collected atom to List of held atom(s)
            currAtoms.Add(other.gameObject);

            // depending on element of atom, activate an icon of it on UI
            if (other.gameObject.name == "Hydrogen Atom")
            {
                hydrogenIcon.SetActive(true);
            }

            else if (other.gameObject.name == "Helium Atom")
            {
                heliumIcon.SetActive(true);
            }

            else if (other.gameObject.name == "Carbon Atom")
            {
                carbonIcon.SetActive(true);
            }

            else if (other.gameObject.name == "Nitrogen Atom")
            {
                nitrogenIcon.SetActive(true);
            }

            else if (other.gameObject.name == "Oxygen Atom")
            {
                oxygenIcon.SetActive(true);
            }
        }
    }
}
