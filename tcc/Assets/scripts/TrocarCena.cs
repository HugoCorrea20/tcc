using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocarCena : MonoBehaviour
{
    public string nomeDaCena;
    public GameObject creditos;
    public GameObject menuprincipal;

    public void TrocarParaCena()
    {
        SceneManager.LoadScene(nomeDaCena);
    }
    public void SAIR()
    {
        Application.Quit();
        Debug.Log("saiu");
    }
    public void abrircreditos()
    {
        creditos.SetActive(true);
        menuprincipal.SetActive(false);

    }
    public void fecharcreditos()
    {
        creditos.SetActive(false );
        menuprincipal.SetActive(true );
    }
}
