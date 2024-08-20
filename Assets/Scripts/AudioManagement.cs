using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManagement : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        AudioSource audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void menuButtonClick(AudioClip buttonSound)
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
