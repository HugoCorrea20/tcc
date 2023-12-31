using UnityEngine;

public class ControlePersonagem : MonoBehaviour
{
    public GameObject personagem1Prefab; // Prefab do Personagem 1
    public GameObject personagem2Prefab; // Prefab do Personagem 2
    public GameObject personagem3Prefab; // Prefab do Personagem 3
    public GameObject bot�o1;
    public GameObject bot�o2;
    public GameObject bot�o3;

    private GameObject personagemAtual; // Refer�ncia ao personagem atualmente ativo

    void Start()
    {
       
    }

    void SpawnarPersonagem(GameObject prefab, Vector3 spawnPosition)
    {
        // Se j� existe um personagem, destrua-o antes de spawnar o novo
        if (personagemAtual != null)
        {
            Destroy(personagemAtual);
        }

        // Spawnar o novo personagem na posi��o desejada
        personagemAtual = Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    public void EscolherPersonagem1()
    {
        Vector3 spawnPosition = new Vector3(0f, 5.8f, 0f); // Defina a posi��o de spawn para o Personagem 1
        SpawnarPersonagem(personagem1Prefab, spawnPosition);
        bot�o1.SetActive(false);
        bot�o2.SetActive(false);
        bot�o3.SetActive(false);
    }

    public void EscolherPersonagem2()
    {
        Vector3 spawnPosition = new Vector3(0f, 5.8f, 0f); // Defina a posi��o de spawn para o Personagem 2
        SpawnarPersonagem(personagem2Prefab, spawnPosition);
        bot�o1.SetActive(false);
        bot�o2.SetActive(false);
        bot�o3.SetActive(false);
    }

    public void EscolherPersonagem3()
    {
        Vector3 spawnPosition = new Vector3(0f, 5.8f, 0f); // Defina a posi��o de spawn para o Personagem 3
        SpawnarPersonagem(personagem3Prefab, spawnPosition);
        bot�o1.SetActive(false);
        bot�o2.SetActive(false);
        bot�o3.SetActive(false);
    }
}
