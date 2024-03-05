using UnityEngine;

public class MovimentoNavio : MonoBehaviour
{
    public float velocidadeMovimento = 5f; // Velocidade de movimento do navio
    public GameObject balaPrefab; // Prefab da bala
    public Transform pontoDeSpawn; // Ponto de spawn da bala
    public float velocidadeBala = 10f; // Velocidade da bala
    


    void Update()
    {
        MovimentarNavio();
        AtirarBala();
        

    }

    void MovimentarNavio()
    {
        float movimentoHorizontal = Input.GetAxis("Horizontal");
        Vector3 movimento = new Vector3(movimentoHorizontal, 0f, 0f) * velocidadeMovimento * Time.deltaTime;
        transform.Translate(movimento);
    }

    void AtirarBala()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            GameObject bala = Instantiate(balaPrefab, pontoDeSpawn.position, pontoDeSpawn.rotation);

         
            bala.GetComponent<Rigidbody2D>().velocity = transform.right * velocidadeBala;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LimiteDoMapa"))
        {
            Destroy(gameObject);
        }
    }

}
