using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Slider fuelBar;
    private float fuelMovingDecrease = 1f;
    private float fuelIdleDecrease = 0.05f;
    private float fuelPoisioningRate = 0.5f;

    void Update()
    {
        // decrease fuel by set amount when UFO is moving
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0f)
        {
            fuelBar.value -= fuelMovingDecrease * Time.deltaTime;
        }
        // decrease fuel by set amount when UFO is idle

        // if (player.GetComponent<Rigidbody2D>().velocity.magnitude == 0f)
        // {
        //     fuelBar.value -= fuelIdleDecrease * Time.deltaTime;
        // }

        // when fuel value reaches 0, only allow horizontal movement
        if (fuelBar.value <= 0f)
        {
            player.GetComponent<PlayerMovement>().sufficientFuel = false;
        }
        else
        {
            player.GetComponent<PlayerMovement>().sufficientFuel = true;
        }
    }

    // increase amount of curr. fuel
    public void HeliumCollected(float fuelHeIncrease)
    {
        fuelBar.value += fuelHeIncrease;
    }
}
