using UnityEngine;

public class ControlePersonagem : MonoBehaviour
{
    public GameObject personagem1Prefab; // Prefab do Personagem 1
    public GameObject personagem2Prefab; // Prefab do Personagem 2
    public GameObject personagem3Prefab; // Prefab do Personagem 3
    public GameObject botão1;
    public GameObject botão2;
    public GameObject botão3;

    private GameObject personagemAtual; // Referência ao personagem atualmente ativo

    void Start()
    {
       
    }

    void SpawnarPersonagem(GameObject prefab, Vector3 spawnPosition)
    {
        // Se já existe um personagem, destrua-o antes de spawnar o novo
        if (personagemAtual != null)
        {
            Destroy(personagemAtual);
        }

        // Spawnar o novo personagem na posição desejada
        personagemAtual = Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    public void EscolherPersonagem1()
    {
        Vector3 spawnPosition = new Vector3(0f, 5.8f, 0f); // Defina a posição de spawn para o Personagem 1
        SpawnarPersonagem(personagem1Prefab, spawnPosition);
        botão1.SetActive(false);
        botão2.SetActive(false);
        botão3.SetActive(false);
    }

    public void EscolherPersonagem2()
    {
        Vector3 spawnPosition = new Vector3(0f, 5.8f, 0f); // Defina a posição de spawn para o Personagem 2
        SpawnarPersonagem(personagem2Prefab, spawnPosition);
        botão1.SetActive(false);
        botão2.SetActive(false);
        botão3.SetActive(false);
    }

    public void EscolherPersonagem3()
    {
        Vector3 spawnPosition = new Vector3(0f, 5.8f, 0f); // Defina a posição de spawn para o Personagem 3
        SpawnarPersonagem(personagem3Prefab, spawnPosition);
        botão1.SetActive(false);
        botão2.SetActive(false);
        botão3.SetActive(false);
    }
}
