using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public bool isGameActive = false;
    public Button restartButton;
    public Button startButton;
    public Button quitButton;
    void Start()
    {
        startButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        isGameActive = true;
    }

    public void QuitGame()
    {
        quitButton.gameObject.SetActive(true);
        Application.Quit();
    }

}
