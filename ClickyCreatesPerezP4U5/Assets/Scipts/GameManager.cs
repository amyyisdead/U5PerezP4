using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI gameOverText;
    private float spawnRate = 1.0f;
    public TextMeshProUGUI scoreText;
    public Button restartButton;
    public bool isGameActive;
    private int score;
    public GameObject titleScreen;
    public TextMeshProUGUI livesText;
    public GameObject pauseScreen;
    private int lives;
    private bool paused;
    
    // Start is called before the first frame update
    void Start()
    {

       

        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int Index = Random.Range(0, targets.Count);
            Instantiate(targets[Index]);
        }
        
    }
    public void UpdateScore(int scoreToAdd)
    {
        
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        
    }
    
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);

        titleScreen.gameObject.SetActive(false);
    }


    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }




    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;

        if(lives <= 0)
        {
            GameOver();
        }
    }
}
