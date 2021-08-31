using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject scoreAndButtons;
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject pauseButton;
    public GameObject player;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScore;
    public TextMeshProUGUI enemiesDestroyedText;
    public TextMeshProUGUI lifeScoreText;
    public Toggle MuteButton;
    
    public List<GameObject> hordes;
    public List<GameObject> powerUps;
    public GameObject boss;
    
    private AudioSource audioSource;
    private PlayerControl playerControl;
    
    [SerializeField]private float timeToSpawnHordes;
    [SerializeField]private float timeToSpawnPowerUps;
    [SerializeField]private int bossRate;
    [SerializeField] private float timeToRespawnPlayer;

    private int score;
    private int enemiesCount;
    public int lifeScore = 2;
    private float bossCountRemainder;

    public bool isReborned;
    public bool isFreezed;
    public bool bossDefeated;
    public bool bossOn = false;
    public static bool gameOver;
    public static bool isActive;
    public int levelSelect;

    public UnityEvent SpawningBoss;
    public UnityEvent DefeatedBoss;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        highScore.text = "HiScore: " + PlayerPrefs.GetInt("HighScore",0).ToString();
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
    private void Awake()
    {
        Muted();
    }
    private void Update()
    {
        bossCountRemainder = enemiesCount % bossRate;
        SpawnBoss();
        BossDefeated();
        CheckLife();
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(timeToRespawnPlayer);
        player.transform.position = playerControl.playerStartPos;
        isReborned = true;
        isFreezed = true;
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
    IEnumerator SpawnRatePowerUps()
    {
        while (isActive)
        {
            yield return new WaitForSeconds(timeToSpawnPowerUps);
            int index = Random.Range(0, powerUps.Count);
            Instantiate(powerUps[index]);
        }
       
    }
    private void ReSpawnHordes()
    {
        int index = Random.Range(0, hordes.Count);
        Instantiate(hordes[index]);
    }
    private void ReSpawnRatePowerUps()
    {
        int index = Random.Range(0, powerUps.Count);
        Instantiate(powerUps[index]);
    }
    public void StartGame(int difficulty)
    {
        isActive = true;
        gameOver = false;
        timeToSpawnHordes /= difficulty;
        EnemyShoot.fireStart /= difficulty;
        levelSelect = difficulty;
        titleScreen.SetActive(false);
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 10, 0.25f));
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
        pauseButton.SetActive(false);

    }
    public void SpawnBoss()
    {
        if(enemiesCount > 2 && bossCountRemainder == 0 && bossOn == false)
        {

            SpawningBoss.Invoke();
        }
    
    }
    public void BossDefeated()
    {
      if (bossDefeated == true)
        {
           
            DefeatedBoss.Invoke();
            bossDefeated = false;
            bossOn = false;
            enemiesCount += 1;
        }
        
    }
    public void BossSpawned()
    {
        var bossPos = new Vector3(boss.transform.position.x, Random.Range(-ScreenBounds.yPlayerBound, ScreenBounds.yPlayerBound), boss.transform.position.z);
        bossOn = true;
        Instantiate(boss,bossPos,boss.transform.rotation);
        StopAllCoroutines();
        CancelInvoke();
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 3, 0));
        
    }
    public void BossDestroyed()
    {
        
        InvokeRepeating("ReSpawnHordes", 5, timeToSpawnHordes);
        InvokeRepeating("ReSpawnRatePowerUps", 1, timeToSpawnPowerUps);
        StartCoroutine(FadeAudioSource.StartFade(audioSource,20, 0.25f));

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
    public void UpdateLife(int lifeToAdd)
    {
        lifeScore += lifeToAdd;
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
    private void CheckLife()
    {
        if (lifeScore > -1)
        {
            lifeScoreText.text = " x " + lifeScore;
        }
        else
        {
            lifeScoreText.text = " x " + 0;
        }

        if (playerControl.playerIsDestroyed && !gameOver)
        {
            StartCoroutine(RespawnPlayer());
            
        }
    }
    public void Restart()
    {
        EnemyShoot.fireStart = 6;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Muted()
    {
        if (MuteSound.isMuted)
        {
            MuteButton.isOn = false;
        }
    }
}
