using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public Slider fuelBar;
    private float fuelMovingDecrease = 0.5f;

    // N2O (Nitrous Oxide) functionality, fuel bar sprites
    [SerializeField] Image fuelBarSprite;
    [SerializeField] Sprite[] fuelBarSpriteList;

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

        // set fuel bar to corresponding sprite based on amount
        if (fuelBar.value < 1)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[0];
        }
        if (fuelBar.value < 2 && fuelBar.value >= 1)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[1];
        }
        if (fuelBar.value < 3 && fuelBar.value >= 2) // example fuel between 2-3 (not inclusive)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[2]; // so fuel bar is filled by 2
        }
        if (fuelBar.value < 4 && fuelBar.value >= 3)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[3];
        }
        if (fuelBar.value < 5 && fuelBar.value >= 4)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[4];
        }
        if (fuelBar.value < 6 && fuelBar.value >= 5)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[5];
        }
        if (fuelBar.value < 7 && fuelBar.value >= 6)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[6];
        }
        if (fuelBar.value < 8 && fuelBar.value >= 7)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[7];
        }
        if (fuelBar.value < 9 && fuelBar.value >= 8)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[8];
        }
        if (fuelBar.value < 10 && fuelBar.value >= 9)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[9];
        }
        if (fuelBar.value == 10)
        {
            fuelBarSprite.sprite = fuelBarSpriteList[10];
        }

    }

    // increase amount of curr. fuel
    public void HeliumCollected(float fuelHeIncrease)
    {
        fuelBar.value += fuelHeIncrease;
    }
}
