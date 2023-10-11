using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShieldAbility : Ability
{
    
    public void UseAbility( TextMeshProUGUI abilityCooldownText,Image abilityImageIcon, TextMeshProUGUI abilityDurationText)//shieldability
    {
        if (abilityOnCD)
        {
            Debug.Log($"{abilityName} ability is on cooldown!");
            return;
        }
        if (abilityEquiped==true && playerStats.magika - abilityCost >= 0 && abilityActive==false && abilityOnCD == false && !PauseMenu.isPaused )
        {
            playerFunctions.UseMagika(abilityCost);
            Vector3 shieldSpawnPosition = player.transform.position+ new Vector3(0,-2,0);
            GameObject shield = Instantiate(abilityPrefab,shieldSpawnPosition, Quaternion.identity);
            Destroy(shield, abilityDuration);
            StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));// counts down ability cd with visual and number countdown on ability icon
            StartCoroutine(AbilityActiveCoroutine(abilityDurationText));//makes sure when shield is active you cant summon another shield
            
        }
        else if (!PauseMenu.isPaused)
        {
            Debug.Log("Not enough magika!!");
        }
    }
}
