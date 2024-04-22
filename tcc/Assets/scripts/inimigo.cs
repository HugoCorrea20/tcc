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
    private bool isAlert = false; // Verifica se o inimigo está alerta
    public Transform heatlhbar; //barra verde
    public GameObject heatltbarobject ; // objeto pai das barras 

    private Vector3 heatltbarScale; //tamanho da barra
    private float heathpercent;   // percetual de vida para o calculo  do tamanho da barra 

    void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating("Shoot", 0f, fireRate);
        player = GameObject.FindGameObjectWithTag("Player"); // Encontrar o jogador pelo tag
        heatltbarScale = heatlhbar.localScale;
        heathpercent = heatltbarScale.x / currentHealth;

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


    void Shoot()
    {
        // Verifica se o jogador está dentro da distância máxima de visão
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= maxDistance)
        {
            // Marca o inimigo como alerta
            isAlert = true;

            // Calcula a posição relativa do jogador em relação ao inimigo
            Vector2 directionToPlayer = player.transform.position - transform.position;

            // Atualiza o flip do inimigo com base na posição relativa do jogador
            if (directionToPlayer.x < 0) // Se o jogador estiver à esquerda do inimigo
            {
                transform.localScale = new Vector3(-1, 1, 1); // Flip para a esquerda
                currentFirePoint = firePointLeft;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); // Sem flip (direita)
                currentFirePoint = firePointRight;
            }

            // Instancia a bala
            GameObject bullet = Instantiate(bulletPrefab, currentFirePoint.position, currentFirePoint.rotation);

            // Adiciona um Rigidbody2D à bala
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Aplica uma velocidade à bala na direção do transform atual
                if (currentFirePoint == firePointLeft) // Se o ponto de origem for o da esquerda, inverte a direção
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
