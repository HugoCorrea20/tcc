using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject tutorial;
    public string menuprincipal;
    public string nomemenu;
    public static bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    void Pause()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pausa o jogo
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Continua o jogo
    }
    public void continuar()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false );
        isPaused = false;
    }
    public void Tutorial()
    {
        tutorial.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void SairTutorial()
    {
        tutorial.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void Menuprincipal()
    {
        nomemenu = menuprincipal;
        //SceneManager.LoadScene(menuprincipal);
        StartCoroutine("Abrir");
        isPaused = false;
        Time.timeScale = 1f;
    }
    private IEnumerator Abrir()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(nomemenu);
    }
}

