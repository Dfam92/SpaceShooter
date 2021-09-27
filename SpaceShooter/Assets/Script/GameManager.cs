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
    public List<GameObject> hordes2;
    public List<GameObject> hordes3;
    public GameObject boss;
    public GameObject extraLife;
    public GameObject normalPowerUpCase;
    public GameObject epicPowerUpCase;
    public GameObject dangerPowerUpCase;
   

    private AudioSource audioSource;
    private PlayerControl playerControl;
    
    [SerializeField]private float timeToSpawnHordes;
    [SerializeField]private float timeToSpawnNormalCasePowerUps;
    [SerializeField] private float timeToSpawnEpicCasePowerUps;
    [SerializeField] private float timeToSpawnDangerCasePowerUps;
    [SerializeField]private int bossRate;
    [SerializeField] private float timeToRespawnPlayer;

    private int score;
    private int enemiesCount;
    private int bossCount;
    public float speedIncrease = 1;
    public float shootRateDecrease;
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
        ChangeColorScore();
    }
    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(timeToRespawnPlayer);
        player.transform.position = playerControl.playerStartPos;
        isReborned = true;
        isFreezed = true;
    }
    
    
    private void SpawnPowerUps()
    {
        InvokeRepeating("SpawnNormalPowerUps", timeToSpawnNormalCasePowerUps, timeToSpawnNormalCasePowerUps);
        InvokeRepeating("SpawnEpicPowerUps", timeToSpawnEpicCasePowerUps, timeToSpawnEpicCasePowerUps);
        InvokeRepeating("SpawnDangerPowerUps", timeToSpawnDangerCasePowerUps, timeToSpawnDangerCasePowerUps);
    }
    private void SpawnHordes()
    {
        if (bossCount == 0)
        {
            int index = Random.Range(0, hordes.Count);
            Instantiate(hordes[index]);
        }
        else if (bossCount == 1)
        {
            int index = Random.Range(0, hordes2.Count);
            Instantiate(hordes2[index]);
        }
        else
        {
            int index = Random.Range(0, hordes3.Count);
            Instantiate(hordes3[index]);
        }
    }
    private void SpawnNormalPowerUps()
    {
        PositionGenerator();
        //Instantiate(normalPowerUpCase);
        ObjectPooler.Instance.SpawnFromPool("NormalCase", normalPowerUpCase.transform.position, normalPowerUpCase.transform.rotation);
    }
    private void SpawnEpicPowerUps()
    {
        if(bossCount > 0)
        {
            PositionGenerator();
            //Instantiate(epicPowerUpCase);
            ObjectPooler.Instance.SpawnFromPool("EpicCase", epicPowerUpCase.transform.position, epicPowerUpCase.transform.rotation);
        }
       
    }
    private void SpawnDangerPowerUps()
    {
        if(bossCount > 1)
        {
            PositionGenerator();
            //Instantiate(dangerPowerUpCase);
            ObjectPooler.Instance.SpawnFromPool("DangerousCase", dangerPowerUpCase.transform.position, dangerPowerUpCase.transform.rotation);
        }
       
    }
    public void StartGame(int difficulty)
    {
        isActive = true;
        gameOver = false;
        timeToSpawnHordes /= difficulty;
        EnemyShoot.fireStart /= difficulty;
        EnemyShoot.fireRate /= difficulty;
        levelSelect = difficulty;
        titleScreen.SetActive(false);
        StartCoroutine(FadeAudioSource.StartFade(audioSource, 10, 0.25f));
        audioSource.Play();
        InvokeRepeating("SpawnHordes", 2, timeToSpawnHordes);
        SpawnPowerUps();
        
        //when Mobile add the buttons firebutton and joystick in prefabs into the canvas 
        scoreAndButtons.SetActive(true);
    }
    public void GameOver()
    {
        playerControl.isMultiplying2x = false;
        playerControl.isMultiplying4x = false;
        PlayerControl.onSideBullets = false;
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
            bossCount += 1;
            speedIncrease += 0.1f;
            shootRateDecrease += 0.25f;
            EnemyShoot.fireRate -= shootRateDecrease;
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
        Instantiate(extraLife);
        InvokeRepeating("SpawnHordes", 5, timeToSpawnHordes);
        InvokeRepeating("SpawnNormalPowerUps", 1, timeToSpawnNormalCasePowerUps);
        InvokeRepeating("SpawnEpicPowerUps", 2, timeToSpawnEpicCasePowerUps);
        InvokeRepeating("SpawnDangerPowerUps", 5, timeToSpawnDangerCasePowerUps);

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
        EnemyShoot.fireRate = 30;
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
    private void ChangeColorScore()
    {
        if(playerControl.isMultiplying2x)
        {
            scoreText.color = Color.red;
        }
        else if(playerControl.isMultiplying4x)
        {
            scoreText.color = Color.magenta;
        }
        else
        {
            scoreText.color = Color.yellow;
        }
    }
    private void PositionGenerator()
    {
        var randomPos = new Vector2(Random.Range(-ScreenBounds.xPlayerBound + 0.5f, ScreenBounds.xPlayerBound - 0.5f), ScreenBounds.yPlayerBound + 0.75f);
        epicPowerUpCase.transform.position = randomPos;
        dangerPowerUpCase.transform.position = randomPos;
        normalPowerUpCase.transform.position = randomPos;

    }
}
