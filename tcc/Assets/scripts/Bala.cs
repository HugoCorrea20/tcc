using UnityEngine;

public class Bala : MonoBehaviour
{
    

    void Start()
    {
        // Destroi a bala após alguns segundos para evitar vazamentos de memória
        Destroy(gameObject, 3f);
    }
}
