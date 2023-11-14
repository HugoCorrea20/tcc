using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    public float velocidade = 5f; // Velocidade de movimento do inimigo
    public float distancia = 5f; // Distância máxima que o inimigo vai percorrer

    private bool moveDireita = true;
    private float posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position.x; // Guarda a posição inicial do inimigo
    }

    void Update()
    {
        if (moveDireita)
        {
            // Move para a direita
            transform.Translate(Vector3.right * velocidade * Time.deltaTime);

            // Verifica se atingiu a distância máxima para a direita
            if (transform.position.x >= posicaoInicial + distancia)
            {
                moveDireita = false; // Muda a direção para a esquerda
            }
        }
        else
        {
            // Move para a esquerda
            transform.Translate(Vector3.left * velocidade * Time.deltaTime);

            // Verifica se atingiu a distância máxima para a esquerda
            if (transform.position.x <= posicaoInicial - distancia)
            {
                moveDireita = true; // Muda a direção para a direita
            }
        }
    }
}

