using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Ability : MonoBehaviour
{
    public InputAction abilityInput;
    public GameObject abilityPrefab;
    public float abilityDuration;
    public float abilityCost;
    public float abilityCooldownDuration;
    public Sprite abilitySprite;
    public AudioClip abilitySoundEffect;
    public AudioSource audioSource;
    public TextMeshProUGUI abilityCooldownText;
    public TextMeshProUGUI abilityDurationText;
    public bool abilityOnCD = false;
    public bool abilityActive = false;
    public bool abilityEquiped = false;
    public string abilityName;
    
    [HideInInspector] public Player_input playerControls;
    [HideInInspector] public GameObject player;
    [HideInInspector] public Unit playerStats;
    [HideInInspector] public Camera playerCamera;
    [HideInInspector] public PauseMenu pauseMenu;
    [HideInInspector] public Vector3 mousePosition;
    [HideInInspector] public PlayerController playerFunctions;
    public void OnEnable()
    {
        playerControls =  new Player_input();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>();
        playerFunctions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void Update() // for referencing objects that are not set to active i.e are null
    {
        if(GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>() !=null)
        {
            playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
            mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition)-new Vector3(0,0,playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
        }
        
    }
    
    public IEnumerator AbilityCooldownCoroutine(TextMeshProUGUI abilityCooldownText, Image abilityImageIcon)
    {
        float startTime = Time.time;
        abilityImageIcon.fillAmount = 0;
        abilityOnCD = true;
        while (Time.time < startTime + abilityCooldownDuration)
        {
            float remainingTime = startTime + abilityCooldownDuration - Time.time;
            abilityImageIcon.fillAmount = remainingTime / abilityCooldownDuration;
            abilityCooldownText.text = Mathf.CeilToInt(remainingTime).ToString();
            yield return null;
        }
        
        abilityOnCD = false;
        abilityCooldownText.text = "";
    }
    public IEnumerator AbilityActiveCoroutine(TextMeshProUGUI abilityDurationText)//only use for abilities with buffs/ single instance objects
    {
        abilityActive = true; 
        float startTime = Time.time;
        float endTime = startTime + abilityDuration;
        while (Time.time < startTime + abilityDuration)
        {
            float remainingTime = endTime - Time.time;
            abilityDurationText.text = Mathf.CeilToInt(remainingTime).ToString();
            yield return null;
        }
        abilityDurationText.text = "";
        abilityActive = false; 
    }

    // create a horizontal grid that stores debuffs
    // when an ability active coroutine starts, add debuff object to horizontal grid.
    // at end of coroutine, remove debuff object from horizontal grid and move all debuff objects left

    public void PlayAbilityAudio()
    {
        audioSource.clip = abilitySoundEffect;
        audioSource.Play();
    }
}
