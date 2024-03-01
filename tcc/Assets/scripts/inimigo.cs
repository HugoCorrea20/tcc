using UnityEngine;

public class inimigo : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Método para causar dano ao inimigo
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para lidar com a morte do inimigo
    void Die()
    {
        // Adicione aqui qualquer lógica que você queira executar quando o inimigo morrer,
        // como reproduzir uma animação de morte, pontuação do jogador, etc.
        Destroy(gameObject);
    }

    // Verifica colisões com balas
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            // Obtém o componente Bala do objeto colidido
            Bala bala = collision.gameObject.GetComponent<Bala>();

            // Verifica se o componente Bala foi encontrado
            if (bala != null)
            {
                // Chama a função TakeDamage para causar dano ao inimigo
                TakeDamage(bala.dano);

                // Destroi a bala após atingir o inimigo
                Destroy(collision.gameObject);
            }
        }
    }

}
