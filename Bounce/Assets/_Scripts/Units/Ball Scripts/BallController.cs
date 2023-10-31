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


    [SerializeField] private float damageSpeedMultiplier;
    
    public int bounceCombos = 0;
    public GameObject bounceComboTextPrefab;
    public Vector3 bounceComboTextPosition;

    public GameObject player;
    public PlayerController playerScript;
    public Camera playerCamera;
    public PlayerCamera playerCameraScript;
    
    
    public float ballShakeDuration;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        DrawBallTrajectoryLines();
    }
    void Update()
    {
        // update bounce amount for bounce meter
        // bounce =  GetBallDamage();
        if(playerScript.isHoldingBall ==true)
        {
            Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
            Vector3 ballHoldDirection = (mousePosition - player.transform.position).normalized;
            ball.transform.position = player.transform.position + ballHoldDirection *playerScript.ballHoldDistance;
            rb.velocity =Vector3.zero;

        }
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
    public void ShootBallInMouseDirection()
    {
        print("shootball!");
        StartCoroutine(ShootingBallCooldownCoroutine());
        Vector3 ballPosition = ball.transform.position;
        Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition)-new Vector3(0,0,playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
        Vector3 shootDirection = (mousePosition - ballPosition).normalized;
        if( playerScript.isChargingBall ==true)
        {
            print(playerScript.storedChargeSpeed);
            rb.velocity = shootDirection * playerScript.storedChargeSpeed;

        }
        else
        {
            print("normal shot");
            rb.velocity = shootDirection * playerScript.ballShootSpeed;
        }
        playerScript.storedChargeSpeed = 0f;
        playerScript.isChargingBall = false; 
        playerScript.isHoldingBall = false;
    }
    
    public IEnumerator ChargeBallCoroutine()
    {
        playerScript.storedChargeSpeed = 0f;
        playerScript.isChargingBall = true; 
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        float startTime = Time.time;
        float endTime = startTime + playerScript.maxChargeDuration;
        while (Time.time < startTime + playerScript.maxChargeDuration && playerScript.isHoldingBall == true && playerScript.isChargingBall)
        {
            float remainingTime = endTime - Time.time;
            print(playerScript.storedChargeSpeed);
            playerScript.storedChargeSpeed= ((playerScript.maxChargeDuration- remainingTime)/playerScript.maxChargeDuration)* playerScript.maxBallSpeed;
            // if stored velocity reaches notch in bounce meter
            
            yield return null;
        }
    }
    
    public IEnumerator ShootingBallCooldownCoroutine()
    {
        playerScript.canShootBall = false; 
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        float startTime = Time.time;
        float endTime = startTime + playerScript.shootingBallCooldownDuration;
        while (Time.time < startTime + playerScript.shootingBallCooldownDuration && playerScript.canShootBall == false)
        {
            float remainingTime = endTime - Time.time;
            yield return null;
        }
       playerScript.canShootBall = true; 
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
    public void DrawBallTrajectoryLines()
    {
        LayerMask wallLayerMask = wallLayerMask = LayerMask.GetMask("Tilemap");
        LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");
        float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
        float maxLineDistance  = 30.0f;
        float currentLineDistance = 0.0f;
        int trajectoryBounces = 1;

        if (distanceFromBall <= playerScript.ballShootRange && playerScript.isDead == false && playerCamera !=null)
        {
            Vector3 ballPosition = ball.transform.position;
            Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
            Vector3 ballDirection = (mousePosition - ballPosition).normalized;

            while (currentLineDistance < maxLineDistance && trajectoryBounces <= 6)
            {
                Vector3 rayCastOffset  = ballDirection * 0.05f;
                RaycastHit2D hit = Physics2D.Raycast(ballPosition + rayCastOffset, ballDirection, maxLineDistance, wallLayerMask | enemyLayerMask);
                // print("Ray number: " + trajectoryBounces + " casted");
                if (hit.collider != null)
                {
                    Vector3 wallNormal = hit.normal;
                    Vector3 collisionPosition = hit.point;
                    float lineLength = (collisionPosition - ballPosition).magnitude;
                    // print("Linelength: "+ lineLength);
                    if(lineLength ==0.0f)
                    {
                        // print("line length is zero");
                    }
                    if ((currentLineDistance + lineLength) < maxLineDistance)
                    {
                        currentLineDistance += lineLength;
                        // print("Ray number: " + trajectoryBounces + " casted" + " currentLineDistance + lineLength: " + currentLineDistance + lineLength +"maxLineDistance: " + maxLineDistance );
                        Debug.DrawLine(ballPosition, collisionPosition, Color.red);
                        
                    }
                    else if((currentLineDistance + lineLength) > maxLineDistance)
                    {
                        // print("Draw leftover line");
                        float leftOverLineDistance = maxLineDistance - currentLineDistance;
                        Debug.DrawLine(ballPosition, ballPosition + ballDirection * leftOverLineDistance, Color.red);
                        currentLineDistance += leftOverLineDistance;
                    }

                    ballPosition = collisionPosition;
                    ballDirection = Vector3.Reflect(ballDirection, wallNormal).normalized;
                    
                    // print("currentLineDistance: " + currentLineDistance);
                    trajectoryBounces++;
                }
                else
                {
                    print("collider is null");
                    break; // Break out of the loop if no collision was detected
                }
            }
        }
    }

}  
