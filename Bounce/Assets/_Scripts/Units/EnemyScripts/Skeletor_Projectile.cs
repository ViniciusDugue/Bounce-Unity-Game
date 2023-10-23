using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletor_Projectile : Bullet
{
    private GameObject player;
    private Rigidbody2D rb;
    public float projectile_speed;
    private float timer;
    public float projectile_distance;
    private Vector3 startingPosition;
    private Vector3 playerVelocity;
    public Vector3 direction;
    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        //velocity used for predicting player location to shoot towards
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        direction = player.transform.position -  transform.position + (playerVelocity  * 0.86f);

        //projectile_speed acts as a force vector essentially
        float deg = 0;
        float x = direction.x*Mathf.Cos(deg*Mathf.PI/180) - direction.y*Mathf.Sin(deg*Mathf.PI/180);
        float y = direction.x*Mathf.Sin(deg*Mathf.PI/180) + direction.y*Mathf.Cos(deg*Mathf.PI/180);
        rb.velocity =  new Vector2(x,y).normalized * projectile_speed;
        //add commented lines below If you want bullet to rotate in direction of shooting
        // float rot = Mathf.Atan2(-direction.y,-direction.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0,0,rot+90);
        startingPosition = transform.position;
    }

    void Update()
    {
        // destroys projectile if it reaches enemy attackrange
        float travelled_Distance  = Mathf.Abs(Vector2.Distance(startingPosition, gameObject.transform.position));
        if(travelled_Distance> projectile_distance)
        {
            Destroy(gameObject);
        }
       
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.CompareTag("Player") && other !=null)
        {
            player.GetComponent<PlayerController>().TakeDamage(enemy.GetComponent<Basic_Enemy>().attackDamage);
            Destroy(gameObject);
            // Debug.Log(other.gameObject.GetComponent<Unit>().health);
        }
    }
}
// how to rotate vector direction by n degrees where radians = r= n *pi/180
// deg = 30
// x' = xcos(deg*pi/180)) - ysin(deg*pi/180)
// y' = xsin(deg*pi/180) + ycos(deg*pi/180)