using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : MonoBehaviour
{
    public int damagePerSecond = 5;
    public float tickInterval = 1f;

    private float lastTickTime = 0f;
    public PlayerController playerFunctions;

    void Awake()
    {
        playerFunctions =  GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerFunctions.IncrementSpikeTileCount();
            if (playerFunctions.numSpikeTiles == 1) 
            {
                playerFunctions.TakeDamage(damagePerSecond);
                lastTickTime = Time.time;
                StartCoroutine(DamagePlayer(other.gameObject));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerFunctions.DecrementSpikeTileCount();
            if (playerFunctions.numSpikeTiles == 0) 
            {
                StopCoroutine(DamagePlayer(other.gameObject));
            }
        }
    }

    private IEnumerator DamagePlayer(GameObject player)
    {
        // Continuously damage player while they are on the spike
        while (playerFunctions.playerOnSpike)
        {
            // Only apply damage if enough time has passed since the last tick
            if (Time.time - lastTickTime >= tickInterval)
            {
                lastTickTime = Time.time;
                playerFunctions.TakeDamage(damagePerSecond);
            }

            yield return null;
        }
    }
}