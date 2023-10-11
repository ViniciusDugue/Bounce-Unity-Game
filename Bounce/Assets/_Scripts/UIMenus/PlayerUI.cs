using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
// controls entire player UI and level text
public class PlayerUI : MonoBehaviour
{
    private GameObject playerUI;
    public TextMeshProUGUI LevelText;

    void Awake()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        playerUI.SetActive(false);
    }
    
    public void ShowPlayerUI()
    {
        playerUI.SetActive(true);
    }

    public void HidePlayerUI()
    {
        playerUI.SetActive(false);
    }
    void OnDestroy()
    {
        Debug.Log("The playerui has been destroyed");
    }
    public void UpdateLevelText()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            LevelText.text = "Level 1";
        }
        else if (SceneManager.GetActiveScene().name == "Level_1")
        {
            LevelText.text = "Level 2";
        }
        else if(SceneManager.GetActiveScene().name == "Level_2")
        {
            LevelText.text = "Level 3";
        }
        else if(SceneManager.GetActiveScene().name == "Level_3")
        {
            LevelText.text = "Level 4";
        }
        else if(SceneManager.GetActiveScene().name == "Level_4")
        {
            LevelText.text = "Level 5";
        }
        else if(SceneManager.GetActiveScene().name == "Level_5")
        {
            LevelText.text = "Level 6";
        }
        else if(SceneManager.GetActiveScene().name == "Level_6")
        {
            LevelText.text = "Level 7";
        }
        else if(SceneManager.GetActiveScene().name == "Level_7")
        {
            LevelText.text = "Level 8";
        }
        else if(SceneManager.GetActiveScene().name == "Level_8")
        {
            LevelText.text = "Level 9";
        }
        else if(SceneManager.GetActiveScene().name == "Level_9")
        {
            LevelText.text = "Level 10";
        }
        
    }
}
