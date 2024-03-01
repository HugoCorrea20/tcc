using UnityEngine;

public class inimigo : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // M�todo para causar dano ao inimigo
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para lidar com a morte do inimigo
    void Die()
    {
        // Adicione aqui qualquer l�gica que voc� queira executar quando o inimigo morrer,
        // como reproduzir uma anima��o de morte, pontua��o do jogador, etc.
        Destroy(gameObject);
    }

    // Verifica colis�es com balas
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            // Obt�m o componente Bala do objeto colidido
            Bala bala = collision.gameObject.GetComponent<Bala>();

            // Verifica se o componente Bala foi encontrado
            if (bala != null)
            {
                // Chama a fun��o TakeDamage para causar dano ao inimigo
                TakeDamage(bala.dano);

                // Destroi a bala ap�s atingir o inimigo
                Destroy(collision.gameObject);
            }
        }
    }

}
