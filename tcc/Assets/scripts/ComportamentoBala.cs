using UnityEngine;

public class ComportamentoBala : MonoBehaviour
{
    public float limiteDeX = 100f;
    public float tempoDeVida = 3f; // Tempo de vida da bala em segundos
    private float tempoRestante; // Tempo restante da bala

    void Start()
    {
        tempoRestante = tempoDeVida; // Inicializa o tempo restante com o tempo de vida total
    }

    void Update()
    {
        
        if ( tempoRestante <= 0f)
        {
            // Destroi a bala
            Destroy(gameObject);
        }

        // Decrementa o tempo restante
        tempoRestante -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("balainimigo"))
        {
            Destroy(collision.gameObject);
        }
    }
}
