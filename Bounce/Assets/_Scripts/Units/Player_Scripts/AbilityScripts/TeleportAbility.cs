using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
//UseAbility(TextMeshProUGUI abilityCooldownText, Image abilityImageIcon)function: take in mouse position
//if within maxteleportdistance: Findteleportlocation( mouseposition):
//else: if outside of maxteleportdistance: FIndteleportlocation(mouseposition.normalized*maxtelportdistance)
//FindTeleportLocation(position)function: given position, return most suitable teleport location
//Given a ray casted out in the direction of the targetposition, check for a valid teleport location on that ray/line that is as close as possible to the target location

//CheckTeleportCollision()function: check for in tilemap bounds, doesnt collide with tilemap colliders,doesnt collide with 2d colliders

public class TeleportAbility : Ability
{
    public float maxTeleportDistance;

    public void UseAbility(TextMeshProUGUI abilityCooldownText, Image abilityImageIcon)
    {
        // Get the Tilemap component of the dungeon tilemap
        Tilemap dungeonTilemap = GameObject.Find("Tilemap").GetComponentInChildren<Tilemap>();
        if (abilityOnCD)
        {
            Debug.Log($"{abilityName} ability is on cooldown!");
            return;
        }
        if (abilityEquiped == true && playerStats.magika - abilityCost >= 0 && abilityOnCD == false && !PauseMenu.isPaused)
        {
            playerFunctions.UseMagika(abilityCost);

            Vector3 teleportDestination = mousePosition;
            Vector3 teleportDirection = (mousePosition - player.transform.position).normalized;
            float teleportDistance = Vector3.Distance(player.transform.position, mousePosition);
            if (teleportDistance > maxTeleportDistance)
            {

                teleportDestination = FindTeleportLocation(player.transform.position, player.transform.position + teleportDirection * maxTeleportDistance, dungeonTilemap);
            }
            else
            {
                teleportDestination = FindTeleportLocation(player.transform.position, mousePosition, dungeonTilemap);
            }
            if(CheckTeleportCollision(teleportDestination, dungeonTilemap))
            {
                player.transform.position = teleportDestination;
            }

            StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));
        }
        else if (!PauseMenu.isPaused)
        {
            Debug.Log("Not enough magika!!");
        }
    }

    private Vector3 FindTeleportLocation(Vector3 startPosition, Vector3 targetPosition, Tilemap dungeonTilemap)
    {
        Vector3 result = targetPosition;
        
        // Find the closest valid teleport location along the line from start to target position
        int numPoints = 8;
        for (int i = 0; i <= numPoints; i++)
        {
            
            // Calculate the t value based on the current iteration
            float t = (float)i / numPoints;
            // Use Vector3.Lerp() to calculate the interpolated position
            Vector3 point = Vector3.Lerp(targetPosition,startPosition, t);
            
            if(CheckTeleportCollision(point, dungeonTilemap))
            {
                // print("valid tplocation");
                Vector3 teleportOffsetPoint = point- (point-startPosition).normalized * 0.15f;
                // print(point);
                return teleportOffsetPoint;
            }

        }
        return startPosition;
    }

    private bool CheckTeleportCollision(Vector3 teleportPosition, Tilemap dungeonTilemap)
    {
        BoundsInt tilemapBounds = dungeonTilemap.cellBounds;
        dungeonTilemap.CompressBounds();
        if (!dungeonTilemap.HasTile(dungeonTilemap.WorldToCell(teleportPosition)))//- new Vector3(tilemapOrigin.x, tilemapOrigin.y, 0))
        {
            return false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(teleportPosition, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<TilemapCollider2D>(out TilemapCollider2D tilemapCollider))
            {
                return false;
            }
            else if (collider is BoxCollider2D || collider is CircleCollider2D)
            {
                return false;
            }
        }
        return true;
    }
}
