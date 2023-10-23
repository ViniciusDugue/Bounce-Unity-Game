using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

//controls ball behavior 
public class BallController : MonoBehaviour
{
    // ball bounces off player and colliders
    // on collision ball deals damage to enemies
    // ball is enabled in level one in gamemanager and is spawned in next to the player in each level
    public float ballSize = 1.0f;
    private Rigidbody2D rb;
    public GameObject ball;
    public float bounce;
    public float maxBounce;

    [SerializeField] private float damageSpeedMultiplier;
    
    public int bounceCombos = 0;
    public GameObject bounceComboTextPrefab;
    public Vector3 bounceComboTextPosition;

    public PlayerCamera playerCameraScript;
    public float ballShakeDuration;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // update bounce amount for bounce meter
        bounce =  GetBallDamage();
    }
    public void ActivateBall()
    {
        ball.SetActive(true);
    }

    public void DeactivateBall()
    {
        ball.SetActive(false);
    }

    public void SpawnBallAtLocation(Vector3 spawnLocation)
    {
        ball.transform.position= spawnLocation;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 preCollisionVelocity = rb.velocity;
        if (collision.collider is TilemapCollider2D)
        {
            print("tilemap collision!!");
            rb.velocity *= 0.9f;
            playerCameraScript.ShakeCamera(ballShakeDuration);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            print("player collision");
            rb.velocity *= 1.0f;
            playerCameraScript.ShakeCamera(ballShakeDuration);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("enemy collision!");
            bounceCombos +=1;
            DisplayBounceComboText();
            rb.velocity *= 1.0f;
            collision.gameObject.GetComponent<Basic_Enemy>().TakeDamage(GetBallDamage());
            playerCameraScript.ShakeCamera(ballShakeDuration);
            print("shake has occurred");
        }
    }

    // calculates damage of ball with respect to balls speed
    private int GetBallDamage()
    {
        float bounceComboDamage =  damageSpeedMultiplier * rb.velocity.magnitude * bounceCombos/2;
        
        int BallDamage = Mathf.RoundToInt(damageSpeedMultiplier * rb.velocity.magnitude + bounceComboDamage);
        print("Full Damage: " + BallDamage + " Combos: "+ bounceCombos+" ComboDamage: " + bounceComboDamage +" velocity: " + rb.velocity.magnitude);
        return BallDamage;
    }

    public void DisplayBounceComboText()
    {
        // Create a new instance of the combo text prefab
        GameObject bounceComboText = Instantiate(bounceComboTextPrefab, transform.position, Quaternion.identity);
        // Set the text to display the damage taken
        if(bounceCombos >1)
        {
            bounceComboText.GetComponent<TextMeshPro>().text = bounceCombos.ToString() + " Hits";
        }
    }
    
    public void ResetBounceCombo()
    {
        bounceCombos = 0;
    }

}  
