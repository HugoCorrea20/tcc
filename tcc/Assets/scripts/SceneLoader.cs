using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;
using System;
using Random = UnityEngine.Random;

public class SceneLoader : MonoBehaviour
{
    
    [Header("Porcetagem")]
    public Slider Slloading;
    public Text textporcetagem;
    [Header("Imagem")]
    public Image imgparaMudar;
    public int cenas;
    public Sprite[] imagens;

    public void Start()
    {
        StartCoroutine(LoadScene_Estiloso());
    }

    public void MudarImagem()
    {
        //pegar img aleatoria
        int rand = Random.Range(0, imagens.Length);

        //alterar a imagem
        imgparaMudar.sprite = imagens[rand];
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            MudarImagem();
        }
    }
    private IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(0);

        while(operation.isDone) 
        {

            float progresso = Mathf.Clamp01(operation.progress /0.9f)*100;
            float progresso2 = Mathf.Clamp01(operation.progress / 0.9f) * 100;

            Slloading.value = operation.progress;
            if (progresso >= 100) progresso = 100;
            textporcetagem.text = progresso + "%";

            yield return null;
        }
    }
    
    private IEnumerator LoadScene_Estiloso()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(cenas);
        operation.allowSceneActivation = false;
        float progresso = 0.0f;
        float progresso2 = 0.0f;

        while (progresso <100 && progresso2 <100) 
        {
           
            progresso += Random.Range(5.0f, 15.0f);
            progresso2 += 0.1f;

            

            Slloading.value =progresso2;
            if (progresso >= 100) progresso = 100;
            textporcetagem.text = ((int)progresso) + "%";
            
            yield return new WaitForSeconds(0.8f);

        }
        Slloading.value = 100;
        textporcetagem.text = "100%";
        operation.allowSceneActivation = true;
        yield return null;
    }
    
}
