using UnityEngine;

public class Bala : MonoBehaviour
{
    public int dano = 10; // Dano causado pela bala

    void Start()
    {
        // Destroi a bala ap�s alguns segundos para evitar vazamentos de mem�ria
        Destroy(gameObject, 2f);
    }
}
