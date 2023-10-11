using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityDraftMenu : MonoBehaviour
{
    //two sections: 
    //reference to abilites that are not yet unlocked( if you draft then just remove from that list)
    //Ability Draft- Pick an ability/item from 3 choices by clicking on ability/item icon and then pressing a second button(select)
    //
    public GameObject draftMenu;
    public GameObject player;
    public InventoryMenu inventoryMenu;
    public AbilityManager abilityManager;
    public bool isOpen = false;
    public GameObject chosenAbilitySlot;
    public GameObject abilityDraftSlot1;
    public GameObject abilityDraftSlot2;
    public GameObject abilityDraftSlot3;
    void Awake()
    {
        draftMenu.SetActive(false);
    }

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    
    public void OpenDraftMenu()
    {
        isOpen = true;
        draftMenu.SetActive(true);  
        DisplayDraftableItems();   
        Time.timeScale = 0f;   
    }

    public void CloseDraftMenu()
    {
        isOpen = false;
        draftMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void DisplayDraftableItems()
    {
        // choose 3 random unique abilities gameobjects from abilityManager.lockedAbilities
        ShuffleChildren(abilityManager.lockedAbilities);
        abilityManager.lockedAbilities.transform.GetChild(0).transform.SetParent(abilityDraftSlot1.transform);
        abilityManager.lockedAbilities.transform.GetChild(0).transform.SetParent(abilityDraftSlot2.transform);
        abilityManager.lockedAbilities.transform.GetChild(0).transform.SetParent(abilityDraftSlot3.transform);
        abilityDraftSlot1.transform.GetChild(0).transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        abilityDraftSlot2.transform.GetChild(0).transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        abilityDraftSlot3.transform.GetChild(0).transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void ContinueButton()
    {
        if(chosenAbilitySlot.transform.GetChild(0)!=null )
        {   if(abilityDraftSlot1.transform.childCount == 1)
            {
                abilityDraftSlot1.transform.GetChild(0).SetParent(abilityManager.lockedAbilities.transform);
            }
            if(abilityDraftSlot2.transform.childCount == 1)
            {
                abilityDraftSlot2.transform.GetChild(0).SetParent(abilityManager.lockedAbilities.transform);
            }
            if(abilityDraftSlot3.transform.childCount == 1)
            {
                abilityDraftSlot3.transform.GetChild(0).SetParent(abilityManager.lockedAbilities.transform);
            }
            

            abilityManager.AddItem(chosenAbilitySlot.transform.GetChild(0).gameObject);
            CloseDraftMenu();
            inventoryMenu.OpenInventoryMenu();
        }
    }

    public void InventoryButton()
    {
        inventoryMenu.OpenInventoryMenu();
    }

    public static void ShuffleChildren(GameObject parent)
    {
        int childCount = parent.transform.childCount;
        Transform[] children = new Transform[childCount];

        // Copy the children into an array
        for (int i = 0; i < childCount; i++)
        {
            children[i] = parent.transform.GetChild(i);
        }

        // Shuffle the array
        for (int i = 0; i < childCount - 1; i++)
        {
            int randomIndex = Random.Range(i, childCount);
            Transform temp = children[randomIndex];
            children[randomIndex] = children[i];
            children[i] = temp;
        }

        // Set the new order of the children
        for (int i = 0; i < childCount; i++)
        {
            children[i].SetSiblingIndex(i);
        }
    }

    void OnDestroy()
    {
        Debug.Log("The draftmenu has been destroyed");
    }
    
}
