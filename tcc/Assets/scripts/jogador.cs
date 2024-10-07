using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class jogador : MonoBehaviour
{
    public int maxHealth = 100; // Vida m�xima do jogador
    public int currentHealth; // Vida atual do jogador
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;

    private bool isGrounded;
    private float lastDirection = 1f;
    public float attackRange = 1.5f; // Ajuste a dist�ncia de ataque conforme necess�rio
    public float pickupRange = 1.5f; // Ajuste a dist�ncia de pegar o item conforme necess�rio
    public bool itemPegado = false;
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
    private  Animator animator; // Refer�ncia ao componente Animator
    public bool wasMoving = false; // Indica se o jogador estava se movendo no frame anterior
    public bool papegado = false;
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
    public GameObject localcavar;
    public GameObject chave;
    public GameObject alcapao;
    public bool chavePegada = false;
    public float alcanceMaximo = 1f;
    public bool inpute = false;
    public Image itemIcon; // Refer�ncia ao componente Image na UI para exibir o �cone do item
    public Image Item2icon;
    public Image paicon;
    public Image chavicone;
    public GameObject chaofalso;
    public AudioSource movementSound;
    public AudioSource stairSound;
    public AudioSource al�ap�osource;
    public AudioSource cavarSound;
    public AudioSource espadasom;
    public AudioSource jumpSound;
    public AudioSource damagesound;
    public float gravidade = 7f;
    public bool ativarMortePorChao = false;
    public GameObject texto;
    public GameObject textopa;
    public GameObject textochave;
    public GameObject textoitem2;
    public bool pulando;


    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        heatltbarScale = heatlhbar.localScale;
        heathpercent = heatltbarScale.x / currentHealth;
        texto.SetActive(false);
        
        animator = GetComponent<Animator>(); // Obt�m o componente Animator

        itemIcon.gameObject.SetActive(false);
         if(Item2icon != null ) 
        {
            Item2icon.gameObject.SetActive(false);
        }
        if(chavicone  != null ) 
        {
            chavicone.gameObject.SetActive(false);
        }
        if (movementSound != null)
        {
            movementSound.loop = true; // O som deve repetir enquanto o jogador estiver se movendo
        }
        if (stairSound != null)
        {
            stairSound.loop = true; // O som deve repetir enquanto o jogador estiver subindo/descendo escadas
        }
        if (textopa != null)
        {
            textopa.SetActive(false);
        }
        // Certifique-se de que o jumpSound est� atribu�do
        if (jumpSound == null)
        {
            Debug.LogWarning("Jump sound is not assigned.");
        }

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
            if (Input.GetMouseButtonDown(0)) // Bot�o esquerdo do mouse para ataque de perto
            {
                Attack();
                espadasom.Play();
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
                if(alcapao != null)
                {
                    if (Vector2.Distance(transform.position, alcapao.transform.position) < alcanceMaximo)
                    {
                        if (chavePegada)
                        {
                            alcapao.SetActive(false);
                            Debug.Log("Al�ap�o aberto!");
                        }
                        else
                        {
                            StartCoroutine(ShowAviso("Voc� precisa da chave para abrir o al�ap�o!"));
                        }
                    }
                }
               
                inpute = true;
            }
            if (escalando==true && Input.GetAxis("Vertical") !=0)
            {
                stairSound.volume = 1;
            }
            else if(escalando==true )
            {
                stairSound.volume = 0;
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                inpute = false;
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
        if (cavarSound != null)
        {
            cavarSound.Play();
        }

        localcavar.SetActive(false);
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
            // Tocar o som de pulo
            if (jumpSound != null)
            {
                jumpSound.Play();
            }
            pulando = true;
            animator.SetBool("pular", true);
        }
    }

    void UpdateAnimations()
    {
        bool isMoving = Mathf.Abs(rb.velocity.x) > 0.01f;

        if (isMoving && isGrounded)
        {
            animator.SetBool("movendo", true);
            animator.SetBool("parado", false);
        }
        else if (!isMoving && isGrounded)
        {
            animator.SetBool("movendo", false);
            animator.SetBool("parado", true);
        }

        // Gerenciar transi��es entre anima��es de pulo e ch�o
        if (!isGrounded)
        {
            animator.SetBool("pular", true);
        }
        else
        {
            animator.SetBool("pular", false);
        }

        wasMoving = isMoving;
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * lastDirection;
        transform.localScale = scale;
    }

    void PickupItem()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Item"))
            {
                currentItem = collider.gameObject;
                currentItem.SetActive(false);
                Debug.Log("Item gen�rico pegado!");
                itemPegado = true;

                // Atualizar o �cone do item na UI
                itemIcon.sprite = currentItem.GetComponent<SpriteRenderer>().sprite;
                itemIcon.gameObject.SetActive(true); // Torna o �cone vis�vel
                break;
            }
            else if (collider.CompareTag("P�"))
            {
                p� = collider.gameObject;
                p�.SetActive(false);
                Debug.Log("P� pegada!");
                papegado = true;

                paicon.sprite = p�.GetComponent<SpriteRenderer>().sprite;
                paicon.gameObject.SetActive(true);
                break;
            }
            else if (collider.CompareTag("item2"))
            {
                item2 = collider.gameObject;
                item2.SetActive(false);
                Debug.Log("item2 pegado");
                itempegado2 = true;

                Item2icon.sprite = item2.GetComponent<SpriteRenderer>().sprite;
                Item2icon.gameObject.SetActive(true);
                break;
            }
            else if (collider.CompareTag("Chave"))
            {
                chave = collider.gameObject;
                chave.SetActive(false);
                Debug.Log("Chave pegada!");
                chavePegada = true;

                chavicone.sprite = chave.GetComponent<SpriteRenderer>().sprite;
                chavicone.gameObject.SetActive(true);
                break;
            }
        }
    }

    void Attack()
    {
        if (!PauseMenu.isPaused)
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
        yield return new WaitForSeconds(0.1f);
        objetoDeAtaque.SetActive(false);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthbar();
        damagesound.Play();

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
        float tempoDecorrido = 0f;
        while (tempoDecorrido < tempoDeTransicao)
        {
            float progresso = tempoDecorrido / tempoDeTransicao;
            Color cor = imagemTransicao.color;
            cor.a = Mathf.Lerp(0f, 1f, progresso);
            imagemTransicao.color = cor;
            tempoDecorrido += Time.deltaTime;
            yield return null;
            Debug.Log("ensostou");
        }

        Color corFinal = imagemTransicao.color;
        corFinal.a = 1f;
        imagemTransicao.color = corFinal;
        SceneManager.LoadScene(proximaCena);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Chao"))
        {
            isGrounded = true;
            if (ativarMortePorChao)
            {
                Die();
            }
        }
        if (other.gameObject.CompareTag("chaofalso"))
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
        if(other.gameObject.CompareTag("chaofalso"))
        {
            isGrounded= false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("balainimigo"))
        {
            Destroy(collision.gameObject);
            if(Vector2.Distance(collision.gameObject.transform.position,transform.position ) <1.0f)
            TakeDamage(danorecibido);
        }
        if (collision.CompareTag("fim"))
        {
            if (itemPegado)
            {
                Color corInicial = imagemTransicao.color;
                corInicial.a = 0f;
                imagemTransicao.color = corInicial;
                StartCoroutine(TransicaoParaProximaCena());
            }
            else
            {
                StartCoroutine(ShowAviso("Voc� precisa pegar o mapa de tesouro primeiro!"));
            }
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            texto.SetActive(true);
        }
        if (collision.gameObject.CompareTag("P�"))
        {
            textopa.SetActive(true);
        }
        if (collision.gameObject.CompareTag("item2"))
        {
            textoitem2.gameObject.SetActive(true);
        }
        if(collision.gameObject.CompareTag("Chave"))
        {
            textochave.SetActive(true);
        }
        else  if (collision.CompareTag("fim2"))
        {
            if (itemPegado && itempegado2)
            {
                Color corInicial = imagemTransicao.color;
                corInicial.a = 0f;
                imagemTransicao.color = corInicial;
                Debug.Log("ensostou");
                StartCoroutine(TransicaoParaProximaCena());
               
            }
            else
            {
                StartCoroutine(ShowAviso("Voc� precisa pegar ambos os mapas de tesouro primeiro!"));
            }
        }
        if (collision.CompareTag("escada"))
        {
            escadas = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Alcapao"))
        {
            if (chavePegada && inpute)
            {
                alcapao.SetActive(false);
                Debug.Log("Al�ap�o aberto!");
                al�ap�osource.Play();
            }
            else if (!chavePegada)
            {
                StartCoroutine(ShowAviso("Voc� precisa da chave para abrir o al�ap�o!"));
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("escada"))
        {
            escadas = false;
            escalando = false;
        }
        if (col.gameObject.CompareTag("Item"))
        {
            texto.SetActive(false);
        }
    }

    public void IniciarTransicao()
    {
        StartCoroutine(TransicaoParaProximaCena());
    }

    private void FixedUpdate()
    {
        if (escalando == true)
        {
            playerrb.gravityScale = 0f;
            playerrb.velocity = new Vector2(playerrb.velocity.x, vertical * velociadeescada);

            // Toca o som da escada se n�o estiver tocando
            if (stairSound != null && !stairSound.isPlaying)
            {
                stairSound.Play();
            }
        }
        else
        {
            playerrb.gravityScale = gravidade;

            // Para o som da escada se estiver tocando
            if (stairSound != null && stairSound.isPlaying)
            {
                stairSound.Stop();
            }
        }
    }


    IEnumerator ShowAviso(string mensagem)
    {
        avisoText.text = mensagem;
        avisoText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        avisoText.gameObject.SetActive(false);
    }

}
