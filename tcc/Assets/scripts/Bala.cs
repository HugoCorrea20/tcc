using UnityEngine;

public class Bala : MonoBehaviour
{
    

    void Start()
    {
        // Destroi a bala ap�s alguns segundos para evitar vazamentos de mem�ria
        Destroy(gameObject, 3f);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("espada"))
        {
            Destroy(gameObject);
        }

    }
}
