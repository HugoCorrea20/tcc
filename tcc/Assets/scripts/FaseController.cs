using UnityEngine;
using UnityEngine.UI; // Para trabalhar com UI, como o Text
using TMPro;

public class FaseController : MonoBehaviour
{
    // Referência ao componente Text (título da fase)
    public TextMeshProUGUI tituloFase;

    // Start é chamado quando a cena começa
    void Start()
    {
        // Verifica se o título foi atribuído na interface do Unity
        if (tituloFase != null)
        {
            // Chama o método para esconder o título após 5 segundos
            Invoke("EsconderTitulo", 2f);
        }
        else
        {
            Debug.LogError("O título da fase não está atribuído!");
        }
    }

    // Método para esconder o título
    void EsconderTitulo()
    {
        tituloFase.gameObject.SetActive(false);
    }
}
