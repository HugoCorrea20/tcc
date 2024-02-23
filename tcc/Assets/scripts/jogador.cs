using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jogador : MonoBehaviour
{
    public float speed = 5f; // Velocidade de movimento do personagem

    private Rigidbody2D rb; // Referência ao componente Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtém o componente Rigidbody2D do objeto
    }

    void Update()
    {
        // Captura os inputs de movimento
        float moveHorizontal = Input.GetAxis("Horizontal");
        

        // Calcula a direção do movimento
        Vector2 movement = new Vector2(moveHorizontal,0f);

        // Aplica o movimento ao Rigidbody2D
        rb.velocity = movement * speed;
    }

}
