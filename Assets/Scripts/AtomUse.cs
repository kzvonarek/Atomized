using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AtomUse : MonoBehaviour
{
    // helium functionality
    private FuelManager fuelBarScript;
    [SerializeField] GameObject playerObj;
    private List<GameObject> playerAtomList;

    // script from PlayerBonding.cs
    private PlayerBonding pBscript;


    void Start()
    {
        // helium functionality
        fuelBarScript = GameObject.FindWithTag("Fuel Manager").GetComponent<FuelManager>();

        // get atom List (currAtoms) from player
        playerAtomList = playerObj.GetComponent<PlayerBonding>().currAtoms;

        pBscript = playerObj.GetComponent<PlayerBonding>();
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

        // check if there is still more helium atoms in list then...
        heliumAtom = playerAtomList.Find(atom => atom.name == "Helium Atom");

        // ...hide helium atom icon when none are left
        if (heliumAtom == null)
        {
            pBscript.heliumIcon.SetActive(false);
        }
    }
}
