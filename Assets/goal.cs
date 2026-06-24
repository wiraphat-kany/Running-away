using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class goal : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    // private float countdown = 5f;
    private bool IsCountdown = false;

    private void Start()
    {
        countdownText.enabled = false;
    }

    void Update()
    {

        /* if (IsCountdown)
         {
             countdownText.text = countdown.ToString("0"); // update the countdown text
 
             if (countdown < 0)
             {
                 countdownText.text = "Time's up!";
                 IsCountdown = false;
                 UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
             }
 
             if (countdown > 0)
             {
                 countdown -= Time.deltaTime;
                 
             
             }
             //   else
             //  {
             //       countdownText.text = "Time's up!";
             //      Time.timeScale = 0f;  // stop the game
             //   }
         }
     }*/

         void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player")
            {
              //  IsCountdown = true;
               // countdownText.enabled = true;
            }

        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("NextScene");
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
        }

    }
}
