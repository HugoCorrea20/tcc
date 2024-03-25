using UnityEngine;
using UnityEngine.UI;

public class imagem : MonoBehaviour
{
    public float fadeDuration = 2.0f; // Duração da transição de opacidade
    public Image image; // Referência à imagem que você deseja controlar

    private bool fading = false;
    private Color originalColor;
    private float timer = 0.0f;

    void Start()
    {
        originalColor = image.color;
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1); // Define a opacidade inicial como máximo
        StartFade();
    }

    void Update()
    {
        if (fading)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / fadeDuration;
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, normalizedTime)); // Interpola entre a opacidade original e zero

            if (normalizedTime >= 1.0f)
            {
                fading = false;
            }
        }
    }

    void StartFade()
    {
        fading = true;
        timer = 0.0f; // Reseta o temporizador
    }
}
