using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public GameObject titleScreen;
    private PlayerController playerController;
    private SpawnManager spawnManager;

    private float spawnInterval;
    private float spawnRandLo = 3;
    private float spawnRandHi = 6;

    private int score; 

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

    }

    // public void UpdateScore(int scoreToAdd) {
    //     score += scoreToAdd;
    //     scoreText.text = "Score: " + score;
    // }

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty) {

        spawnRandLo /= difficulty;
        spawnRandHi /= difficulty;

        titleScreen.gameObject.SetActive(false);
        spawnInterval = Random.Range(spawnRandLo, spawnRandHi);
        StartCoroutine(spawnManager.SpawnInterval(spawnInterval));
        playerController.isAlive = true;
        //score = 0;
        //UpdateScore(0);
    }
}
