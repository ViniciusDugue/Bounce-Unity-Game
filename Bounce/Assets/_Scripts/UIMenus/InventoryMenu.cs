using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    // display 3 ability slots from inventory that are equiped.
    //store unlocked abilites somewhere like a list/inventory
    //
    // display all abilities unlocked.isOpen
    public bool isOpen = false;
    public GameObject inventoryMenu;
    public AbilityDraftMenu abilityDraftMenu;
    public AbilityManager abilityManager;
    void Awake()
    {
        inventoryMenu.SetActive(false);
    }

    public void OpenInventoryMenu()
    {
        print("inventorymenu opened");
        isOpen = true;
        inventoryMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseInventoryMenu()
    {
        print("inventorymenu closed");
        isOpen = false;
        abilityManager.SetEquipedAbilites();
        inventoryMenu.SetActive(false);
        if(abilityDraftMenu.isOpen!=true)
        {
            Time.timeScale = 1f;
        }
    }

    void OnDestroy()
    {
        Debug.Log("The InventoryMenu has been destroyed");
    }
}
