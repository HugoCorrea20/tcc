using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class jogador : MonoBehaviour
{
    public int maxHealth = 100; // Vida m�xima do jogador
    public int currentHealth; // Vida atual do jogador
    public float speed = 5f;
    public float jumpForce = 10f;
    //public GameObject bulletPrefab;
   // public float velocidade_tiro = 10f;
   // public Transform bulletSpawnPointRight;
   // public Transform bulletSpawnPointLeft;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastDirection = 1f;
    public float attackRange = 1.5f; // Ajuste a dist�ncia de ataque conforme necess�rio
    //private float lastShootTime;
    //public float shootCooldown = 5f; // Tempo de espera entre os tiros
    public float pickupRange = 1.5f; // Ajuste a dist�ncia de pegar o item conforme necess�rio
    public  bool itemPegado = false;
    public GameObject currentItem; // Vari�vel para armazenar o item atualmente dispon�vel para ser pego
    public GameObject fimObjeto; // Objeto do fim do jogo
    public TextMeshProUGUI avisoText; // Refer�ncia ao objeto de texto para exibir o aviso
    public int danorecibido = 10;
    public Transform heatlhbar; //barra verde
    public GameObject heatltbarobject; // objeto pai das barras 
    public GameObject p�;
    private Vector3 heatltbarScale; //tamanho da barra
    private float heathpercent;   // percetual de vida para o calculo  do tamanho da barra 
    public string proximaCena; // Nome da pr�xima cena a ser carregada
    public float tempoDeTransicao = 1f; // Tempo da transi��o
    public Image imagemTransicao; // Refer�ncia para a imagem de transi��o
    private Animator animator; // Refer�ncia ao componente Animator
    public bool wasMoving = false; // Indica se o jogador estava se movendo no frame anterior
    public bool papegado =false; 
    public Transform localCavar;
    public GameObject item;
    public GameObject item2;
    public bool itempegado2 = false;
    public GameObject objetoDeAtaque;
    private float vertical;
    private float velociadeescada = 8f;
    private bool escadas;
    private bool escalando;
    public Rigidbody2D playerrb;


    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
       // lastShootTime = -shootCooldown; // Configura o tempo inicial de modo que o jogador possa atirar imediatamente
        heatltbarScale = heatlhbar.localScale;
        heathpercent = heatltbarScale.x / currentHealth;
        animator = GetComponent<Animator>(); // Obt�m o componente Animator
    }
    void UpdateHealthbar()
    {
        heatltbarScale.x = heathpercent * currentHealth;
        heatlhbar.localScale = heatltbarScale;
    }
    void Update()
    {
        if (!PauseMenu.isPaused) // Verifica se o jogo n�o est� pausado
        {
            MovimentarJogador();
            /*
            if (Input.GetMouseButtonDown(1) && Time.time - lastShootTime > shootCooldown) // Verifica se o tempo decorrido � maior que o tempo de espera
            {
                Shoot();
                lastShootTime = Time.time; // Atualiza o tempo do �ltimo tiro
            }
            */
            if (Input.GetMouseButtonDown(0)) // Bot�o esquerdo do mouse para ataque de perto
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (papegado && Vector2.Distance(transform.position, localCavar.position) < 1.5f) // Verifica se o jogador est� segurando a p� e est� perto do local de cavar
                {
                    Cavar(); // Chama a fun��o para cavar
                }
                else
                {
                    PickupItem();
                }
            }
            UpdateAnimations();
            vertical = UnityEngine.Input.GetAxis("Vertical");
            if (escadas && Mathf.Abs(vertical) > 0f)
            {
                escalando = true;
            }
        }
    }

    void Cavar()
    {
        // Ative o item a ser coletado.
        
        item.SetActive(true);
        Debug.Log("Item coletado ap�s cavar!");
    }

    void MovimentarJogador()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        if (moveHorizontal != 0)
        {
            lastDirection = Mathf.Sign(moveHorizontal);
            Flip();
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    void UpdateAnimations()
    {
        // Verifica se o jogador est� se movendo
        bool isMoving = Mathf.Abs(rb.velocity.x) > 0.01f;

        // Atualiza as anima��es com base no estado atual
        if (isMoving)
        {
            animator.SetBool("movendo", true); // Ativa a anima��o de movimento
            animator.SetBool("parado", false); // Desativa a anima��o de parada
        }
        else
        {
            animator.SetBool("movendo", false); // Desativa a anima��o de movimento
            animator.SetBool("parado", !wasMoving); // Ativa a anima��o de parada apenas se o jogador estava se movendo no frame anterior
        }

        // Atualiza o estado anterior de movimento
        wasMoving = isMoving;
    }
    void Flip()
    {
        // Inverte a escala do jogador para mudar a dire��o visualmente
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * lastDirection;
        transform.localScale = scale;
    }

    void PickupItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item")) // Verifica se � um item gen�rico
            {
                // Pegar o item gen�rico
                currentItem = collider.gameObject;
                currentItem.SetActive(false); // Desativa o item
                Debug.Log("Item gen�rico pegado!");
                itemPegado = true;
                break;
            }
            else if (collider.CompareTag("P�")) // Verifica se � uma p�
            {
                // Pegar a p�
                p� = collider.gameObject;
                p�.SetActive(false); // Desativa a p�
                Debug.Log("P� pegada!");
                // Execute qualquer l�gica adicional aqui, se necess�rio
                papegado = true;
                break;
            }
            if(collider.CompareTag("item2"))
            {
                item2 = collider.gameObject;
                item2.SetActive(false);
                Debug.Log("item2 pegado");

                itempegado2 = true;
                break;
            }
        }
    }


    void Attack()
    {
        if (!PauseMenu.isPaused) // Verifica se o jogo n�o est� pausado antes de permitir o ataque
        {
            Vector2 attackPosition = transform.position + new Vector3(lastDirection * attackRange, 0f, 0f);
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition, 0.5f);
            objetoDeAtaque.SetActive(true);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Inimigo"))
                {
                    collider.GetComponent<inimigo>().TakeDamage(10);
                }
            }
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("jacare"))
                {
                    collider.GetComponent<jacare>().TakeDamage(10);
                }
            }
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("jaquatirica"))
                {
                    collider.GetComponent<jaquatirica>().TakeDamage(10);
                }
            }
        }
        StartCoroutine(DesativarObjetoDeAtaque());
    }
    IEnumerator DesativarObjetoDeAtaque()
    {
        // Aguardar um curto per�odo de tempo antes de desativar o objeto de ataque
        yield return new WaitForSeconds(0.1f); // Ajuste o tempo conforme necess�rio

        // Desativar o objeto de ataque
        objetoDeAtaque.SetActive(false);
    }
    /*
    void Shoot()
    {
        if (!PauseMenu.isPaused) // Verifica se o jogo n�o est� pausado antes de permitir o tiro
        {
            Vector3 spawnPosition;

            if (lastDirection > 0)
            {
                spawnPosition = bulletSpawnPointRight.position;
            }
            else
            {
                spawnPosition = bulletSpawnPointLeft.position;
            }

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            Vector2 shootDirection = lastDirection > 0 ? Vector2.right : Vector2.left;

            bullet.GetComponent<Rigidbody2D>().AddForce(shootDirection * velocidade_tiro, ForceMode2D.Impulse);
        }
    }
    */
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public IEnumerator TransicaoParaProximaCena()
    {
        // Gradualmente aumenta a opacidade da imagem de transi��o
        float tempoDecorrido = 0f;
        while (tempoDecorrido < tempoDeTransicao)
        {
            // Calcula o progresso da transi��o
            float progresso = tempoDecorrido / tempoDeTransicao;

            // Interpola a opacidade da cor da imagem
            Color cor = imagemTransicao.color;
            cor.a = Mathf.Lerp(0f, 1f, progresso);
            imagemTransicao.color = cor;

            // Atualiza o tempo decorrido
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        // Garante que a opacidade est� no m�ximo
        Color corFinal = imagemTransicao.color;
        corFinal.a = 1f;
        imagemTransicao.color = corFinal;

        // Carrega a pr�xima cena ap�s a transi��o
        SceneManager.LoadScene(proximaCena);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("balainimigo"))
        {
            Destroy(collision.gameObject); // Destrua a bala inimiga
            TakeDamage(danorecibido); // Cause dano ao jogador

        }
        if (collision.CompareTag("fim")) // Verifica se colidiu com o objeto de fim do jogo
        {
            if (itemPegado)
            {
                //fimObjeto.SetActive(true);
                //Time.timeScale = 0f;

                // Inicia a cor da imagem de transi��o com alpha 0
                Color corInicial = imagemTransicao.color;
                corInicial.a = 0f;
                imagemTransicao.color = corInicial;
                StartCoroutine(TransicaoParaProximaCena());
            }
            else
            {
                StartCoroutine(ShowAviso("Voc� precisa pegar o mapa de tesouro primeiro!")); // Exibe o aviso ao jogador
            }
        }
        if (collision.CompareTag("fim2")) // Verifica se colidiu com o objeto de fim 2 do jogo
        {
            if (itemPegado && itempegado2) // Verifica se ambos os itens foram pegos
            {
                StartCoroutine(TransicaoParaProximaCena());
            }
            else
            {
                StartCoroutine(ShowAviso("Voc� precisa pegar ambos os mapas de tesouro  primeiro!")); // Exibe o aviso ao jogador
            }
        }
        if (collision.CompareTag("escada"))
        {
            escadas = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("escada"))
        {
            escadas = false;
            escalando = false;
        }
    }
    public void IniciarTransicao()
    {
        StartCoroutine(TransicaoParaProximaCena());
    }
    private void FixedUpdate()
    {
        {
            if (escalando == true)
            {
                playerrb.gravityScale = 0f;
                playerrb.velocity = new Vector2(playerrb.velocity.x, vertical * velociadeescada);
            }

            else
            {
                playerrb.gravityScale = 10f;
            }
        }
    }
    IEnumerator ShowAviso(string mensagem)
    {
        avisoText.text = mensagem; // Define o texto do aviso
        avisoText.gameObject.SetActive(true); // Ativa o objeto de texto
        yield return new WaitForSeconds(2f); // Aguarda 2 segundos
        avisoText.gameObject.SetActive(false); // Desativa o objeto de texto
    }
    IEnumerator ShowAviso2(string mensagem)
    {
        avisoText.text = mensagem; // Define o texto do aviso
        avisoText.gameObject.SetActive(true); // Ativa o objeto de texto
        yield return new WaitForSeconds(2f); // Aguarda 2 segundos
        avisoText.gameObject.SetActive(false); // Desativa o objeto de texto
    }
}
