using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] enemyPrefab;

    private int waveNumber = 1;
    private int enemyCount = 0;

    [SerializeField] float spawnRange = 10.0f;

    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;

    private bool gameOver = false;
    private int score = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {        
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }

        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1.0f;
                SceneManager.LoadScene(0);
            }
        }
    }

    void SpawnEnemyWave(int enemies)
    {
        for (int i = 0; i < enemies; i++)
        {
            int enemyType = Random.Range(0, enemyPrefab.Length);

            Instantiate(enemyPrefab[enemyType], GenerateSpawnPosition(), enemyPrefab[enemyType].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    public void UpdateHealth(int health)
    {
        healthText.text = "Health : " + health;
    }

    public void AddScore()
    {
        score++;

        scoreText.text = "Score : " + score;
    }

    public void GameOver()
    {
        GameOverText.gameObject.SetActive(true);

        Time.timeScale = 0.0f;

        gameOver = true;
    }
}
