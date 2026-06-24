using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool facingRight = true;
    private Rigidbody2D rb;
    public Transform leftSensor;
    public Transform rightSensor;
    public float sensorWidth = 0.5f;
    public LayerMask obstacleLayer;
    [SerializeField] private float changeDirectionTimer = 0.5f;
    private SpriteRenderer sp;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Horizontal movement
        float hInput = facingRight ? 1f : -1f;
        rb.velocity = new Vector2(hInput * moveSpeed, rb.velocity.y);

        // Obstacle detection
        bool obstacleOnLeft = Physics2D.OverlapBox(leftSensor.position, new Vector2(sensorWidth, 0.1f), 0f, obstacleLayer);
        bool obstacleOnRight = Physics2D.OverlapBox(rightSensor.position, new Vector2(sensorWidth, 0.1f), 0f, obstacleLayer);

        if (obstacleOnLeft && changeDirectionTimer <= 0)
        {
            facingRight = true;
            changeDirectionTimer = 0.5f; // Reset timer
            sp.flipX = false;
        }
        else if (obstacleOnRight && changeDirectionTimer <= 0)
        {
            facingRight = false;
            changeDirectionTimer = 0.5f; // Reset timer
            sp.flipX = true;
        }

        if (changeDirectionTimer > 0)
        {
            changeDirectionTimer -= Time.deltaTime;
        }
        
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
       
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("NextScene");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        }

    }
}
