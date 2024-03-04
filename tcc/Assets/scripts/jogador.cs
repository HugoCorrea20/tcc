using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jogador : MonoBehaviour
{
    public float speed = 5f; 
    public float jumpForce = 10f; 
    public GameObject bulletPrefab; 
    public float velocidade_tiro = 10f;
    public Transform bulletSpawnPointRight; 
    public Transform bulletSpawnPointLeft; 
    private Rigidbody2D rb; 
    private bool isGrounded;
    private float lastDirection = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");

        
        Vector2 movement = new Vector2(moveHorizontal, 0f);


        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

       
        if (moveHorizontal != 0)
        {
            lastDirection = Mathf.Sign(moveHorizontal);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
           
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

       
        if (Input.GetMouseButtonDown(1))
        {
            Shoot(); 
        }
    }


    void Shoot()
    {
        
        Vector3 spawnPosition;

        if (lastDirection > 0) 
        {
            spawnPosition = bulletSpawnPointRight.position;
        }
        else
        {
            spawnPosition = bulletSpawnPointLeft.position;
        }

       
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

       
        Vector2 shootDirection = lastDirection > 0 ? Vector2.right : Vector2.left;

        
        bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection * velocidade_tiro, ForceMode2D.Impulse);
    }

  
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = false;
        }
    }
}
