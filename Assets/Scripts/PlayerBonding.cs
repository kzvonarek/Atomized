using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

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
    public GameObject oxygenIcon;

    // fuel functionality
    private FuelManager fuelBarScript;

    void Start()
    {
        fuelBarScript = GameObject.FindWithTag("Fuel Manager").GetComponent<FuelManager>();
    }

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
            // let atom game object know bonded occured
            other.gameObject.GetComponent<AtomBonding>().atomBonded = true;

            // add collected atom to List of held atom(s)
            currAtoms.Add(other.gameObject);

            // depending on element (name) of atom, activate an icon of it on UI
            // zoom out camera when picking up an atom
            // decrease speed when picking up atom
            // the bigger the atom, the larger the camera scales out and the more speed is decreased
            // for helium, increase current fuel value
            switch (other.gameObject.name)
            {
                case "Hydrogen Atom":
                    hydrogenIcon.SetActive(true); // display icon in UI
                    mainCamera.m_Lens.OrthographicSize += 2f; // increase camera FOV
                    GetComponent<PlayerMovement>().speed -= 0.3f; // decrease speed
                    break;
                case "Helium Atom":
                    heliumIcon.SetActive(true);
                    mainCamera.m_Lens.OrthographicSize += 1f;
                    GetComponent<PlayerMovement>().speed -= 0.1f;

                    // add fuel to fuel bar
                    fuelBarScript.HeliumCollected(1f);
                    break;
                case "Carbon Atom":
                    carbonIcon.SetActive(true);
                    mainCamera.m_Lens.OrthographicSize += 5f;
                    GetComponent<PlayerMovement>().speed -= 1f;
                    break;
                case "Nitrogen Atom":
                    nitrogenIcon.SetActive(true);
                    mainCamera.m_Lens.OrthographicSize += 4f;
                    GetComponent<PlayerMovement>().speed -= 0.7f;
                    break;
                case "Oxygen Atom":
                    oxygenIcon.SetActive(true);
                    GetComponent<PlayerMovement>().speed -= 0.5f;
                    break;
            }

            // prevent speed from being 0 or less (game is unplayable)
            if (GetComponent<PlayerMovement>().speed < 0.1f)
            {
                GetComponent<PlayerMovement>().speed = 0.1f;
            }
        }
    }
}
