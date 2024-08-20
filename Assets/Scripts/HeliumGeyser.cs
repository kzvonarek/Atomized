using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliumGeyser : MonoBehaviour
{
    // fuel functionality
    private bool isInGeyser = false;
    private FuelManager fuelBarScript;

    void Start()
    {
        fuelBarScript = GameObject.FindWithTag("Fuel Manager").GetComponent<FuelManager>();
    }

    void Update()
    {
        if (isInGeyser)
        {
            fuelBarScript.HeliumCollected(0.05f);
        }
    }

    // if player enters range of geyser, allow refueling
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInGeyser = true;
        }
    }

    // if player exits range of geyser, do not allow for refueling
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInGeyser = false;
        }
    }
}
