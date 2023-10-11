using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp_Projectile : Bullet
{
    private GameObject player;
    public float projectile_speed;
    public float projectile_distance;
    private Vector3 startingPosition;
    
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            Debug.Log(other.gameObject.GetComponent<Unit>().health);
        }
    }
}
