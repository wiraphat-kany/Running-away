using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollidersTest : MonoBehaviour
{


    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag( "Player"))
        {
            Debug.Log("NextScene");
            SceneManager.DontDestroyOnLoad(rb2d);
        }
    }
}
