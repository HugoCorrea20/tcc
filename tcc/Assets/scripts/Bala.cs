using UnityEngine;

public class Bala : MonoBehaviour
{
    public int dano = 10; // Dano causado pela bala

    void Start()
    {
        // Destroi a bala após alguns segundos para evitar vazamentos de memória
        Destroy(gameObject, 2f);
    }
}
