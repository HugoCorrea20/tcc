using UnityEngine;

public class inimigo : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float velocidade = 2f; // Velocidade do inimigo
    public float limiteEsquerdo = -5f; // Limite esquerdo do movimento
    public float limiteDireito = 5f; // Limite direito do movimento

    private int direcao = 1; // 1 para direita, -1 para esquerda

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Move o inimigo na direção atual
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

        // Verifica se o inimigo atingiu um dos limites
        if (transform.position.x <= limiteEsquerdo)
        {
            direcao = 1; // Altera a direção para a direita
        }
        else if (transform.position.x >= limiteDireito)
        {
            direcao = -1; // Altera a direção para a esquerda
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bala"))
        {
            Destroy(other.gameObject);
            TakeDamage(10);
        }
    }
}
