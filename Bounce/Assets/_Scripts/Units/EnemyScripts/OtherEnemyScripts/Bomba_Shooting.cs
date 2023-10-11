using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba_Shooting : MonoBehaviour
{
    public GameObject bullet;
    private Transform bulletPos;
    public float timer;
    private GameObject Player;
    private GameObject enemy;
    public int projectile_Count;
    public float blowUpRange;
    public float blowUpDelay;
    private bool isDead;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        enemy = gameObject;
        isDead = false;
    }

    void Update()
    {
        //if in range, blow up on player
        float distance = Vector2.Distance(transform.position, Player.transform.position);
        if ( distance <= blowUpRange &&enemy.GetComponent<Basic_Enemy>().chaseRange!=0)
        {
            timer += Time.deltaTime;
            if(timer>(7.0/enemy.GetComponent<Basic_Enemy>().attackSpeed)&& isDead==false)
            {
                timer = 0;
                //stop bomba from moving and after short delay, blowup
                enemy.GetComponent<Basic_Enemy>().speed=0;
                Invoke("blowUp", blowUpDelay);
                isDead=true;
            }
        }
            
        
    }
    void blowUp()
    {
        
        bulletPos = enemy.transform;
        //remove bomba visibility
        enemy.GetComponent<Renderer>().enabled=false;
        for (int i = 0; i <= projectile_Count; i++)
        {
            float angle = (360/projectile_Count) * i;
            Vector3 projectileDirection =  new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),Mathf.Cos(angle * Mathf.Deg2Rad),0f).normalized;
            GameObject newProjectile = Instantiate(bullet, bulletPos.position, Quaternion.identity );
            Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
            newProjectile.GetComponent<Bullet>().enemy = gameObject;
            rb.velocity = (projectileDirection * newProjectile.GetComponent<Tower_Projectile>().projectile_speed);
        }
        Destroy(gameObject,2f);
        
    }
}
