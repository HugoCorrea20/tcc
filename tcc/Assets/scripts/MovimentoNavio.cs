using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Adicione esta linha para trabalhar com UI

public class MovimentoNavio : MonoBehaviour
{
    public float velocidadeMovimento = 5f; // Velocidade de movimento do navio
    public int maxHealth = 100; // Vida máxima do jogador
    public int currentHealth; // Vida atual do jogador
    public GameObject balaPrefab; // Prefab da bala
    public Transform pontoDeSpawn; // Ponto de spawn da bala
    public float velocidadeBala = 10f; // Velocidade da bala
    public AudioSource tirodocanhao;
    private float tempoUltimoTiro;
    private bool primeiroTiroDisparado = false;
    public float tempotiro = 3f;
    public int danorecibido = 10;
    public Transform heatlhbar; //barra verde
    public GameObject heatltbarobject; // objeto pai das barras 
    public GameObject mensagemNavioInimigo; // Referência ao GameObject de mensagem
    public GameObject mensagemIlhaFinal; // Referência ao GameObject de mensagem da ilha final
    private float mensagemNavioTimer = 0f; // Timer para controlar a exibição da mensagem do navio
    private float mensagemIlhaTimer = 0f; // Timer para controlar a exibição da mensagem da ilha

    private Vector3 heatltbarScale; //tamanho da barra
    private float heathpercent;   // percetual de vida para o calculo  do tamanho da barra 
    private SpriteRenderer spriteRenderer;
  //  Color originalcolor;
    void Start()
    {
        tempoUltimoTiro = Time.time;
        currentHealth = maxHealth; // Inicialize a vida atual com a vida máxima
        heatltbarScale = heatlhbar.localScale;
        heathpercent = heatltbarScale.x / currentHealth;
        mensagemNavioInimigo.SetActive(false); // Inicialmente, a mensagem está desativada
        mensagemIlhaFinal.SetActive(false); // Inicialmente, a mensagem está desativada
        //spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void UpdateHealthbar()
    {
        heatltbarScale.x = heathpercent * currentHealth;
        heatlhbar.localScale = heatltbarScale;
    }

    void Update()
    {
        if (!PauseMenu.isPaused) // Verifica se o jogo não está pausado
        {
            MovimentarNavio();

            if (!primeiroTiroDisparado || Time.time - tempoUltimoTiro >= tempotiro)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AtirarBala();
                    tempoUltimoTiro = Time.time;
                    primeiroTiroDisparado = true;
                }
            }

            // Verifica o timer da mensagem do navio inimigo
            if (mensagemNavioInimigo.activeSelf && Time.time - mensagemNavioTimer >= 5f)
            {
                mensagemNavioInimigo.SetActive(false); // Desativa a mensagem após 5 segundos
            }

            // Verifica o timer da mensagem da ilha final
            if (mensagemIlhaFinal.activeSelf && Time.time - mensagemIlhaTimer >= 5f)
            {
                mensagemIlhaFinal.SetActive(false); // Desativa a mensagem após 5 segundos
            }
        }
    }

    void MovimentarNavio()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        Vector3 movimento = new Vector3(movimentoHorizontal, 0f, 0f) * velocidadeMovimento * Time.deltaTime;
        transform.Translate(movimento);
    }

    void AtirarBala()
    {
        GameObject bala = Instantiate(balaPrefab, pontoDeSpawn.position, pontoDeSpawn.rotation);
        bala.GetComponent<Rigidbody2D>().velocity = transform.right * velocidadeBala;
        tirodocanhao.Play();
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthbar();

        if (currentHealth <= 0)
        {
            Die();
        }
        //StopCoroutine(BlinkRed());
        //StartCoroutine(BlinkRed());
    }
    /*
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
    */
    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("balainimigo"))
        {
            Destroy(collision.gameObject); // Destrua a bala inimiga
            TakeDamage(danorecibido); // Cause dano ao jogador
        }
        else if (collision.gameObject.CompareTag("navioinimigo"))
        {
            mensagemNavioInimigo.SetActive(true); // Ativa a mensagem
            mensagemNavioTimer = Time.time; // Reseta o timer
        }
        else if (collision.gameObject.CompareTag("ilhafinal"))
        {
            mensagemIlhaFinal.SetActive(true); // Ativa a mensagem da ilha final
            mensagemIlhaTimer = Time.time; // Reseta o timer
        }
    }
}
