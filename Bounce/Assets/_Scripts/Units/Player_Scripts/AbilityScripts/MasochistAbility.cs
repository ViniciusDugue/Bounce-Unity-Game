using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MasochistAbility : Ability
{
    public float damageHealPercentage;
    public Color masochistColor;
    public void UseAbility( TextMeshProUGUI abilityCooldownText,Image abilityImageIcon, TextMeshProUGUI abilityDurationText)
    {
        if (abilityOnCD)
        {
            Debug.Log($"{abilityName} ability is on cooldown!");
            return;
        }
        if (abilityEquiped==true && abilityActive==false && abilityOnCD == false && !PauseMenu.isPaused )
        {
            // playerFunctions.UseMagika(abilityCost);
            StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));
            StartCoroutine(AbilityActiveCoroutine(abilityDurationText));
            StartCoroutine(HealFromDamageTaken());
            StartCoroutine(playerFunctions.ShiftColor(masochistColor, abilityDuration));
        }
        else if (!PauseMenu.isPaused)
        {
            Debug.Log("Not enough magika!!");
        }
    }
    private IEnumerator HealFromDamageTaken()
    {   
        playerStats.damageReduction *= -(damageHealPercentage/100);
        yield return new WaitForSeconds(abilityDuration);
        playerStats.damageReduction /= -(damageHealPercentage/100);
    }
}
