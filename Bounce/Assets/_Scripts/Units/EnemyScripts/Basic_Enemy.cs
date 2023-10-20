using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Basic_Enemy : Unit
{
    // public float chaseRange = 10f;
    // public float maxChaseRange = 2f;
    private Transform target;
    private float minAmbientRange = 2.5f;
    private Vector3 targetDirection;
    private Vector3 startingPosition;
    private GameObject player;
    private float chaseTimer = 0f;

    private float directionTime = 0f;

    public void Awake()
    {
        startingPosition = transform.position;
        targetDirection = Random.insideUnitCircle.normalized;
        player = GameObject.FindWithTag("Player");
        target = player.transform;
    }
    public void Update()
    {
        Vector3 playerDirection = target.position - transform.position;
        //Chase player if in chase range but no further than maxChaseRange and no Collisions
        if (Vector3.Distance(transform.position, target.position) <= chaseRange && Vector3.Distance(transform.position, target.position)>= maxChaseRange)
        {
            MoveUnit(playerDirection *speed *Time.deltaTime);
            startingPosition = transform.position;
            chaseTimer += Time.deltaTime;
        }
        //ambient movement
        else if(Vector3.Distance(transform.position, target.position)>= maxChaseRange)
        { 
            // Calculate the distance from the starting position
            float distanceFromStart = Vector3.Distance(transform.position, startingPosition);

            // Check if the unit is beyond the maximum range
            if (distanceFromStart > minAmbientRange)
            {
                // Move the unit back towards the starting position
                /*Vector3 moveDirection = (startingPosition - transform.position).normalized;
                MoveUnit(moveDirection * Time.deltaTime* speed*4);*/

                // Set the target direction back to the starting position
                targetDirection = (startingPosition - transform.position).normalized;
                directionTime = Random.Range(0.5f, 1.5f);
            }

            // Universal move unit
            MoveUnit(targetDirection * Time.deltaTime * speed * 4);
            chaseTimer = 0f;

            // If the unit has been moving in the same direction for too long, generate a new random direction
            if (directionTime < 0f)
            {
                targetDirection = Random.insideUnitCircle.normalized;
                directionTime = Random.Range(0f, 1f);
            }

            directionTime -= Time.deltaTime;
        }
        if (Vector3.Distance(transform.position, target.position) <= maxChaseRange && chaseTimer >= 2.5f)
        {
            startingPosition = transform.position;
            chaseTimer = 0f;
        }
    }
    // public void Update()
    // {
        
    //     float playerEnemyDistance = Vector3.Distance(transform.position, target.position);
    //     // if inbetween range of maxChaseRange/chaseRange, chase player
    //     if (playerEnemyDistance <= chaseRange && playerEnemyDistance>= maxChaseRange)
    //     {
    //         ChasePlayer();
    //     }
    //     //if not in range of player, do ambient movement 
    //     else if(playerEnemyDistance>= maxChaseRange)
    //     { 
    //         MoveAmbiently();
    //     }
    //     //if out of chaserange then store position as start position for ambient movement
    //     if (Vector3.Distance(transform.position, target.position) <= maxChaseRange)
    //     {
    //         startingPosition = transform.position;
    //     }
    // }
    
    // public void ChasePlayer()
    // {
    //     Vector3 playerDirection = new Vector2(target.position.x -  transform.position.x, target.position.y -  transform.position.y);
    //     MoveUnit(playerDirection * Time.deltaTime* speed*4);
    //     startingPosition = transform.position;
    // }

    // public void MoveAmbiently()
    // {
    //     // Calculate the distance from the starting position
    //     float distanceFromStart = Vector3.Distance(transform.position, startingPosition);

    //     // if the unit is beyond the maximum range, move unit back towards start position
    //     if (distanceFromStart > minAmbientRange)
    //     {
    //         Vector3 moveDirection = (startingPosition - transform.position).normalized;
    //         MoveUnit(moveDirection * Time.deltaTime* speed*4);
    //     } 
    //     // Move the unit in a semi-random direction
    //     else 
    //     {

    //         MoveUnit(randomDirection * Time.deltaTime* speed*4);
    //     }   
    //     // If the unit has been moving in the same direction for too long, generate a new random direction
    //     if (Random.Range(0f, 1f) < 0.05f)
    //     {
    //         randomDirection = Random.insideUnitCircle.normalized;
    //     }
    // }
   
    public void TakeDamage(int damage)
    {
        if(health - damage <=0)
        {
            Destroy(gameObject);
        }
        else
        {
            if (isColorShifted == false) 
            {
            StartCoroutine(ShiftColor(whiteShift, 0.15f));
            }
            health-= damage;
        }
    }
}