using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float defense;
    public float attackDamage;
    public float attackSpeed;
    public float chaseRange;
    public float maxChaseRange;
    public float collisionOffset = 2.0f;
    public float magika;
    public float maxMagika;
    public float magikaRegen;
    public float damageReduction = 1f;
    public int closeCalls;
    public int goldCollected;
    [HideInInspector] public float totalDamageTaken; 
    [HideInInspector] public float totalMagikaUsed;

    public ContactFilter2D movementFilter;
    public List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public Rigidbody2D rb;
    
    // returns true/false if there is Collisions when moving in direction
    public bool checkCollision(Vector2 direction)
    {
        rb =  GetComponent<Rigidbody2D>(); 
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            speed * Time.fixedDeltaTime + collisionOffset); // The magnitude to cast equal to the movement plus an offset
        if (count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void MoveUnit(Vector2 moveVector)
    {
        // Check for collisions in the desired movement direction
        if (checkCollision(moveVector.normalized) != true)
        {
            rb.MovePosition(rb.position + moveVector);
        }
        else
        {
            print("collision detected");
        }
    }
}