using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;
    public static bool isPaused;
    public void Resume()
    {
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        AudioListener.pause = false;
        isPaused = false;
    }

    public void Pause()
    {

        {
            pauseButton.SetActive(false);
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            AudioListener.pause = true;
            isPaused = true;
        }
        
       
    }

}
