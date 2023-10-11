using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp_Shooting : MonoBehaviour
{
    public GameObject bullet;
    private Transform bulletPos;
    public float timer;
    private GameObject player;
    private GameObject enemy;
    [SerializeField] private float angle;
    private Vector3 playerVelocity;
    private Vector3 playerDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = gameObject;
    }

    void Update()
    {
        //velocity used for predicting player location to shoot towards
        playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
        playerDirection = player.transform.position -  transform.position + (playerVelocity  * 0.21f);
        //if in range, shoot player
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if( distance <enemy.GetComponent<Basic_Enemy>().chaseRange)
        {
            timer += Time.deltaTime;
            if(timer>(7.0/enemy.GetComponent<Basic_Enemy>().attackSpeed))
            {
                timer = 0;
                shoot();
            }
        }
    }

    void shoot()
    {
        bulletPos = enemy.transform;
        List<Vector3> vectorList = new List<Vector3>();
        vectorList.Add(playerDirection.normalized);
        vectorList.Add(Quaternion.Euler(0, 0, angle) * playerDirection.normalized); // 30 degrees up from direction
        vectorList.Add(Quaternion.Euler(0, 0, -angle) * playerDirection.normalized); // 30 degrees down from direction
        foreach (Vector3 direction in vectorList)
        {
            GameObject newProjectile = Instantiate(bullet, bulletPos.position, Quaternion.identity );
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            newProjectile.GetComponent<Bullet>().enemy = gameObject;
            rb.velocity = (direction * newProjectile.GetComponent<Imp_Projectile>().projectile_speed);
        }
        
    }
}


