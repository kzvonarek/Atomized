using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomUse : MonoBehaviour
{
    // helium functionality
    private FuelManager fuelBarScript;

    void Start()
    {
        // helium functionality
        fuelBarScript = GameObject.FindWithTag("Fuel Manager").GetComponent<FuelManager>();
    }

    // destroy helium atom, and increase fuel by set amount
    public void HeliumButtonClicked()
    {
        // add fuel to fuel bar
        fuelBarScript.HeliumCollected(1f);
    }
}
