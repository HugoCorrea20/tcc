using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jogador : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento do personagem
    public float jumpForce = 10f; // For�a do pulo

    private Rigidbody2D rb; // Refer�ncia ao componente Rigidbody2D
    private bool isGrounded; // Verifica se o jogador est� no ch�o

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obt�m o componente Rigidbody2D do objeto
    }

    void Update()
    {
        // Captura os inputs de movimento
        float moveHorizontal = Input.GetAxis("Horizontal");

        // Calcula a dire��o do movimento
        Vector2 movement = new Vector2(moveHorizontal, 0f);

        // Aplica o movimento ao Rigidbody2D
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        // Verifica se o jogador est� no ch�o
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // Aplica uma for�a vertical (para cima) ao Rigidbody2D
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false; // O jogador j� n�o est� mais no ch�o ap�s o pulo
        }
    }

    // Verifica se o jogador est� colidindo com o ch�o
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
        }
    }

    // Verifica se o jogador n�o est� mais colidindo com o ch�o
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = false;
        }
    }
}
