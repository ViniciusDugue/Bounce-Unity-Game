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
    public float collisionOffset = 0.05f;
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
    
    
    
    private void Awake() 
    {
        rb =  GetComponent<Rigidbody2D>(); 
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);
        // Collider2D childCollider = transform.GetChild(0).GetComponent<Collider2D>();

        // if (parentCollider != null && childCollider != null)
        // {
        //     Physics2D.IgnoreCollision(childCollider,parentCollider);
        // }
        // else
        // {
        //     Debug.LogWarning("Colliders not found on parent or child.");
        // }
    }

    // returns true/false if there is Collisions when moving in direction
    // public bool checkCollision(Vector2 direction)
    // {
    //     int count = rb.Cast(
    //         direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
    //         movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
    //         castCollisions, // List of collisions to store the found collisions into after the Cast is finished
    //         speed * Time.fixedDeltaTime + collisionOffset); // The magnitude to cast equal to the movement plus an offset
    //     if (count == 0)
    //     {
    //         return false;
    //     }
    //     else
    //     {
    //         return true;
    //     }
    // }
    public bool checkCollision(Vector2 direction)
    {
        Vector2 size = (GetComponent<BoxCollider2D>().size  + new Vector2(collisionOffset, collisionOffset)) * transform.lossyScale;
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        
        int count = Physics2D.BoxCast(
            rb.position,         // The center of the box
            size,                // The size of the box
            0f,                  // The angle of the box (0 for no rotation)
            direction,           // The direction to cast
            movementFilter,      // The settings that determine where a collision can occur
            hits,                // List of collisions to store the found collisions into
            speed * Time.fixedDeltaTime + collisionOffset); // The magnitude to cast equal to the movement plus an offset
        
        if (count <=1)// the 1 collision is yourself
        {
            return false;
        }
        else
        {
            // for (int i = 0; i < count; i++)
            // {
            //     print("Collision detected with: " + hits[i].collider.name);
            // }
            return true;
        }
    }
    public void MoveUnit(Vector2 moveVector)
    {
        // Check for collisions in the desired movement direction
        if (checkCollision(moveVector.normalized) == true)
        {
            // Try moving along the x-axis only
            Vector2 horizontalMoveVector = new Vector2(moveVector.x, 0).normalized * speed * Time.fixedDeltaTime;
            Vector2 verticalMoveVector = new Vector2(0, moveVector.y).normalized * speed * Time.fixedDeltaTime;
            // if (checkCollision(horizontalMoveVector.normalized) == false)
            // {
            //     moveVector = horizontalMoveVector;
            // }
            // // Try moving along the y-axis only
            // else if (checkCollision(verticalMoveVector.normalized) == false)
            // {
            //     moveVector = verticalMoveVector;
            // }
            //  // No available movement direction - don't move
            // else
            // {
            //     moveVector = Vector2.zero;
            // } 
            moveVector = Vector2.zero;  
        }
        
        // if(moveVector.magnitude !=0.0f)
        // {
        //     print("Object Name: " + gameObject.name + ", Move Vector: " + moveVector);
        // }
        // Move the unit in the chosen direction
        transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(moveVector.x, moveVector.y,0), speed * Time.deltaTime);
        // rb.MovePosition(rb.position + moveVector);
    }
}