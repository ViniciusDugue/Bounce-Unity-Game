// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Curve_Projectile: Bullet
// {
//     private GameObject player;
//     private Rigidbody2D rb;
//     public float projectile_speed;
//     private float timer;
//     public float projectile_distance;
//     private Vector3 startingPosition;
//     private Vector3 playerVelocity;
//     public Vector3 direction;

//     public float Frequency = 2;
//     public float Amplitude = 2;
//     private float lifetime;

//     private void OnEnable()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         player = GameObject.FindGameObjectWithTag("Player");
//         //velocity used for predicting player location to shoot towards
//         playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
//         direction = player.transform.position -  transform.position + (playerVelocity  * 0.56f);

//         lifetime = 0;
//         direction.Normalize(); //Safety incase the object is rotated towards/away from the camera a little bit.
        
//     }

//     private void FixedUpdate()
//     {
//         lifetime += Time.fixedDeltaTime;
//         rb.velocity = GetProjectileVelocity(direction, projectile_speed, lifetime, Frequency, Amplitude);

//         float rot = Mathf.Atan2(-rb.velocity.y,-rb.velocity.x) * Mathf.Rad2Deg;
//         transform.rotation = Quaternion.Euler(0,0,rot+90);
//     }

//     private Vector2 GetProjectileVelocity(Vector2 _forward, float _speed, float _time, float _frequency, float _amplitude)
//     {
//         Vector2 up = new Vector2(-_forward.y, _forward.x);
//         float up_speed = Mathf.Cos(_time * _frequency) * _amplitude * _frequency;
//         return up * up_speed + _forward * _speed;
//     }

//     void Update()
//     {
//         // destroys projectile if it reaches enemy attackrange
//         float travelled_Distance  = Mathf.Abs(Vector2.Distance(startingPosition, gameObject.transform.position));
//         if(travelled_Distance> projectile_distance)
//         {
//             Destroy(gameObject);
//         }
       
//     }
//     void OnTriggerEnter2D(Collider2D other)
//     {
//         //checks for player collision and if so removes player hp
//         if (other.gameObject.CompareTag("Shield"))
//         {
//             Destroy(gameObject);
//         }
//         else if(other.gameObject.CompareTag("Player"))
//         {
//             player.GetComponent<PlayerController>().TakeDamage(enemy.GetComponent<Basic_Enemy>().attackDamage);
//             Destroy(gameObject);
//             Debug.Log(other.gameObject.GetComponent<Unit>().health);
//         }
//     }
// }
// // how to rotate vector direction by n degrees where radians = r= n *pi/180
// // deg = 30
// // x' = xcos(deg*pi/180)) - ysin(deg*pi/180)
// // y' = xsin(deg*pi/180) + ycos(deg*pi/180)