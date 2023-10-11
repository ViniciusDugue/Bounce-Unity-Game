using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SpeedAbility : Ability
{
    public float speedBoostPercentage;
    public void UseAbility( TextMeshProUGUI abilityCooldownText, Image abilityImageIcon, TextMeshProUGUI abilityDurationText)
    {
        if (abilityOnCD)
        {
            Debug.Log($"{abilityName} ability is on cooldown!");
            return;
        }
        if (abilityEquiped==true &&playerStats.magika - abilityCost >= 0 && abilityActive==false && abilityOnCD == false && !PauseMenu.isPaused)
        {
            playerFunctions.UseMagika(abilityCost);
            StartCoroutine(AbilityActiveCoroutine(abilityDurationText));
            StartCoroutine(BoostSpeed());
            StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));
        }
        else if (!PauseMenu.isPaused)
        {
            Debug.Log("Not enough magika!!");
        }
    }
    private IEnumerator BoostSpeed()
    {   
        playerStats.speed *= (speedBoostPercentage/100 + 1);
        yield return new WaitForSeconds(abilityDuration);
        playerStats.speed /= (speedBoostPercentage/100 +1);
    }
}
