using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject scoreAndButtons;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemiesDestroyedText;
    public List<GameObject> hordes;
    
    private AudioSource audioSource;
    
   
    [SerializeField]private float timeToSpawnHordes;

    private int score;
    private int enemiesCount;
    public static bool gameOver;
    public static bool isActive;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
       
    }
    private void FixedUpdate()
    {
        
    }
    IEnumerator SpawnHordes()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(timeToSpawnHordes);
            int index = Random.Range(0, hordes.Count);
            Instantiate(hordes[index]);
        }
       
    }

    public void StartGame(int dificculty)
    {
        isActive = true;
        gameOver = false;
        timeToSpawnHordes /= dificculty;
        titleScreen.SetActive(false);
        audioSource.Play();
        StartCoroutine(SpawnHordes());

        //when Mobile add the buttons firebutton and joystick in prefabs into the canvas 
        scoreAndButtons.SetActive(true);
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
    public void UpdateEnemies(int enemiesToAdd)
    {
        enemiesCount += enemiesToAdd;
        enemiesDestroyedText.text = "Enemies: " + enemiesCount;
    }
    
}
