using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jacare : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isAlert = false; // Verifica se o inimigo está alerta
    private int direcao = 1; // 1 para direita, -1 para esquerda
    public float velocidade = 2f; // Velocidade do inimigo
    public float limiteEsquerdo = -5f; // Limite esquerdo do movimento
    public float limiteDireito = 5f; // Limite direito do movimento
    public int danorecibido = 10;
    public Transform heatlhbar; //barra verde
    public GameObject heatltbarobject; // objeto pai das barras 
    private Vector3 heatltbarScale; //tamanho da barra
    private float heathpercent;   // percetual de vida para o calculo  do tamanho da barra 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        heatltbarScale = heatlhbar.localScale;
        heathpercent = heatltbarScale.x / currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlert) // Move apenas se não estiver alerta
        {
            // Move o inimigo na direção atual
            transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

            // Verifica se o inimigo atingiu um dos limites
            if (transform.position.x <= limiteEsquerdo)
            {
                direcao = 1; // Altera a direção para a direita
                             // Define o flip para a direita
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x >= limiteDireito)
            {
                direcao = -1; // Altera a direção para a esquerda
                              // Define o flip para a esquerda
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    void UpdateHealthbar()
    {
        heatltbarScale.x = heathpercent * currentHealth;
        heatlhbar.localScale = heatltbarScale;
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthbar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            Destroy(collision.gameObject);
            TakeDamage(danorecibido);
        }
    }
}
