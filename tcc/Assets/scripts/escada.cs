using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escada : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool escadas;
    private bool escalando;

    public Rigidbody2D playerrb;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vertical = UnityEngine.Input.GetAxis("Vertical");
        if (escadas && Mathf.Abs(vertical) > 0f)
        {
            escalando = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("escada"))
        {
            escadas = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("escada"))
        {
            escadas = false;
            escalando = false;
        }
    }
    private void FixedUpdate()
    {
        {
            if (escalando == true)
            {
                playerrb.gravityScale = 0f;
                playerrb.velocity = new Vector2(playerrb.velocity.x, vertical * speed);
            }

            else
            {
                playerrb.gravityScale = 2f;
            }
        }
    }
}
