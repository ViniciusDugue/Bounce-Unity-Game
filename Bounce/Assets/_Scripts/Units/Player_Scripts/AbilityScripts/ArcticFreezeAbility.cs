using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ArcticFreezeAbility : Ability
{
    public float frostbiteScaleMultiplier;
    public float freezeRadius;
    
    public void UseAbility( TextMeshProUGUI abilityCooldownText,Image abilityImageIcon)
    {
        if (abilityOnCD)
        {
            Debug.Log($"{abilityName} ability is on cooldown!");
            return;
        }
        //on mouse right click, find all enemy gameobjects in radius around mouse location.
        // set enemy speed to 0 and aggro off for duration
        //spawn frostbite game object on all enemies in radius and make it disapear after duration
        //bonus** make frostbite gameobject scale with enemy gameobject scale
        if (abilityEquiped==true  && abilityOnCD == false && !PauseMenu.isPaused)
        {
            // playerFunctions.UseMagika(abilityCost);
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(mousePosition, freezeRadius);

            foreach (Collider2D hitCollider in hitColliders)
            {
                Basic_Enemy enemy = hitCollider.GetComponent<Basic_Enemy>();
                if (enemy != null)
                {
                    StartCoroutine(FreezeEnemyForDuration(enemy, abilityDuration));
                    
                    Vector3 frostbitePosition = enemy.transform.position - new Vector3(0, 0.3f, 0);
                    GameObject frostbite = Instantiate(abilityPrefab, frostbitePosition, Quaternion.identity);
                    frostbite.transform.localScale = enemy.transform.localScale * frostbiteScaleMultiplier;
                    Destroy(frostbite, abilityDuration);
                }
            }
            StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));
        }
        else if (!PauseMenu.isPaused)
        {
            Debug.Log("Not enough magika!!");
        }
    }

    private IEnumerator FreezeEnemyForDuration(Basic_Enemy enemy, float duration)
    {
        float originalSpeed = enemy.speed;
        float originalChaseRange = enemy.chaseRange;
        enemy.chaseRange = 0f;
        enemy.speed = 0f;
        yield return new WaitForSeconds(duration);
        enemy.chaseRange = originalChaseRange;
        enemy.speed = originalSpeed;
    }
}
