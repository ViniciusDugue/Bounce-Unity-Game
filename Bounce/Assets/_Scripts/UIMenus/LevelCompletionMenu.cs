using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelCompletionMenu : MonoBehaviour
{
    //On level completion opens a menu that displays level stats/scoreboard
    // Has next level button to go to next level

    public bool isOpen = false;
    public GameObject levelCompletionMenu;
    public TextMeshProUGUI scoreboardText;
    public InventoryMenu inventoryMenu;
    public ScoreBoard scoreBoard;
 
    void Awake()
    {
        levelCompletionMenu.SetActive(false);
    }
    public void Update()
    {
        if(Time.timeScale == 0f)
        {
            print("time set to 0");
        }
        else
        {
            print("time set to 1");
        }
    }
    public void OpenLevelCompletionMenu()
    {
        print("levelmenu opened");
        if(levelCompletionMenu.activeSelf==false)
        {
            scoreBoard.DisplayScoreBoard();
        }
        isOpen = true;
        levelCompletionMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseLevelCompletionMenu()
    {
        print("levelmenu closed");
        isOpen = false;
        levelCompletionMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PressInventoryButton()
    {
        // CloseLevelCompletionMenu();
        inventoryMenu.OpenInventoryMenu();

    }
    public void GoNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            GameManager.Instance.ChangeState(GameState.Level_2);
        }
        else if(SceneManager.GetActiveScene().name == "Level_2")
        {
            GameManager.Instance.ChangeState(GameState.Level_3);
        }
        CloseLevelCompletionMenu();
    }

    void OnDestroy()
    {
        Debug.Log("The levelcompletionmenu has been destroyed");
    }
}
