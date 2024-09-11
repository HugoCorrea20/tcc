using System.Collections;
using UnityEngine;

public class jacare : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool isAlert = false;
    private Transform player; // Referência ao jogador
    private int direcao = 1;
    public float velocidade = 2f;
    public float limiteEsquerdo = -5f;
    public float limiteDireito = 5f;
    public int danorecibido = 10;
    public float damageInterval = 5f; // Intervalo de dano
    private bool playerInContact = false; // Verifica se o jogador está em contato

    public Transform heatlhbar;
    public GameObject heatltbarobject;
    private Vector3 heatltbarScale;
    private float heathpercent;
    public float alcanceDetecao = 5;

    public AudioSource alertSound; // Referência ao AudioSource

    void Start()
    {
        currentHealth = maxHealth;
        heatltbarScale = heatlhbar.localScale;
        heathpercent = heatltbarScale.x / currentHealth;

        player = GameObject.FindGameObjectWithTag("Player").transform; // Encontrar o jogador
      
    }

    void Update()
    {
        if (!isAlert)
        {
            transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

            if (transform.position.x <= limiteEsquerdo)
            {
                direcao = 1;
            }
            else if (transform.position.x >= limiteDireito)
            {
                direcao = -1;
            }

            if (direcao == 1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direcao == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            // Verifica se o jogador está dentro do alcance de detecção
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer < alcanceDetecao) // Verifica se o jogador está dentro do alcance de detecção
            {
                isAlert = true;
                alertSound.Play(); // Toca o som quando o jogador é detectado
            }
        }
        else
        {
            // Movimento apenas na direção x
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidade * Time.deltaTime);

            // Verifica se o jogador está fora do alcance de detecção
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > alcanceDetecao) // Verifica se o jogador está fora do alcance de detecção
            {
                isAlert = false;
            }

            if (direcao == 1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (direcao == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

  

    IEnumerator CauseDamagePeriodically(jogador player)
    {
        while (playerInContact)
        {
            player.TakeDamage(danorecibido);
            yield return new WaitForSeconds(damageInterval);
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
        // Verifica se o jacaré colidiu com o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInContact = true;
            StartCoroutine(CauseDamagePeriodically(collision.gameObject.GetComponent<jogador>()));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se o jacaré saiu da colisão com o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInContact = false;
            StopCoroutine(CauseDamagePeriodically(collision.gameObject.GetComponent<jogador>()));
        }
    }
}
