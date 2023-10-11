using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletor_Shooting : MonoBehaviour
{
    public GameObject bullet;
    private Transform bulletPos;
    public float timer;
    private GameObject Player;
    private GameObject enemy;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemy = gameObject;
    }

    void Update()
    {
        //if in range, shoot player
        float distance = Vector2.Distance(transform.position, Player.transform.position);
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
        GameObject newProjectile = Instantiate(bullet, bulletPos.position, Quaternion.identity);
        // passes in enemy Gameobject to instantiated projectile
        newProjectile.GetComponent<Bullet>().enemy = gameObject;
    }
}
