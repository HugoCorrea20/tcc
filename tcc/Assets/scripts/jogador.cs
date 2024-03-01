using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jogador : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento do personagem
    public float jumpForce = 10f; // Força do pulo
    public GameObject bulletPrefab; // Prefab da bala
    public float velocidade_tiro = 10f;
    public Transform bulletSpawnPointRight; // Ponto de spawn da bala quando o jogador está indo para a direita
    public Transform bulletSpawnPointLeft; // Ponto de spawn da bala quando o jogador está indo para a esquerda
    private Rigidbody2D rb; // Referência ao componente Rigidbody2D
    private bool isGrounded; // Verifica se o jogador está no chão


    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D do objeto
    }

    void Update()
    {
        // Captura os inputs de movimento
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Calcula a direção do movimento
        Vector2 movement = new Vector2(moveHorizontal, 0f);

        // Aplica o movimento ao Rigidbody2D
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        // Verifica se o jogador está no chão
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Aplica uma força vertical (para cima) ao Rigidbody2D
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // O jogador já não está mais no chão após o pulo
        }

        // Verifica se o botão direito do mouse foi pressionado
        if (Input.GetMouseButtonDown(1))
        {
            Shoot(moveHorizontal); // Chama a função de atirar e passa a direção do movimento
        }
    }

    // Função para atirar
    void Shoot(float direction)
    {
        // Ajusta a posição de spawn do tiro de acordo com a direção do jogador
        Vector3 spawnPosition;

        if (direction > 0) // Jogador está indo para a direita
        {
            spawnPosition = bulletSpawnPointRight.position;
        }
        else // Jogador está indo para a esquerda
        {
            spawnPosition = bulletSpawnPointLeft.position;
        }

        // Instancia o objeto de bala na posição de spawn determinada
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Determina a direção do tiro com base na direção do movimento do jogador
        Vector2 shootDirection = direction > 0 ? Vector2.right : Vector2.left;

        // Aplica uma força na direção determinada à bala
        bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection * velocidade_tiro, ForceMode2D.Impulse);
    }

    // Verifica se o jogador está colidindo com o chão
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
        }

    }

    // Verifica se o jogador não está mais colidindo com o chão
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = false;
        }
    }
}
