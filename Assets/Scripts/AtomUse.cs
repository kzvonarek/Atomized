using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtomUse : MonoBehaviour
{
    // helium functionality
    private FuelManager fuelBarScript;
    [SerializeField] GameObject player;
    private List<GameObject> playerAtomList;

    void Start()
    {
        // helium functionality
        fuelBarScript = GameObject.FindWithTag("Fuel Manager").GetComponent<FuelManager>();

        // get atom List (currAtoms) from player
        playerAtomList = player.GetComponent<PlayerBonding>().currAtoms;
    }

    // destroy helium atom, and increase fuel by set amount
    public void HeliumButtonClicked()
    {
        // add fuel to fuel bar
        fuelBarScript.HeliumCollected(1f);

        // find a helium atom in List/collected atoms and destroy it/remove from List (currAtoms)
        GameObject heliumAtom = playerAtomList.Find(atom => atom.name == "Helium Atom");

        if (heliumAtom != null)
        {
            playerAtomList.Remove(heliumAtom);
            Destroy(heliumAtom);
        }

        // hide helium icon (ADD THIS)
    }
}
