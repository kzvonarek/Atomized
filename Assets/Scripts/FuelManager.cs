using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Slider fuelBar;
    private float fuelMovingDecrease = 1f;

    void Update()
    {
        // decrease fuel by set amount when UFO is moving (while player is inputting)
        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0f && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            fuelBar.value -= fuelMovingDecrease * Time.deltaTime;
        }

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
