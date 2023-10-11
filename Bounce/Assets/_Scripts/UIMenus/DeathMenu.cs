using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeathMenu : MonoBehaviour
{
    public GameObject deathMenu;
    public GameObject player;
    public TextMeshProUGUI deathText;
    public Game_Timer gameTimer;
    public bool isDeathMenuOpen = false;
    void Awake()
    {
        deathMenu.SetActive(false);
    }

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player")!=null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void OpenDeathMenu()
    {
        if(deathMenu.activeSelf==false)
        {
            GenerateDeathText();
        }
        isDeathMenuOpen = true;
        deathMenu.SetActive(true);        
        Time.timeScale = 0f;
    }

    public void CloseDeathMenu()
    {
        isDeathMenuOpen = false;
        deathMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void RestartLevel()
    {
        
        player.GetComponent<PlayerController>().playerSpriteRenderer.enabled = true;
        player.GetComponent<PlayerController>().ResetPlayerStats();
        player.transform.position = new Vector3(-19,0,0);
        gameTimer.ResetTimer();
        CloseDeathMenu();
        
    }
    void OnDestroy()
    {
        Debug.Log("The deathmenu has been destroyed");
    }
    public void GenerateDeathText()
    {
        List<string> messages = new List<string>()
        {
            "I'd say you died with dignity, but I'd be lying.",
            "Im amazed at how many creative ways you're coming up with to die!",
            "It's okay, even the best have off days. You're just having a really, really off day.",
            "Well, that was an... interesting attempt. Maybe try closing your eyes this time?",
            "Im starting to think the game over screen is your favorite part of the game. Want me to just loop it for you?",
            "At least you're consistent. Consistently dying, that is!",
            "Try Harder.",
            "I almost feel bad.",
            "Have you considered knitting instead of dungeon crawlling? It might be more your speed.",
            "Dies of Death...",
            "Im impressed by your commitment to losing. You're really putting in the effort!",
            "Hey keep it up, maybe the enemies feel bad and might start letting you win soon! ",
            "Are you trying to beat the speedrun of dying? Youre not even good at that.",
            "You are learned in the ways of dying.",
        };
        int index = Random.Range(0, messages.Count);
        string message = messages[index];
        deathText.text = message;
    }
}
