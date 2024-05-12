using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fimdejogo : MonoBehaviour
{
    public string menuprincipal;
    public void Menuprincipal()
    {
        SceneManager.LoadScene(menuprincipal);
    }

    public void SAIR()
    {
        Application.Quit();
        Debug.Log("saiu");
    }
}
