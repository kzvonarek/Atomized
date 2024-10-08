using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliumGeyser : MonoBehaviour
{
    // fuel functionality
    private bool isInGeyser = false;
    private FuelManager fuelBarScript;

    // audio
    private AudioSource geyserAudioSource;
    [SerializeField] AudioClip geyserSFX;

    void Start()
    {
        fuelBarScript = GameObject.FindWithTag("Fuel Manager").GetComponent<FuelManager>();
        geyserAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isInGeyser)
        {
            fuelBarScript.HeliumCollected(2f * Time.deltaTime);
        }
    }

    // if player enters range of geyser, allow refueling
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            geyserAudioSource.PlayOneShot(geyserSFX);
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
