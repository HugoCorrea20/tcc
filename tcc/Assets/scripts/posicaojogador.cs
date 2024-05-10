using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject jogador;
    public Transform posicaoinicial;

    private void Start()
    {
        Invoke(nameof(posicao), 0.2f);
    }
    public void posicao()
    {
        jogador.transform.position = posicaoinicial.position;
    }
}
