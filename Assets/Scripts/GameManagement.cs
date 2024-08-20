using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject heliumFuelBar;
    [SerializeField] GameObject HeIcon;
    [SerializeField] GameObject o2Icon;
    [SerializeField] GameObject n2oIcon;
    [SerializeField] GameObject h2oIcon;
    [SerializeField] GameObject cH4Icon;
    [SerializeField] TextMeshProUGUI timerText;
    private float timeElapsed;
    private bool paused;
    public bool dead;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        // timer that showcases hours, minutes, seconds, and milliseconds alive
        timeElapsed += Time.deltaTime;
        int hours = Mathf.FloorToInt(timeElapsed / 3600);
        int minutes = Mathf.FloorToInt(timeElapsed % 3600 / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        int milliseconds = Mathf.FloorToInt(timeElapsed * 100 % 100);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", hours, minutes, seconds, milliseconds);

        // pause game when player presses escape or presses pause button
        if (Input.GetKeyDown(KeyCode.Escape) && paused == false && dead == false)
        {
            pauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true && dead == false) // unpause game
        {
            unpauseGame();
        }

        if (dead)
        {
            death();
        }
    }

    public void pauseGame()
    {
        paused = true;
        pauseMenu.SetActive(true); // make pause menu visible
        Time.timeScale = 0;
    }

    public void unpauseGame()
    {
        paused = false;
        pauseMenu.SetActive(false); // make pause menu invisible
        Time.timeScale = 1;
    }

    // on death hide all icons/UI, and allow for Main Menu or game Restart
    public void death()
    {
        Time.timeScale = 0;

        deathMenu.SetActive(true);

        pauseButton.SetActive(false);
        heliumFuelBar.SetActive(false);
        HeIcon.SetActive(false);
        h2oIcon.SetActive(false);
        n2oIcon.SetActive(false);
        cH4Icon.SetActive(false);
        o2Icon.SetActive(false);
    }
}
