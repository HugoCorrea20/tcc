using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavioInimigo : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public GameObject balaPrefab;
    public Transform pontoDeTiro;
    public float alcanceDeVisao = 10f;
    public LayerMask jogadorLayer;

    public float velocidadeMovimento = 2f;
    public float limiteDireita = 5f;
    public float limiteEsquerda = -5f;

    private GameObject jogador;
    private bool movendoDireita = true;
    private bool atirando = false;
    public float tempodetiro = 20f;
    public float shootCooldown = 5;
    public int danorecibido = 10;
    public Transform heatlhbar; //barra verde
    public GameObject heatltbarobject; // objeto pai das barras 

    private Vector3 heatltbarScale; //tamanho da barra
    private float heathpercent;   // percetual de vida para o calculo  do tamanho da barra 

    void Start()
    {
        currentHealth = maxHealth;
        jogador = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("AtirarSeJogadorVisivel", 0f, shootCooldown);
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
        if (!atirando) // Continua o movimento apenas se não estiver atirando
        {
            MovimentoLateral();
        }
    }

    void MovimentoLateral()
    {
        if (movendoDireita)
        {
            transform.Translate(Vector2.right * velocidadeMovimento * Time.deltaTime);
            if (transform.localScale.x < 0) // Verifica se a escala é negativa (flip para esquerda)
            {
                Flip(); // Se sim, faz o flip para direita
            }
        }
        else
        {
            transform.Translate(Vector2.left * velocidadeMovimento * Time.deltaTime);
            if (transform.localScale.x > 0) // Verifica se a escala é positiva (flip para direita)
            {
                Flip(); // Se sim, faz o flip para esquerda
            }
        }

        if (transform.position.x >= limiteDireita)
        {
            movendoDireita = false;
        }
        else if (transform.position.x <= limiteEsquerda)
        {
            movendoDireita = true;
        }

        if (JogadorVisivel())
        {
            atirando = true;
            Invoke("FinalizarTiro", tempodetiro); // Aqui ajuste o tempo de pausa antes de retomar o movimento
        }
    }

    void FinalizarTiro()
    {
        atirando = false;
    }

    void AtirarSeJogadorVisivel()
    {
        if (JogadorVisivel())
        {
            Atirar();
        }
    }

    bool JogadorVisivel()
    {
        if (jogador == null)
            return false;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (jogador.transform.position - transform.position).normalized, alcanceDeVisao, jogadorLayer);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Vector3 playerDirection = jogador.transform.position - transform.position;
            if (playerDirection.x > 0 && transform.localScale.x < 0) // Jogador está à direita, mas o navio está virado para a esquerda
            {
                Flip();
            }
            else if (playerDirection.x < 0 && transform.localScale.x > 0) // Jogador está à esquerda, mas o navio está virado para a direita
            {
                Flip();
            }
            return true;
        }
        return false;
    }

    void Atirar()
    {
        GameObject novaBala = Instantiate(balaPrefab, pontoDeTiro.position, Quaternion.Euler(0, 0, 0));
        novaBala.GetComponent<Rigidbody2D>().velocity = pontoDeTiro.right * 10f;
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

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
