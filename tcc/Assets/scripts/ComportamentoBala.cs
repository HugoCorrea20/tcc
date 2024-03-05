using UnityEngine;

public class ComportamentoBala : MonoBehaviour
{
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
}
