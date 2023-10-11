using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Shooting : MonoBehaviour
{
    public GameObject bullet;
    private Transform bulletPos;
    public float timer;
    private GameObject player;
    private GameObject enemy;
    private float angle=90f;
    private Vector3 projectile_direction;
    public float spiral_Speed;
    private Vector3 player_direction;
    private Vector3 playerVelocity;
    public bool clockWise;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = gameObject;
        
    }

    void Update()
    {
        //if in range, shoot player
        float distance = Vector2.Distance(transform.position, player.transform.position);
        
        // playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        // player_direction = player.transform.position -  transform.position + (playerVelocity  * 0.0f);
        if( distance <enemy.GetComponent<Basic_Enemy>().chaseRange)
        {
            timer += Time.deltaTime;
            if(timer>(7.0/enemy.GetComponent<Basic_Enemy>().attackSpeed))
            {
                timer = 0;
                if(clockWise==true)
                {
                    angle -= Time.deltaTime * 10 * spiral_Speed;
                }
                else
                {
                    angle += Time.deltaTime * 10 * spiral_Speed;
                }
                
                projectile_direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle),1).normalized;
                shoot(projectile_direction);
            }
        }
        else
        {   
            // angle = Vector2.Angle(transform.position,player_direction)+ 90;
            // print(angle);
        }
    }
    void shoot(Vector3 projectile_direction )
    {
        bulletPos = enemy.transform; 
        GameObject newProjectile = Instantiate(bullet, bulletPos.position, Quaternion.identity );
        Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
        newProjectile.GetComponent<Bullet>().enemy = gameObject;
        rb.velocity = (projectile_direction * newProjectile.GetComponent<Tower_Projectile>().projectile_speed);
    }
}
