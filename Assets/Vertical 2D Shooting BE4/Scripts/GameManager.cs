using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver = false;
    private bool isGameStart = false;
    private int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverText;
    public GameObject clearText;
    public GameObject startText;
    //private AudioSource audioSource;
    private Boss boss;
    private EnemySpawner spawner;

    public float spawnTime = 0f;
    private int spawnCount = 1;

    private float pauseValue = 1f;

    public event System.Action onGameOver;
    //public event System.Action bossDie;

    private void Awake()
    {
        gameOverText.SetActive(false);
        clearText.SetActive(false);
        //audioSource = GetComponent<AudioSource>();
        var findGo = GameObject.FindWithTag("Boss");
        boss = findGo.GetComponent<Boss>();

        var findSpawner = GameObject.FindWithTag("EnemySpawner");
        spawner = findSpawner.GetComponent<EnemySpawner>();

        Time.timeScale = 0f;
    }

    private void Start()
    {
        //audioSource.Play();
        isGameStart = false;
        isGameOver = false;

    }

    public void OnPlayerDie()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
        Time.timeScale = 0f;
        //audioSource.enabled = false;

        onGameOver.Invoke();
    }

    private void OnStartGame()
    {
        isGameStart = true;
        startText.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        scoreText.text = $"Score: {score}";

        if(!isGameStart && Input.GetKeyDown(KeyCode.S))
        {
            OnStartGame();
        }

        if (isGameStart)
        {
            spawnTime += Time.deltaTime;

            if (spawnTime >= 60f && spawnCount == 1)
            {
                spawnCount--;
                boss.SpawnBoss();
                spawner.Stop();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseGame();
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void ClearGame()
    {
        clearText.SetActive(true);
        isGameOver = true;
        Time.timeScale = 0f;
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void AddScore()
    {
        score++;
    }
}
