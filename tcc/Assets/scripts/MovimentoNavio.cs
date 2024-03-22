using UnityEngine;

public class MovimentoNavio : MonoBehaviour
{
    public float velocidadeMovimento = 5f; // Velocidade de movimento do navio
    public GameObject balaPrefab; // Prefab da bala
    public Transform pontoDeSpawn; // Ponto de spawn da bala
    public float velocidadeBala = 10f; // Velocidade da bala

    private float tempoUltimoTiro;
    private  bool primeiroTiroDisparado = false;

    void Start()
    {
        tempoUltimoTiro = Time.time;
    }

    void Update()
    {
        MovimentarNavio();

        if (!primeiroTiroDisparado || Time.time - tempoUltimoTiro >= 3f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AtirarBala();
                tempoUltimoTiro = Time.time;
                primeiroTiroDisparado = true;
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
    }
}
