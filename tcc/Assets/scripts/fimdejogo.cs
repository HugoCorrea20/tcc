using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fimdejogo : MonoBehaviour
{
    public string menuprincipal;
    public string nomemenu;
    public void Menuprincipal()
    {
        nomemenu = menuprincipal;
        //SceneManager.LoadScene(menuprincipal);
        StartCoroutine("Abrir");
    }
    private IEnumerator Abrir()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(nomemenu);
    }
    public void SAIR()
    {
        Application.Quit();
        Debug.Log("saiu");
    }
}
