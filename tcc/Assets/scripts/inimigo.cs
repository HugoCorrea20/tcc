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
    public float maxDistance = 10f; // Dist�ncia m�xima de vis�o
    public int danorecibido = 10;
    private int direcao = 1; // 1 para direita, -1 para esquerda
    private GameObject player; // Refer�ncia ao jogador
    private Transform currentFirePoint; // Ponto de origem atual do tiro
    private bool isAlert = false; // Verifica se o inimigo est� alerta

    void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating("Shoot", 0f, fireRate);
        player = GameObject.FindGameObjectWithTag("Player"); // Encontrar o jogador pelo tag
    }

    void Update()
    {
        if (!isAlert) // Move apenas se n�o estiver alerta
        {
            // Move o inimigo na dire��o atual
            transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

            // Verifica se o inimigo atingiu um dos limites
            if (transform.position.x <= limiteEsquerdo)
            {
                direcao = 1; // Altera a dire��o para a direita
            }
            else if (transform.position.x >= limiteDireito)
            {
                direcao = -1; // Altera a dire��o para a esquerda
            }
        }
    }

    void Shoot()
    {
        // Verifica se o jogador est� dentro da dist�ncia m�xima de vis�o
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= maxDistance)
        {
            // Marca o inimigo como alerta
            isAlert = true;

            // Calcula a posi��o relativa do jogador em rela��o ao inimigo
            Vector2 directionToPlayer = player.transform.position - transform.position;

            // Determina o ponto de origem do tiro com base na posi��o relativa do jogador
            if (directionToPlayer.x < 0) // Se o jogador estiver � esquerda do inimigo
            {
                currentFirePoint = firePointLeft;
            }
            else
            {
                currentFirePoint = firePointRight;
            }

            // Instancia a bala
            GameObject bullet = Instantiate(bulletPrefab, currentFirePoint.position, currentFirePoint.rotation);

            // Adiciona um Rigidbody2D � bala
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Aplica uma velocidade � bala na dire��o do transform atual
                if (currentFirePoint == firePointLeft) // Se o ponto de origem for o da esquerda, inverte a dire��o
                {
                    rb.velocity = -currentFirePoint.right * bulletSpeed;
                }
                else
                {
                    rb.velocity = currentFirePoint.right * bulletSpeed;
                }
            }
        }
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

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
