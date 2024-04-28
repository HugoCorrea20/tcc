using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comportamentobalainimigo : MonoBehaviour
{
    public float velocidade = 10f; // Velocidade da bala

    void Start()
    {
        // Defina a velocidade da bala para que ela vá para a direita
        GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidade;
    }

    public float limiteDeX = 100f;

    void Update()
    {
        // Verifique se a posição X da bala ultrapassou o limite
        if (transform.position.x > limiteDeX)
        {
            // Destrua a bala
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            Destroy(collision.gameObject);
            
        }
    }
}
