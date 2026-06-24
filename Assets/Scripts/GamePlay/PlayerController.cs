using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 10f;
    
    [Space(20)]
    public float boostedSpeed = 2f; // Speed of player movement when boosted
    public float boostDuration = 1f; // Duration of the boost in seconds
    private float boostTimeLeft = 0f;
    
     [Space(20)]
        public float slowSpeed = 3f; // Speed of player movement when boosted
        public float slowDuration = 1f; // Duration of the boost in seconds
        private float slowTimeLeft = 0f;


    [Space(20)]
    public float jumpBoost = 2f;
    public LayerMask jumpPadLayer;
    
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private bool isRunning;

    private BoxCollider2D coll;
    [SerializeField] private LayerMask jumpablaGround;
    
    [HideInInspector] public StaminaController _staminaController;
    
    public float bananaSlowdownFactor = 0.5f;
    public bool isSlowdown = false;

    public NewItemCollector newItemCollector;
    
    
    void Start()
    {
        _staminaController = GetComponent<StaminaController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        newItemCollector = GetComponent<NewItemCollector>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        float hInput = Input.GetAxisRaw("Horizontal");
        float moveSpeedInput = isRunning ? runSpeed : moveSpeed;
            
        if (boostTimeLeft > 0f)
        {
            moveSpeedInput *= boostedSpeed;
            boostTimeLeft -= Time.deltaTime;
        }
        
        if (slowTimeLeft > 0f)
        {
            moveSpeedInput -= slowSpeed;
            slowTimeLeft -= Time.deltaTime;
        }

        
        // Move the player horizontally
        if (!isSlowdown)
        {
            rb.velocity = new Vector2(hInput * moveSpeedInput, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(hInput * moveSpeedInput * bananaSlowdownFactor, rb.velocity.y);
        }
        anim.SetBool("RUN", Mathf.Abs(hInput) > 0);
        
        // Flip the player's sprite based on direction
        if (hInput > 0)
            sr.flipX = false;
        else if (hInput < 0)
            sr.flipX = true;
        
        

        // Jump if the player presses the jump button and is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce + (isInJumpBoostArea() ? jumpBoost : 0f));
                //  isJumping = true;
        }

        // Check if the player is running by holding down the run button
        isRunning = Input.GetKey(KeyCode.LeftShift);
        
        if (!isRunning)
        {
            _staminaController.weAreSprinting = false;
        }

        if (isRunning && rb.velocity.sqrMagnitude > 0)
        {
            if (_staminaController.playerStamina > 0)
            {
                _staminaController.weAreSprinting = true;
                _staminaController.Sprinting();
            }
            else
            {
                isRunning = false;
            }
        }
    }

    private bool IsGround()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpablaGround);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BoostPad")) // Check if collider is a boost
        {
            StartCoroutine(ApplyBoost()); // Start the coroutine to apply boost
        }
        if (collision.CompareTag("SlowPad")) // Check if collider is a boost
        {
            StartCoroutine(ApplySlow()); // Start the coroutine to apply boost
        }
    }
  

    IEnumerator ApplyBoost()
    {
        boostTimeLeft = boostDuration; // Set boost time to duration
        yield return new WaitForSeconds(boostDuration); // Wait for boost duration to end
        boostTimeLeft = 0f; // Reset boost time
    }
    
    IEnumerator ApplySlow()
    {
        slowTimeLeft = slowDuration; // Set boost time to duration
        yield return new WaitForSeconds(slowDuration); // Wait for boost duration to end
        slowTimeLeft = 0f; // Reset boost time
    }
    
    private bool isInJumpBoostArea()
    {
        // Check if the player is in a jump boost area
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f,jumpPadLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("JumpPad"))
            {
                return true;
            }
        }
        return false;
    }

    public void SetRunSpeed(float speed)
    {
        runSpeed = speed;
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("Banana"))
        {
            Debug.Log("Banana");
            if (newItemCollector.shieldTimer <= 0f)
            {
                // Slow down the player
                isSlowdown = true;
                Debug.Log("banana on");

                // Start a coroutine to restore the player's speed after 3 seconds
                StartCoroutine(RestoreSpeedAfterDelay(3f));
            }
            Destroy(col.gameObject);
        }
    }
    
    IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("banana off");
        isSlowdown = false;
    }

    public void SpeedBoost()
    {
        StartCoroutine(ApplyBoost());
    }
    
  
    
}
