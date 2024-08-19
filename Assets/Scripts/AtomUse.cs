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

    // H2O (water) platform functionality
    [SerializeField] GameObject h2oPlatform;
    [SerializeField] GameObject h2oPlatformPoint;

    // script(s) from Player
    private PlayerBonding pBscript;
    private PlayerMovement pMscript;


    void Start()
    {
        // helium functionality
        fuelBarScript = GameObject.FindWithTag("Fuel Manager").GetComponent<FuelManager>();

        // get atom List (currAtoms) from player
        playerAtomList = playerObj.GetComponent<PlayerBonding>().currAtoms;

        // get script from PlayerBonding.cs, to de/ac-tivate icons
        pBscript = playerObj.GetComponent<PlayerBonding>();

        // get script from PlayerMovement.cs, to de/ac-tivate icons
        pMscript = playerObj.GetComponent<PlayerMovement>();
    }

    // destroy helium atom/icon, and increase fuel by set amount
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

        // check if there is still more helium atom(s) in list then...
        heliumAtom = playerAtomList.Find(atom => atom.name == "Helium Atom");

        // ...hide helium atom icon when none are left
        if (heliumAtom == null)
        {
            pBscript.heliumIcon.SetActive(false);
        }
    }

    // destroy two oxygen atoms, and increase speed by set amount for 5 seconds
    public void O2ButtonClicked()
    {
        // find 2 oxygen atoms in List/collected atoms and destroy them/remove from List (currAtoms)
        destroyOxygenAtom();
        destroyOxygenAtom();
        pBscript.totalOxygen -= 2; // decrease count of total held oxygen atoms

        // hide O2 atom icon when there are less than two oxygen atoms present
        if (pBscript.totalOxygen < 2)
        {
            pBscript.oTwoIcon.SetActive(false);
        }

        // increase speed by 5f for 5 seconds
        pMscript.speed += 5f;
        StartCoroutine(fiveSecondTimer());

        // need to add speed back, which was lost from inititally holding two oxygen atoms
    }

    IEnumerator fiveSecondTimer()
    {
        yield return new WaitForSeconds(5);

        // decrease speed by 5f after 5 seconds
        pMscript.speed -= 5f;
    }

    void destroyOxygenAtom()
    {
        GameObject oxygenAtom = playerAtomList.Find(atom => atom.name == "Oxygen Atom");

        if (oxygenAtom != null)
        {
            playerAtomList.Remove(oxygenAtom);
            Destroy(oxygenAtom);
        }
    }

    // H2O (water) platform creation
    public void h2oButtonClicked()
    {
        // spawn in water platform at platform spawn point
        Instantiate(h2oPlatform, h2oPlatformPoint.transform.position, Quaternion.identity);
    }
}
