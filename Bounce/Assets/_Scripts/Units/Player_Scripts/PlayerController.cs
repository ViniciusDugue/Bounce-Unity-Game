using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class PlayerController : Unit
{ 
    private InputAction ability1Input;
    private InputAction ability2Input;
    private InputAction ability3Input;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI magikaText;
    public Unit playerStats;
    private Vector2 moveInput;
    private GameObject player;
    public Player_input playerControls;
    private InputAction pause;
    private PauseMenu pauseMenu;
    private Camera playerCamera;
    public bool isDead = false;
    private Vector3 mousePosition;
    private AbilityManager abilityManager;
    [HideInInspector] public SpriteRenderer playerSpriteRenderer;
    private DeathMenu deathMenu;
    
    private InputAction ballShootInput;
    public GameObject ball;
    public float ballShootSpeed;
    public float ballShootRange;

    private InputAction ballHoldInput;
    public float ballHoldRange;
    public float ballHoldDistance;
    public bool isHoldingBall = false;
    public float holdDuration;

    public LayerMask wallLayerMask;
    public LayerMask enemyLayerMask;

    [HideInInspector] public int numSpikeTiles = 0;
    public bool playerOnSpike { get { return numSpikeTiles > 0; } }

    public void Awake()
    {

        wallLayerMask = LayerMask.GetMask("Tilemap");
        enemyLayerMask = LayerMask.GetMask("Enemy");
        rb =  GetComponent<Rigidbody2D>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        deathMenu = FindObjectOfType<DeathMenu>();
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Unit>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerControls =  new Player_input();
        pause = playerControls.Player.Pause;
        abilityManager =  GameObject.FindGameObjectWithTag("AbilityManager").GetComponent<AbilityManager>();
        
        ability1Input = playerControls.Player.Ability1;
        ability2Input = playerControls.Player.Ability2;
        ability3Input = playerControls.Player.Ability3;
        ballShootInput =  playerControls.Player.ShootBall;
        ballHoldInput = playerControls.Player.HoldBall;
        if(pause==null)
            print("player pause is null");
        DeactivatePlayer();
        
        Debug.Log("player var set");
    }

    public void ActivatePlayer()
    {
        player.SetActive(true);
    }

    public void DeactivatePlayer()
    {
        player.SetActive(false);
    }

    void Update()
    {
        deathMenu = FindObjectOfType<DeathMenu>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        PassiveMagikaRegeneration();
        healthText.text = Mathf.Round(playerStats.health).ToString() + " / " + playerStats.maxHealth.ToString();
        magikaText.text = Mathf.Round(playerStats.magika).ToString() + " / " + playerStats.maxMagika.ToString();
        if(isDead ==true && deathMenu.isDeathMenuOpen==false)
        {
            print("player died");
            playerSpriteRenderer.enabled = false;
            if(deathMenu ==null)
            {
                print("death menu is null");
            }
            deathMenu.OpenDeathMenu();
        }

        if(isHoldingBall ==true)
        {
            
            Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
            Vector3 ballHoldDirection = (mousePosition - player.transform.position).normalized;
            
            ball.transform.position = player.transform.position + ballHoldDirection *ballHoldDistance;
            
        }
    }

    private void OnEnable()
    {
        pause.Enable();
        pause.performed += Pause;
        ability1Input.Enable();
        ability1Input.performed +=UseAbility1;
        ability2Input.Enable();
        ability2Input.performed +=UseAbility2;
        ability3Input.Enable();
        ability3Input.performed +=UseAbility3;
        ballShootInput.Enable();
        ballShootInput.performed +=ShootBall;
        ballHoldInput.Enable();
        ballHoldInput.performed +=HoldBall;
        Debug.Log("Player enabled");
    }

    private void OnDisable()
    {
        pause.Disable();
        pause.performed -= Pause;
        ability1Input.Disable();
        ability1Input.performed -=UseAbility1;
        ability2Input.Disable();
        ability2Input.performed -=UseAbility2;
        ability3Input.Disable();
        ability3Input.performed -=UseAbility3;
        ballShootInput.Disable();
        ballShootInput.performed -=ShootBall;
        ballHoldInput.Disable();
        ballHoldInput.performed -=HoldBall;
        Debug.Log("Player disabled");

    }

    public void SpawnPlayerAtLocation(Vector3 spawnLocation)
    {
        player.transform.position= spawnLocation;
    }

    public void Pause(InputAction.CallbackContext context)
    {
        bool isPaused = PauseMenu.isPaused;
        if(context.performed && isPaused ==false&& Time.timeScale ==1f)
        {
            pauseMenu.PauseGame();
        }
        else if(context.performed && isPaused ==true && Time.timeScale ==0f)
        {
            pauseMenu.ResumeGame();
        }
    }

    void FixedUpdate()
    {
        DrawBallTrajectoryLines();
        MovePlayer(moveInput);
    }

    public void MovePlayer(Vector2 direction)
    {
        Vector2 horizontalMoveDirection = new Vector2(direction.x, 0).normalized * speed * Time.fixedDeltaTime;
        Vector2 verticalMoveDirection = new Vector2(0, direction.y).normalized * speed * Time.fixedDeltaTime;
        if (checkCollision(direction)== true)
        {
            //try left/right
            if(checkCollision(horizontalMoveDirection.normalized)==false)
            {
                direction = horizontalMoveDirection;
            }
            //try up/down
            else if(checkCollision(verticalMoveDirection.normalized)==false)
            {
                direction = verticalMoveDirection;
            }
            else
            {
                direction = Vector2.zero;
            }
        }   
        
        // if(direction.magnitude !=0.0f)
        // {
        //     print("Object Name: " + gameObject.name + ", Move Vector: " + direction);
        // }
        Vector2 moveVector = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveVector);
        rb.velocity = direction * speed;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void UseAbility1(InputAction.CallbackContext context)
    {
        //Time.timeScale ==1f to check if a menu is open
        if( context.performed &&isDead == false && Time.timeScale==1f)
        {
            abilityManager.UseAbility1();
        }
    }
    public void UseAbility2(InputAction.CallbackContext context)
    {
        if( context.performed &&isDead == false && Time.timeScale==1f)
        {
            abilityManager.UseAbility2();
        }
    }
    public void UseAbility3(InputAction.CallbackContext context)
    {
        if( context.performed &&isDead == false && Time.timeScale==1f)
        {
            abilityManager.UseAbility3();
        }
    }
    
    public void ShootBall(InputAction.CallbackContext context)
    {
        float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
        if( distanceFromBall<= ballShootRange && isHoldingBall==true && context.performed &&isDead == false && Time.timeScale==1f)
        {
            isHoldingBall = false;
            Vector3 ballPosition = ball.transform.position;
            Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition)-new Vector3(0,0,playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
            Vector3 shootDirection = (mousePosition - ballPosition).normalized;
            ball.GetComponent<Rigidbody2D>().velocity = shootDirection * ballShootSpeed;
            print("Shootball!!");
        }
    }
    public void HoldBall(InputAction.CallbackContext context)
    {
        //on button press
        // if in ballradius, ball moves towards closest spot in ballholding radius around player
        // ball will stay near player and follow cursor while rotating around player in radius
        // turn on isHolding bool
        // allow for drawing trajectory line with isHolding bool
        //allow for Shooting ball in ball function with isHolding bool
        
        float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
        if (distanceFromBall<= ballHoldRange && context.performed &&isDead == false && Time.timeScale==1f)
        {
            print("hold ball used");
            StartCoroutine(HoldCoroutine());
        }
    
    }

    public IEnumerator HoldCoroutine()
    {
        isHoldingBall = true; 
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        float startTime = Time.time;
        float endTime = startTime +holdDuration;
        while (Time.time < startTime + holdDuration && isHoldingBall == true)
        {
            float remainingTime = endTime - Time.time;
            yield return null;
        }
        isHoldingBall = false; 
    }

    public void DrawBallTrajectoryLines()
    {
        float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
        float maxLineDistance  = 30.0f;
        float currentLineDistance = 0.0f;
        int trajectoryBounces = 1;

        if (distanceFromBall <= ballShootRange && !isDead)
        {
            Vector3 ballPosition = ball.transform.position;
            Vector3 mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, playerCamera.ScreenToWorldPoint(Input.mousePosition).z);
            Vector3 ballDirection = (mousePosition - ballPosition).normalized;

            while (currentLineDistance < maxLineDistance && trajectoryBounces <= 6)
            {
                Vector3 rayCastOffset  = ballDirection * 0.05f;
                RaycastHit2D hit = Physics2D.Raycast(ballPosition + rayCastOffset, ballDirection, maxLineDistance, wallLayerMask | enemyLayerMask);
                // print("Ray number: " + trajectoryBounces + " casted");
                if (hit.collider != null)
                {
                    Vector3 wallNormal = hit.normal;
                    Vector3 collisionPosition = hit.point;
                    float lineLength = (collisionPosition - ballPosition).magnitude;
                    // print("Linelength: "+ lineLength);
                    if(lineLength ==0.0f)
                    {
                        // print("line length is zero");
                    }
                    if ((currentLineDistance + lineLength) < maxLineDistance)
                    {
                        currentLineDistance += lineLength;
                        // print("Ray number: " + trajectoryBounces + " casted" + " currentLineDistance + lineLength: " + currentLineDistance + lineLength +"maxLineDistance: " + maxLineDistance );
                        Debug.DrawLine(ballPosition, collisionPosition, Color.red);
                        
                    }
                    else if((currentLineDistance + lineLength) > maxLineDistance)
                    {
                        // print("Draw leftover line");
                        float leftOverLineDistance = maxLineDistance - currentLineDistance;
                        Debug.DrawLine(ballPosition, ballPosition + ballDirection * leftOverLineDistance, Color.red);
                        currentLineDistance += leftOverLineDistance;
                    }

                    ballPosition = collisionPosition;
                    ballDirection = Vector3.Reflect(ballDirection, wallNormal).normalized;
                    
                    // print("currentLineDistance: " + currentLineDistance);
                    trajectoryBounces++;
                }
                else
                {
                    print("collider is null");
                    break; // Break out of the loop if no collision was detected
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        float damageTaken = Mathf.RoundToInt(damage * damageReduction);
        if(playerStats.health + playerStats.defense - damageTaken <=0)
        {
            playerStats.totalDamageTaken += (damageTaken - playerStats.defense)-playerStats.health ;
            playerStats.health =0;
            isDead =true;
        }
        else if(playerStats.health + playerStats.defense - damageTaken <20)
        {
            if(damageTaken >=0)
            {
                playerStats.closeCalls +=1;
            }
            
            playerStats.health-=(damageTaken - playerStats.defense);
        }
        else
        {
            playerStats.health-=(damageTaken - playerStats.defense);
        }
        if(damageTaken >=0)// if there is no damage reduction/masochist mastery is not activated
        {
            playerStats.totalDamageTaken += (damageTaken - playerStats.defense);
        }
        if (isColorShifted == false) 
        {
        StartCoroutine(ShiftColor(redShift, 0.15f));
        }
    }

    public void UseMagika(float abilityCost)
    {
        if(playerStats.magika - abilityCost >=0)
        {
            playerStats.totalMagikaUsed += abilityCost;
            playerStats.magika-=abilityCost;
        }
    }

    public void HealHealth(float healAmount)
    {
        if (playerStats.health + healAmount >=playerStats.maxHealth)
           {
                playerStats.health = playerStats.maxHealth;
           }
        else
        {
            playerStats.health += healAmount;
        }
    }

    public void HealMagika(float healMagikaAmount)
    {
        if (playerStats.magika + healMagikaAmount >=playerStats.maxMagika)
           {
                playerStats.magika = playerStats.maxMagika;
           }
        else
        {
            playerStats.magika += healMagikaAmount;
        }
    }

    public void PassiveMagikaRegeneration()
    {
        if(rb.velocity.magnitude!=0f)
        {
            if (playerStats.magika + magikaRegen >=playerStats.maxMagika)
            {
                playerStats.magika = playerStats.maxMagika;
            }
            else
            {
                playerStats.magika += magikaRegen;
            }
        }
    }

    public void ResetPlayerStats()
    {
        isDead=false;
        playerStats.health = playerStats.maxHealth;
        playerStats.magika = playerStats.maxMagika;
        playerStats.totalDamageTaken = 0;
        playerStats.totalMagikaUsed = 0;
        // abilityManager.SetEquipedAbilites();
    }

    public void IncrementSpikeTileCount() {
        numSpikeTiles++;
    }

    public void DecrementSpikeTileCount() {
        numSpikeTiles--;
        if (numSpikeTiles < 0) {
            Debug.LogError("numSpikeTiles should never be negative");
            numSpikeTiles = 0;
        }
    }
}