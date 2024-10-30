using System.Collections;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float velocidade = 2f; // Velocidade do inimigo
    public float limiteEsquerdo = -5f; // Limite esquerdo do movimento
    public float limiteDireito = 5f; // Limite direito do movimento
    public GameObject bulletPrefab; // Prefab da bala
    public Transform firePointLeft; // Ponto de origem do tiro para a esquerda
    public Transform firePointRight; // Ponto de origem do tiro para a direita
    public float fireRate = 3f; // Taxa de disparo (em segundos)
    public float bulletSpeed = 5f; // Velocidade da bala
    public LayerMask playerLayer; // Camada do jogador
    public float maxDistance = 10f; // Distância máxima de visão
    public int danorecibido = 10;
    private int direcao = 1; // 1 para direita, -1 para esquerda
    private GameObject player; // Referência ao jogador
    private Transform currentFirePoint; // Ponto de origem atual do tiro
    public bool isAlert = false; // Verifica se o inimigo está alerta
    public Transform heatlhbar; //barra verde
    public GameObject heatltbarobject; // objeto pai das barras
    private Vector3 heatltbarScale; //tamanho da barra
    private float heathpercent; // percetual de vida para o calculo do tamanho da barra 
    public AudioSource tirosom;
    private SpriteRenderer spriteRenderer;
    private Animator animator; // Adiciona um campo para o Animator
    Color originalcolor;

    void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating("Shoot", 0f, fireRate);
        player = GameObject.FindGameObjectWithTag("Player"); // Encontrar o jogador pelo tag
        heatltbarScale = heatlhbar.localScale;
        heathpercent = heatltbarScale.x / currentHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalcolor = spriteRenderer.material.color;
        animator = GetComponent<Animator>(); // Inicialize o Animator
    }

    void UpdateHealthbar()
    {
        heatltbarScale.x = heathpercent * currentHealth;
        heatlhbar.localScale = heatltbarScale;
    }

    void Update()
    {
        if (!isAlert) // Move apenas se não estiver alerta
        {
            // Move o inimigo na direção atual
            transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);
            animator.SetBool("movendo", true); // Ativa a animação de movimento

            // Verifica se o inimigo atingiu um dos limites
            if (transform.position.x <= limiteEsquerdo)
            {
                direcao = 1; // Altera a direção para a direita
                transform.localScale = new Vector3(1, 1, 1); // Define o flip para a direita
            }
            else if (transform.position.x >= limiteDireito)
            {
                direcao = -1; // Altera a direção para a esquerda
                transform.localScale = new Vector3(-1, 1, 1); // Define o flip para a esquerda
            }
        }
        else
        {
            animator.SetBool("movendo", false); // Desativa a animação de movimento
            
        }
    }

    void Shoot()
    {
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= maxDistance)
        {
            isAlert = true;

            Vector2 directionToPlayer = player.transform.position - transform.position;
            if (directionToPlayer.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Flip para a esquerda
                currentFirePoint = firePointLeft;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); // Sem flip (direita)
                currentFirePoint = firePointRight;
            }
            animator.SetTrigger("tiro");
            tirosom.Play();
            GameObject bullet = Instantiate(bulletPrefab, currentFirePoint.position, currentFirePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (currentFirePoint == firePointLeft)
                {
                    rb.velocity = -currentFirePoint.right * bulletSpeed;
                }
                else
                {
                    rb.velocity = currentFirePoint.right * bulletSpeed;
                }
            }
        }
        else
        {
            isAlert = false;
            animator.SetBool("movendo", true);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthbar();

        if (currentHealth <= 0)
        {
            Die();
        }
        StopCoroutine(BlinkRed());
        StartCoroutine(BlinkRed());
    }

    IEnumerator BlinkRed()
    {
        float blinkDuration = 0.1f; // Duração de cada "piscar"

        for (int i = 0; i < 5; i++) // Piscar 5 vezes
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(blinkDuration);
            spriteRenderer.color = originalcolor;
            yield return new WaitForSeconds(blinkDuration);
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
