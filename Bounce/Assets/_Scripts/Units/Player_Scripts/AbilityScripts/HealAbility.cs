using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealAbility : Ability
{
    public float healAbilityAmount;
    public void UseAbility( TextMeshProUGUI abilityCooldownText, Image abilityImageIcon)
    {
        if (abilityOnCD)
        {
            Debug.Log($"{abilityName} ability is on cooldown!");
            return;
        }
        if (abilityEquiped==true &&playerStats.magika - abilityCost >= 0 && abilityOnCD == false && !PauseMenu.isPaused)
        {
            playerFunctions.UseMagika(abilityCost);
            playerFunctions.HealHealth(healAbilityAmount);
            StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));
        }
        else if (!PauseMenu.isPaused)
        {
            Debug.Log("Not enough magika!!");
        }
    }
}
