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
    public float attackRange = 1.5f; // Ajuste a dist�ncia de ataque conforme necess�rio
    private float lastShootTime;
    public float shootCooldown = 5f; // Tempo de espera entre os tiros
    public float pickupRange = 1.5f; // Ajuste a dist�ncia de pegar o item conforme necess�rio
    public GameObject currentItem; // Vari�vel para armazenar o item atualmente dispon�vel para ser pego
    private bool itemPegado = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastShootTime = -shootCooldown; // Configura o tempo inicial de modo que o jogador possa atirar imediatamente
    }

    void Update()
    {
        if (!PauseMenu.isPaused) // Verifica se o jogo n�o est� pausado
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

            if (Input.GetMouseButtonDown(1) && Time.time - lastShootTime > shootCooldown) // Verifica se o tempo decorrido � maior que o tempo de espera
            {
                Shoot();
                lastShootTime = Time.time; // Atualiza o tempo do �ltimo tiro
            }
            if (Input.GetMouseButtonDown(0)) // Bot�o esquerdo do mouse para ataque de perto
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupItem();
            }
        }
    }
    void PickupItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                // Pegar o item
                currentItem = collider.gameObject;
                // Desative o item (ou destrua-o)
                currentItem.SetActive(false);
                // Execute qualquer l�gica adicional aqui, se necess�rio
                Debug.Log("Item pegado!");
                // Sair do loop ap�s pegar um item
                break;
            }
        }
    }
    void Attack()
    {
        if (!PauseMenu.isPaused) // Verifica se o jogo n�o est� pausado antes de permitir o ataque
        {
            Vector2 attackPosition = transform.position + new Vector3(lastDirection * attackRange, 0f, 0f);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition, 0.5f);

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Inimigo"))
                {
                    collider.GetComponent<inimigo>().TakeDamage(10);
                }
            }
        }
    }

    void Shoot()
    {
        if (!PauseMenu.isPaused) // Verifica se o jogo n�o est� pausado antes de permitir o tiro
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
