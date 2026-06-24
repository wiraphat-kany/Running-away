using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    public float speed = 3f;
    public float stoppingDistance = 1f;
    public float jumpForce = 5f;

    private Rigidbody2D rb;
    private bool grounded = true;
    
    bool isChasingPlayer = false;
    [SerializeField] TextMeshProUGUI countdownText;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChasePlayerDelay());
        StartCoroutine(ShowCountdown()); 
      
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player");
        //if (isChasingPlayer) {
        if (isChasingPlayer) 
        {
            // move enemy towards player
            Vector2 direction = (player.transform.position - transform.position).normalized; 
            float distance = Vector2.Distance(player.transform.position, transform.position);
            
            if (distance > stoppingDistance)
            {
                transform.Translate(direction * speed * Time.deltaTime);
            }
            
            if (player.transform.position.y > transform.position.y && grounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
        
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("NextScene");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Lose");
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
    
    IEnumerator ChasePlayerDelay() {
        yield return new WaitForSeconds(1);
        isChasingPlayer = true;
        
        // show text saying "Enemy will chase you in 2, 1..."
    }
    IEnumerator ShowCountdown()
    {
        for (int i = 1; i >= 0; i--)
        {
            countdownText.text = "Enemy will chase you in " + i.ToString() + "...";
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "";
    }
   
}

