using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class jacare : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isAlert = false;
    private Transform player; // Refer�ncia ao jogador
    private int direcao = 1;
    public float velocidade = 2f;
    public float limiteEsquerdo = -5f;
    public float limiteDireito = 5f;
    public int danorecibido = 10;
    public float damageInterval = 5f; // Intervalo de dano
    private float lastDamageTime; // Tempo do �ltimo dano

    public Transform heatlhbar;
    public GameObject heatltbarobject;
    private Vector3 heatltbarScale;
    private float heathpercent;

    void Start()
    {
        currentHealth = maxHealth;
        heatltbarScale = heatlhbar.localScale;
        heathpercent = heatltbarScale.x / currentHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontrar o jogador
        lastDamageTime = Time.time; // Inicializa o tempo do �ltimo dano
    }

    void Update()
    {
        if (!isAlert)
        {
            transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

            if (transform.position.x <= limiteEsquerdo)
            {
                direcao = 1;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (transform.position.x >= limiteDireito)
            {
                direcao = -1;
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // Verifica se o jogador est� dentro do alcance de detec��o
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer < 5f) // Defina a dist�ncia conforme necess�rio
            {
                isAlert = true;
            }
        }
        else
        {
            // Move em dire��o ao jogador
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocidade * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jacar� colidiu com o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            // Causa dano ao jogador
            collision.gameObject.GetComponent<jogador>().TakeDamage(danorecibido);
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
