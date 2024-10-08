using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomUse : MonoBehaviour
{
    // helium functionality
    private FuelManager fuelBarScript;
    [SerializeField] GameObject playerObj;
    private List<GameObject> playerAtomList;

    // H2O (water) platform functionality
    [SerializeField] GameObject h2oPlatform;
    [SerializeField] GameObject h2oPlatformPoint;

    // CH4 (Methane) functionality
    [SerializeField] float launchForce;

    // N2O (Nitrous Oxide) functionality, fuel bar sprites
    [SerializeField] Image fuelBarSprite;
    [SerializeField] Sprite[] fuelBarSpriteList;


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

    void Update()
    {
        // hide helium atom icon when there are less than 1 helium atom present
        if (pBscript.totalHelium < 1)
        {
            pBscript.heliumIcon.SetActive(false);
        }
        // hide O2 compound icon when there are less than 2 oxygen atoms present
        if (pBscript.totalOxygen < 2)
        {
            pBscript.oTwoIcon.SetActive(false);
        }

        // hide H2O compound icon when there are less than 1 oxygen/2 hydrogen atoms present
        if (pBscript.totalOxygen < 1 || pBscript.totalHydrogen < 2)
        {
            pBscript.h2oIcon.SetActive(false);
        }

        // hide CH4 compound icon when there are less than 1 carbon/4 hydrogen atoms present
        if (pBscript.totalCarbon < 1 || pBscript.totalHydrogen < 4)
        {
            pBscript.cHFourIcon.SetActive(false);
        }

        // hide N2O compound icon when there are less than 2 nitrogen/1 oxygen atoms present
        if (pBscript.totalNitrogen < 2 || pBscript.totalOxygen < 1)
        {
            pBscript.n2oIcon.SetActive(false);
        }
    }

    // destroy helium atom/icon, and increase fuel by set amount
    public void HeliumButtonClicked()
    {
        // add fuel to fuel bar
        fuelBarScript.HeliumCollected(1f); // test for optimal/fair fuel increase

        destroyHeliumAtom();
        pBscript.totalHelium -= 1; // lower helium count
        pBscript.bondFunction(-1, -0.1f); // decrease FOV by 1, increase speed by 0.1f
    }

    void destroyHeliumAtom()
    {
        // find a helium atom in List/collected atoms and destroy it/remove from List (currAtoms)
        GameObject heliumAtom = playerAtomList.Find(atom => atom.name == "Helium Atom");

        if (heliumAtom != null)
        {
            playerAtomList.Remove(heliumAtom);
            Destroy(heliumAtom);
        }
    }

    // destroy two oxygen atoms, and increase speed by set amount for 5 seconds
    public void O2ButtonClicked()
    {
        // find 2 oxygen atoms in List/collected atoms and destroy them/remove from List (currAtoms)
        destroyOxygenAtom();
        destroyOxygenAtom();
        pBscript.totalOxygen -= 2; // decrease count of total held oxygen atoms

        // increase speed by 5f for 5 seconds
        pMscript.speed += 5f;
        StartCoroutine(fiveSecondTimerO2());
        pBscript.bondFunction(-6, -1f); // decrease FOV by 6, increase speed by 1f
    }

    IEnumerator fiveSecondTimerO2()
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

        // find 1 oxygen atom, and 2 hyrdogen atoms in List/collected atoms and destroy them/remove from List (currAtoms)
        destroyOxygenAtom();
        destroyHydrogenAtom();
        destroyHydrogenAtom();
        pBscript.totalOxygen -= 1; // decrease count of total held oxygen atoms
        pBscript.totalHydrogen -= 2; // decrease count of total held hydrogen atoms
        pBscript.bondFunction(-7, -1.1f); // decrease FOV by 7, increase speed by 1.1f
    }

    void destroyHydrogenAtom()
    {
        GameObject hydrogenAtom = playerAtomList.Find(atom => atom.name == "Hydrogen Atom");

        if (hydrogenAtom != null)
        {
            playerAtomList.Remove(hydrogenAtom);
            Destroy(hydrogenAtom);
        }
    }

    public void cH4ButtonClicked()
    {
        // launch player away, and break all bonds
        playerObj.GetComponent<Rigidbody2D>().AddForce(Vector2.up.normalized * launchForce);

        // find 1 carbon atom, and 4 hydrogen atoms in List/collected atoms and destroy them/remove from List (currAtoms)
        destroyCarbonAtom();
        destroyHydrogenAtom();
        destroyHydrogenAtom();
        destroyHydrogenAtom();
        destroyHydrogenAtom();
        pBscript.totalCarbon -= 1; // decrease count of total held carbon atoms
        pBscript.totalHydrogen -= 4; // decrease count of total held hydrogen atoms
        pBscript.bondFunction(-13, -2.2f); // decrease FOV by 13, increase speed by 2.2f
    }

    void destroyCarbonAtom()
    {
        GameObject carbonAtom = playerAtomList.Find(atom => atom.name == "Carbon Atom");

        if (carbonAtom != null)
        {
            playerAtomList.Remove(carbonAtom);
            Destroy(carbonAtom);
        }
    }

    public void n2oButtonClicked()
    {

        // find 2 nitrogen atom and 1 oxygen atom in List/collected atoms and destroy them/remove from List (currAtoms)
        destroyNitrogenAtom();
        destroyNitrogenAtom();
        destroyOxygenAtom();
        pBscript.totalNitrogen -= 2; // decrease count of total held nitrogen atoms
        pBscript.totalOxygen -= 1; // decrease count of total held oxygen atoms
        pBscript.bondFunction(-11, -1.9f); // decrease FOV by 11, increase speed by 1.9f

        // max fuel for 5 seconds
        fuelBarScript.fuelBar.maxValue = 100;
        fuelBarScript.fuelBar.value = 100;

        // change fuel bar color to signify nitrous oxide boost, using bool
        fuelBarSprite.sprite = fuelBarSpriteList[1];

        StartCoroutine(fiveSecondTimerNO());
    }

    IEnumerator fiveSecondTimerNO()
    {
        yield return new WaitForSeconds(5);

        // decrease fuel back to normal after 5 seconds
        fuelBarScript.fuelBar.maxValue = 10;
        fuelBarScript.fuelBar.value = 10;

        // change fuel bar color back to normal, using bool
        fuelBarSprite.sprite = fuelBarSpriteList[0];
    }

    void destroyNitrogenAtom()
    {
        GameObject nitrogenAtom = playerAtomList.Find(atom => atom.name == "Nitrogen Atom");

        if (nitrogenAtom != null)
        {
            playerAtomList.Remove(nitrogenAtom);
            Destroy(nitrogenAtom);
        }
    }
}
