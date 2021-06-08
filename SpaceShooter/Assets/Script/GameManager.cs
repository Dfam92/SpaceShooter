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
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI enemiesDestroyedText;
    
    public List<GameObject> hordes;
    public List<GameObject> powerUps;
    public GameObject boss;
    
    private AudioSource audioSource;
    
    
    [SerializeField]private float timeToSpawnHordes;
    [SerializeField]private float timeToSpawnPowerUps;
    [SerializeField]private int bossRate;

    private int score;
    private int enemiesCount;
    private float bossRemainder;

    public bool bossDefeated;
    public bool bossOn = false;
    public static bool gameOver;
    public static bool isActive;
    

    


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        highScore.text = "HiScore: " + PlayerPrefs.GetInt("HighScore",0).ToString();
        
    }
  

    private void Update()
    {
        BossDefeated();
        SpawnBoss();
        bossRemainder = enemiesCount % bossRate;
        
        
    }
    IEnumerator SpawnHordes()
    {
        while (isActive && bossOn == false)
        {
            yield return new WaitForSeconds(timeToSpawnHordes);
            int index = Random.Range(0, hordes.Count);
            Instantiate(hordes[index]);
        }
       
    }
    IEnumerator SpawnRatePowerUps()
    {
        while (isActive && bossOn == false)
        {
            yield return new WaitForSeconds(timeToSpawnPowerUps);
            int index = Random.Range(0, powerUps.Count);
            Instantiate(powerUps[index]);
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
        StartCoroutine(SpawnRatePowerUps());
        

        //when Mobile add the buttons firebutton and joystick in prefabs into the canvas 
        scoreAndButtons.SetActive(true);
    }

    public void GameOver()
    {
        PlayerControl.isMultiplying2x = false;
        PlayerControl.isMultiplying4x = false;
        PlayerControl.sideBullets = false;
        gameOver = true;
        gameOverScreen.SetActive(true);
        isActive = false;
        audioSource.Stop();
        HighScore();

    }

    public void SpawnBoss()
    {
        if(enemiesCount > 2 && bossRemainder == 0 && bossOn == false)
        {
            bossOn = true;
            Instantiate(boss);
            
        }
    
    }

    public void BossDefeated()
    {
      if (bossDefeated == true)
        {
            bossOn = false;
            bossDefeated = false;

        }
        
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

    private void HighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore.text = "HiScore: 0";
    }
    
}
