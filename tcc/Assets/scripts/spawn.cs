using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class posicaojogador : MonoBehaviour
{
    public GameObject jogador;
    public Transform posicaoinicial;

    private void Start()
    {
        jogador.transform.position= posicaoinicial.position;
    }
}
