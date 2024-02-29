using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jogador : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento do personagem
    public float jumpForce = 10f; // Força do pulo

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
