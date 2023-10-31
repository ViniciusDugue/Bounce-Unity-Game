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
    // public TextMeshProUGUI BounceText;
    private Vector2 moveInput;
    private GameObject player;
    public Player_input playerControls;
    private InputAction pause;
    private PauseMenu pauseMenu;
    public Camera playerCamera;
    public PlayerCamera playerCameraScript;
    public bool isDead = false;
    private Vector3 mousePosition;
    private AbilityManager abilityManager;
    [HideInInspector] public SpriteRenderer playerSpriteRenderer;
    private DeathMenu deathMenu;
    
    private InputAction ballShootInput;
    public GameObject ball;
    public BallController ballScript;

    public float maxBallSpeed;
    public float ballShootSpeed;
    public float ballShootRange;
    public bool canShootBall = true; 
    public float shootingBallCooldownDuration;

    private InputAction ballHoldInput;
    public float ballHoldRange;
    public float ballHoldDistance;
    public bool isHoldingBall = false;
    

    private InputAction ballChargeInput;
    public bool isChargingBall = false;
    public float maxChargeDuration;
    public float storedChargeSpeed;

    public void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        deathMenu = FindObjectOfType<DeathMenu>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerControls =  new Player_input();
        pause = playerControls.Player.Pause;
        abilityManager =  GameObject.FindGameObjectWithTag("AbilityManager").GetComponent<AbilityManager>();
        
        ability1Input = playerControls.Player.Ability1;
        ability2Input = playerControls.Player.Ability2;
        ability3Input = playerControls.Player.Ability3;
        ballShootInput =  playerControls.Player.ShootBall;
        ballHoldInput = playerControls.Player.HoldBall;
        ballChargeInput = playerControls.Player.ChargeBall;
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
        
        healthText.text = Mathf.Round(health).ToString() + " / " + maxHealth.ToString();
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
        ballChargeInput.Enable();
        ballChargeInput.performed +=ChargeBall;
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
        ballChargeInput.Disable();
        ballChargeInput.performed -=ChargeBall;
        Debug.Log("Player disabled");

    }

    public void SpawnPlayerAtSpawnBox()
    {
        print("player spawned at spawnbox");
        Vector3 spawnLocation =  GameObject.FindGameObjectWithTag("PlayerSpawn").transform.position;
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
        
        Vector2 moveVector = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveVector);
        rb.velocity = direction * speed;
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

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void HoldBall(InputAction.CallbackContext context)
    {
        //on button press
        // if in ballradius, ball moves towards closest spot in ballholding radius around player
        // ball will stay near player and follow cursor while rotating around player in radius
        // turn on isHolding bool
        // allow for drawing trajectory line with isHolding bool
        //allow for Shooting ball in ball function with isHolding bool
        print("hold ball used");
        float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
        if (distanceFromBall<= ballHoldRange && context.performed && isDead == false && Time.timeScale==1f)
        {
            isHoldingBall = true;
        }
    }

    public void ChargeBall(InputAction.CallbackContext context)
    {
        // while holding down shift, stored velocity increases steadily proportionally to max velocity.
        // ball chargetime =  5f
        // Every notch/level achieved in the bounce meter is simply cosmetic and just represents a new velocity 
        // Each notch is of length= maxvelocity/5. When stored velocity% notchlength = 0:shake camera
        // if isbeingheld is false or the max hold time/ max velocity is reached, set the ball velocity to charged velocity
        // ball velocity cannot go over max velocity


        ballScript.ResetBounceCombo();
        float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
        if(isHoldingBall == true && context.performed && isDead == false && Time.timeScale==1f)
        {
            StartCoroutine(ballScript.ChargeBallCoroutine());
        }
    }

    public void ShootBall(InputAction.CallbackContext context)
    {
        ballScript.ResetBounceCombo();
        float distanceFromBall = Vector3.Distance(player.transform.position, ball.transform.position);
        if( distanceFromBall<= ballShootRange && canShootBall == true && context.performed && isDead == false && Time.timeScale==1f)
        {
            ballScript.ShootBallInMouseDirection();
        }
    }

    public void TakeDamage(float damage)
    {
        float damageTaken = Mathf.RoundToInt(damage *  (1.0f- damageReduction));
        if(health + defense - damageTaken <=0)
        {
            totalDamageTaken += (damageTaken - defense)-health ;
            health =0;
            isDead =true;
        }
        else if(health + defense - damageTaken <20)
        {
            if(damageTaken >=0)
            {
                closeCalls +=1;
            }
            health-=(damageTaken - defense);
        }
        else
        {
            health-=(damageTaken - defense);
        }
        if(damageTaken >=0)// if there is no damage reduction/masochist mastery is not activated
        {
            totalDamageTaken += (damageTaken - defense);
        }
        if (isColorShifted == false) 
        {
            playerCameraScript.ShakeCamera(invulnerabilityShakeDuration);
            StartCoroutine(Invulnerability(whiteShift, invulnerabilityDuration, invulnurabilityFlashCount));
        }
    }

    public void HealHealth(float healAmount)
    {
        if (health + healAmount >=maxHealth)
           {
                health = maxHealth;
           }
        else
        {
            health += healAmount;
        }
    }

    public void ResetPlayerStats()
    {
        isDead=false;
        health = maxHealth;
        storedChargeSpeed= 0f;
        totalDamageTaken = 0;
        // abilityManager.SetEquipedAbilites();
    }

    
}