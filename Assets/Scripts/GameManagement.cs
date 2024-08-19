using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float timeElapsed;

    void Update()
    {
        // timer that showcases hours, minutes, seconds, and milliseconds alive
        timeElapsed += Time.deltaTime;
        int hours = Mathf.FloorToInt(timeElapsed / 3600);
        int minutes = Mathf.FloorToInt(timeElapsed % 3600 / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        int milliseconds = Mathf.FloorToInt(timeElapsed * 100 % 100);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds);

        // score?
    }
}
