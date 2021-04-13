using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isActive;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    private AudioSource audioSource;
    private int score;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void StartGame()
    {
        isActive = true;
        gameOver = false;
        titleScreen.SetActive(false);
        
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        isActive = false;
        audioSource.Stop();
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
