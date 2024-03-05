using UnityEngine;

public class ComportamentoBala : MonoBehaviour
{
    public float limiteDeX = 100f;

    void Update()
    {
        // Verifique se a posi��o X da bala ultrapassou o limite
        if (transform.position.x > limiteDeX)
        {
            // Destrua a bala
            Destroy(gameObject);
        }
    }
}
