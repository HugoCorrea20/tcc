using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fimdejogo : MonoBehaviour
{
    public GameObject gameover;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            gameover.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
