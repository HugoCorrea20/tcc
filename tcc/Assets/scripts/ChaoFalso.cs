using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaoFalso : MonoBehaviour
{
    private jogador jogadorScript;
    private bool jogadorNoPiso = false;
    public  Coroutine destruirCoroutine;
     public float tempo = 5f;

    void Start()
    {
        jogadorScript = GameObject.FindGameObjectWithTag("Player").GetComponent<jogador>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jogadorNoPiso = true;
            if (destruirCoroutine == null)
            {
                destruirCoroutine = StartCoroutine(DestruirPisoAposTempo(tempo)); // Inicia a coroutine com 5 segundos
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            jogadorNoPiso = false;
            if (destruirCoroutine != null)
            {
                StopCoroutine(destruirCoroutine); // Para a coroutine se o jogador sair
                destruirCoroutine = null; // Reseta a referência da coroutine
            }
        }
    }

    private IEnumerator DestruirPisoAposTempo(float tempo)
    {
        yield return new WaitForSeconds(tempo);

        if (jogadorNoPiso)
        {
            jogadorScript.ativarMortePorChao = true;
            Destroy(gameObject);
        }

        destruirCoroutine = null; // Reseta a referência da coroutine após completar
    }
}
