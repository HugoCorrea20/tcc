using UnityEngine;

public class inimigo : MonoBehaviour
{
    public int maxHealth = 100;
    public  int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
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
