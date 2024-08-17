using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBonding : MonoBehaviour
{
    private bool isInTrigger;
    private bool canBond;
    [SerializeField] public List<GameObject> currAtoms = new List<GameObject>();

    // add more if needed for levels with more than one of each element
    public GameObject hydrogenIcon;
    public GameObject heliumIcon;
    public GameObject carbonIcon;
    public GameObject nitrogenIcon;
    public GameObject oxygenIcon;

    void Update()
    {
        if (isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                canBond = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Atom"))
        {
            isInTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Atom"))
        {
            isInTrigger = false;
            canBond = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (canBond && other.gameObject.GetComponent<AtomBonding>().atomBonded == false)
        {
            // let atom game object know bonded occured
            other.gameObject.GetComponent<AtomBonding>().atomBonded = true;

            // add collected atom to List of held atom(s)
            currAtoms.Add(other.gameObject);

            // depending on element (name) of atom, activate an icon of it on UI
            switch (other.gameObject.name)
            {
                case "Hydrogen Atom":
                    hydrogenIcon.SetActive(true);
                    break;
                case "Helium Atom":
                    heliumIcon.SetActive(true);
                    break;
                case "Carbon Atom":
                    carbonIcon.SetActive(true);
                    break;
                case "Nitrogen Atom":
                    nitrogenIcon.SetActive(true);
                    break;
                case "Oxygen Atom":
                    oxygenIcon.SetActive(true);
                    break;
            }
        }
    }

    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     Debug.Log("hello");
    //     // form bond(s) on contact with atom and when B key is pressed
    //     if (other.gameObject.CompareTag("Atom"))
    //     {
    //         isInTrigger = true;
    //         if (Input.GetKeyDown(KeyCode.B) && other.gameObject.GetComponent<AtomBonding>().atomBonded == false)
    //         {
    //             // let atom game object know bonded occured
    //             other.gameObject.GetComponent<AtomBonding>().atomBonded = true;

    //             // add collected atom to List of held atom(s)
    //             currAtoms.Add(other.gameObject);

    //             // depending on element (name) of atom, activate an icon of it on UI
    //             switch (other.gameObject.name)
    //             {
    //                 case "Hydrogen Atom":
    //                     hydrogenIcon.SetActive(true);
    //                     break;
    //                 case "Helium Atom":
    //                     heliumIcon.SetActive(true);
    //                     break;
    //                 case "Carbon Atom":
    //                     carbonIcon.SetActive(true);
    //                     break;
    //                 case "Nitrogen Atom":
    //                     nitrogenIcon.SetActive(true);
    //                     break;
    //                 case "Oxygen Atom":
    //                     oxygenIcon.SetActive(true);
    //                     break;
    //             }
    //         }
    //     }
    // }
}
