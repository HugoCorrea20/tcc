using System.Collections;
using UnityEngine;

public class jaquatirica : MonoBehaviour
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
    private bool playerInContact = false; // Verifica se o jogador est� em contato

    public Transform heatlhbar;
    public GameObject heatltbarobject;
    private Vector3 heatltbarScale;
    private float heathpercent;
    public float alcanceDetecao = 5;
    public AudioSource alertSound;
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

            // Verifica se o jogador est� dentro do alcance de detec��o
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer < alcanceDetecao) // Verifica se o jogador est� dentro do alcance de detec��o
            {
                isAlert = true;
                alertSound.Play();
            }
        }
        else
        {
            // Movimento apenas na dire��o x
            Vector2 targetPosition = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, velocidade * Time.deltaTime);

            // Verifica se o jogador est� fora do alcance de detec��o
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > alcanceDetecao) // Verifica se o jogador est� fora do alcance de detec��o
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
       
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
        // Verifica se a jaguatirica colidiu com o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInContact = true;
            StartCoroutine(CauseDamagePeriodically(collision.gameObject.GetComponent<jogador>()));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se a jaguatirica saiu da colis�o com o jogador
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInContact = false;
            StopCoroutine(CauseDamagePeriodically(collision.gameObject.GetComponent<jogador>()));
        }
    }
}
