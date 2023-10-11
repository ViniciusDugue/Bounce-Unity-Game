using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public  GameObject pauseMenu;
    public static bool isPaused=false;

    void Awake()
    {
        pauseMenu.SetActive(false);
    }

    void Enable()
    {
        pauseMenu = GameObject.Find("PauseMenu");
    }
    public void SetPause(bool pause)
    {
        isPaused = pause;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused  = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void QuitGame()
    {
        
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused  = false;
        GameManager.Instance.ChangeState(GameState.MainMenu);
        
    }
}
