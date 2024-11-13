using UnityEngine;
using UnityEngine.UI; // Para trabalhar com UI, como o Text
using TMPro;

public class FaseController : MonoBehaviour
{
    // Refer�ncia ao componente Text (t�tulo da fase)
    public TextMeshProUGUI tituloFase;

    // Start � chamado quando a cena come�a
    void Start()
    {
        // Verifica se o t�tulo foi atribu�do na interface do Unity
        if (tituloFase != null)
        {
            // Chama o m�todo para esconder o t�tulo ap�s 5 segundos
            Invoke("EsconderTitulo", 2f);
        }
        else
        {
            Debug.LogError("O t�tulo da fase n�o est� atribu�do!");
        }
    }

    // M�todo para esconder o t�tulo
    void EsconderTitulo()
    {
        tituloFase.gameObject.SetActive(false);
    }
}
