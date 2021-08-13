using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
 
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        AudioListener.pause = false;
        
    }

    public void Pause()
    {
        if(!GameManager.gameOver)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            AudioListener.pause = true;
        }
       
    }

}
