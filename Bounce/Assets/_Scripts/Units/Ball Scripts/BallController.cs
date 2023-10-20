using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//controls ball behavior 
public class BallController : MonoBehaviour
{
    // ball bounces off player and colliders
    // on collision ball deals damage to enemies
    // ball is enabled in level one in gamemanager and is spawned in next to the player in each level
    public float ballSize = 1.0f;
    private Rigidbody2D rb;
    private GameObject ball;
    public int ballDamage = 10;
    public PlayerCamera playerCameraScript;
    [SerializeField] private float damageSpeedMultiplier;
    [SerializeField] private float ballInitialSpeed = 20f;
    public float ballShakeDuration;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = gameObject;
        rb.velocity = new Vector3(-1,-1,0).normalized * ballInitialSpeed;
        // dynamicCollider= transform.Find("ChildObject");
        // SetBallSize();
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
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            print("player collision");
            rb.velocity *= 1.0f;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("enemy collision!");
            
            rb.velocity *= 1.0f;
            collision.gameObject.GetComponent<Basic_Enemy>().TakeDamage(GetBallDamage());
            playerCameraScript.ShakeCamera(ballShakeDuration);
            print("shake has occurred");
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        Vector3 preCollisionVelocity = rb.velocity;
        

        if (collision.gameObject.CompareTag("Player"))
        {
            print("player collision exited");
        }

    }

    // calculates damage of ball with respect to balls speed
    private int GetBallDamage()
    {
        int BallDamage = Mathf.RoundToInt(damageSpeedMultiplier * rb.velocity.magnitude);
        // return ballDamage * rb.velocity.normalized.magnitude;
        print("BallDamage: " + BallDamage);
        return BallDamage;
    }
    // private void SetBallSize()
    // {
    //     // Scale the sprite based on the current scale multiplied by ballSize
    //     transform.localScale = new Vector3(transform.localScale.x * ballSize, transform.localScale.y * ballSize, 1);

    //     // Adjust the CircleCollider2D radius based on the current radius multiplied by ballSize
    //     CircleCollider2D collider = GetComponent<CircleCollider2D>();
    //     if (collider != null)
    //     {
    //         collider.radius *= ballSize;
    //     }
    // }
}  
