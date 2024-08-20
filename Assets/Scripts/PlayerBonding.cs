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

    public GameObject heliumIcon;
    public GameObject h2oIcon;
    public GameObject oTwoIcon;
    public GameObject cHFourIcon;
    public GameObject n2oIcon;

    // keep count of number of specific atoms
    public int totalOxygen = 0;
    public int totalHydrogen = 0;
    public int totalHelium = 0;
    public int totalCarbon = 0;
    public int totalNitrogen = 0;

    void Update()
    {
        // if player is in range, and presses Right-Click, player initiates bond with atom
        if (isInTrigger)
        {
            if (Input.GetMouseButtonDown(1))
            {
                canBond = true;
            }
        }

        // check if player has sufficient atoms to make icon(s) visible
        if (totalOxygen >= 2)
        {
            oTwoIcon.SetActive(true);
        }

        if (totalOxygen >= 1 && totalHydrogen >= 2)
        {
            h2oIcon.SetActive(true);
        }

        if (totalHelium >= 1)
        {
            heliumIcon.SetActive(true);
        }

        if (totalCarbon >= 1 && totalHydrogen >= 4)
        {
            cHFourIcon.SetActive(true);
        }

        if (totalNitrogen >= 2 && totalOxygen >= 1)
        {
            n2oIcon.SetActive(true);
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
        if (canBond && other.gameObject != null && other.gameObject.GetComponent<AtomBonding>().atomBonded == false)
        {
            // let atom game object know that a bond occured
            other.gameObject.GetComponent<AtomBonding>().atomBonded = true;

            // add collected atom to a list of held atom(s)
            currAtoms.Add(other.gameObject);

            /* update these comments near end
            - depending on element (name) of atom, [temp] activate an icon of it on UI (different for specific atoms)
            - zoom out camera when picking up an atom
            - decrease speed when picking up atom
            - the bigger the atom, the larger the camera scales out and the more speed is decreased
            - for helium, increase current fuel value
            */
            switch (other.gameObject.name)
            {
                case "Hydrogen Atom":
                    totalHydrogen += 1;
                    bondFunction(2f, 0.3f);
                    break;
                case "Helium Atom":
                    totalHelium += 1;
                    bondFunction(1f, 0.1f);
                    break;
                case "Carbon Atom":
                    totalCarbon += 1;
                    bondFunction(5f, 1f);
                    break;
                case "Nitrogen Atom":
                    totalNitrogen += 1;
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
