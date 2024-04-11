using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class fim: MonoBehaviour
{
    public GameObject gameover;
    public string reinicar;
    public string menuprincipal;
    
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameover.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    */
    public void Recomeçar()
    {
        SceneManager.LoadScene(reinicar);
        Time.timeScale = 1f;
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(menuprincipal);
        Time.timeScale = 1f;
    }
    public void SAIR()
    {
        Application.Quit();
        Debug.Log("saiu");
    }
}
