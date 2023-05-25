using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public bool gameEnded = false;
    public bool gamePaused = false;
    public  int shots = 5;
    [SerializeField] TextMeshProUGUI shotsText;
    public GameObject GameoverMenu;
    public GameObject PauseMenu;
    LevelManager LevelManager;
    
    void Start()
    {GameoverMenu.SetActive(false);
        UpdateScoreText();
        LevelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            NewGame();


        UpdateScoreText();
        if(shots == 0) {

            GameOver();
        }
    }
    void UpdateScoreText() {
        int shotsTextWidth = 6;
        string scoreString = shots.ToString();
        scoreString = scoreString.PadLeft(shotsTextWidth, ' ');

        string update = "Shots :" + shots;


        shotsText.text = update;

    }
  public  void  GameOver() {
        FindObjectOfType<AimerController>().enabled = false;
        GameoverMenu.SetActive(true);
        Time.timeScale = 0f;
        gameEnded = true;

    }
    public void NewGame() {
        
        Time.timeScale = 1f;
        GameoverMenu.SetActive(false);
        gameEnded = false;
        FindObjectOfType<AimerController>().enabled = true;



        SceneManager.LoadScene(0);

    }

    public void QuitGame() {
        Application.Quit();
    }
    public void Pause() {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;

    }
    public void Resume() {
        FindObjectOfType<AimerController>().enabled = true;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;

    }

    
}
