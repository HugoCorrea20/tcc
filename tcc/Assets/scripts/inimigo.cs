using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade de movimento do inimigo
    public float distancia = 5f; // Dist�ncia m�xima que o inimigo vai percorrer

    private bool moveDireita = true;
    private float posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position.x; // Guarda a posi��o inicial do inimigo
    }

    void Update()
    {
        if (moveDireita)
        {
            // Move para a direita
            transform.Translate(Vector3.right * velocidade * Time.deltaTime);

            // Verifica se atingiu a dist�ncia m�xima para a direita
            if (transform.position.x >= posicaoInicial + distancia)
            {
                moveDireita = false; // Muda a dire��o para a esquerda
            }
        }
        else
        {
            // Move para a esquerda
            transform.Translate(Vector3.left * velocidade * Time.deltaTime);

            // Verifica se atingiu a dist�ncia m�xima para a esquerda
            if (transform.position.x <= posicaoInicial - distancia)
            {
                moveDireita = true; // Muda a dire��o para a direita
            }
        }
    }
}

