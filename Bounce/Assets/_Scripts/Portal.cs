using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public LevelCompletionMenu levelCompletionMenu;

    void OnTriggerEnter2D(Collider2D other)
    {
        levelCompletionMenu  = FindObjectOfType<LevelCompletionMenu>();
        if (other.CompareTag("Player")&&levelCompletionMenu.isOpen == false)
        {
            levelCompletionMenu.OpenLevelCompletionMenu();
        }
    }

}
