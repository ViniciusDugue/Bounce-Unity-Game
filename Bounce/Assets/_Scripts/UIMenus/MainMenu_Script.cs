using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu_Script : MonoBehaviour
{
    private GameObject mainMenu;
    void Awake()
    {
        mainMenu = GameObject.FindWithTag("MainMenu");
        mainMenu.SetActive(false);
    }
    
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        mainMenu = GameObject.Find("MainMenu");
        //when you go back and forth in the gamestates from mainmenu to lvl1, this function and 
        //playgame function keeps getting called without awake in this function ever getting called so
        //mainmenu var ends up not getting references
    }

    public void PlayGame()
    {
        mainMenu = GameObject.FindWithTag("MainMenu");
        mainMenu.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Level_1);
    }
}
