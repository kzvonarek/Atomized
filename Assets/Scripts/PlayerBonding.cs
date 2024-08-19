using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class PlayerBonding : MonoBehaviour
{
    private bool isInTrigger;
    private bool canBond;
    public List<GameObject> currAtoms = new List<GameObject>();
    public CinemachineVirtualCamera mainCamera;

    // add more if needed for levels with more than one of each element
    public GameObject hydrogenIcon;
    public GameObject heliumIcon;
    public GameObject carbonIcon;
    public GameObject nitrogenIcon;
    public GameObject oTwoIcon;

    // keep count of number of specific atoms
    public int totalOxygen = 0;

    void Update()
    {
        // if player is in range, and presses B, player initiates bond with atom
        if (isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                canBond = true;
            }
        }

        // check if player has TWO or more oxygen atoms, if so set O2 icon to visible
        if (totalOxygen >= 2)
        {
            oTwoIcon.SetActive(true);
        }
    }

    // if player enters range of atom, allow bonding
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Atom"))
        {
            isInTrigger = true;
        }
    }

    // if player exits range of atom, do not allow for bonding
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
            // let atom game object know that a bond occured
            other.gameObject.GetComponent<AtomBonding>().atomBonded = true;

            // add collected atom to a list of held atom(s)
            currAtoms.Add(other.gameObject);

            /*
            - depending on element (name) of atom, [temp] activate an icon of it on UI (different for specific atoms)
            - zoom out camera when picking up an atom
            - decrease speed when picking up atom
            - the bigger the atom, the larger the camera scales out and the more speed is decreased
            - for helium, increase current fuel value
            */
            switch (other.gameObject.name)
            {
                case "Hydrogen Atom":
                    hydrogenIcon.SetActive(true); // display icon in UI
                    bondFunction(2f, 0.3f);
                    break;
                case "Helium Atom":
                    heliumIcon.SetActive(true);
                    bondFunction(1f, 0.1f);
                    break;
                case "Carbon Atom":
                    carbonIcon.SetActive(true);
                    bondFunction(5f, 1f);
                    break;
                case "Nitrogen Atom":
                    nitrogenIcon.SetActive(true);
                    bondFunction(4f, 0.7f);
                    break;
                case "Oxygen Atom":
                    totalOxygen += 1;
                    bondFunction(3f, 0.5f);
                    break;
            }

            // prevent speed from being 0 or less (game is unplayable)
            if (GetComponent<PlayerMovement>().speed < 0.1f)
            {
                GetComponent<PlayerMovement>().speed = 0.1f;
            }
        }
    }

    void bondFunction(float fovIncrease, float speedDecrease)
    {
        mainCamera.m_Lens.OrthographicSize += fovIncrease; // increase camera FOV
        GetComponent<PlayerMovement>().speed -= speedDecrease; // decrease speed
    }
}
