using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TrocarDeCena : MonoBehaviour
{
    public string proximaCena; // Nome da próxima cena a ser carregada
    public float tempoDeTransicao = 1f; // Tempo da transição
    public Image imagemTransicao; // Referência para a imagem de transição

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Verifica se o jogador colidiu com o gatilho
        {
            // Inicia a cor da imagem de transição com alpha 0
            Color corInicial = imagemTransicao.color;
            corInicial.a = 0f;
            imagemTransicao.color = corInicial;

            // Inicia a transição
            StartCoroutine(TransicaoParaProximaCena());
        }
    }
    public void IniciarTransicao()
    {
        StartCoroutine(TransicaoParaProximaCena());
    }

    public IEnumerator TransicaoParaProximaCena()
    {
        // Gradualmente aumenta a opacidade da imagem de transição
        float tempoDecorrido = 0f;
        while (tempoDecorrido < tempoDeTransicao)
        {
            // Calcula o progresso da transição
            float progresso = tempoDecorrido / tempoDeTransicao;

            // Interpola a opacidade da cor da imagem
            Color cor = imagemTransicao.color;
            cor.a = Mathf.Lerp(0f, 1f, progresso);
            imagemTransicao.color = cor;

            // Atualiza o tempo decorrido
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        // Garante que a opacidade está no máximo
        Color corFinal = imagemTransicao.color;
        corFinal.a = 1f;
        imagemTransicao.color = corFinal;

        // Carrega a próxima cena após a transição
        SceneManager.LoadScene(proximaCena);
    }
}
