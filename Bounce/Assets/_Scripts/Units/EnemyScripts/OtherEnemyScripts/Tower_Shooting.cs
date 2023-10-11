using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Shooting : MonoBehaviour
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
        List<Vector3> vectorList = new List<Vector3>();

        vectorList.Add(new Vector3(1, 0,-0.306776494f));
        vectorList.Add(new Vector3(-1, 0,-0.306776494f));
        vectorList.Add(new Vector3(0, 1,-0.306776494f));
        vectorList.Add(new Vector3(0, -1,-0.306776494f));
        foreach (Vector3 direction in vectorList)
        {
            GameObject newProjectile = Instantiate(bullet, bulletPos.position, Quaternion.identity );
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            newProjectile.GetComponent<Bullet>().enemy = gameObject;
            rb.velocity = (direction * newProjectile.GetComponent<Tower_Projectile>().projectile_speed);
        }
    }
}
