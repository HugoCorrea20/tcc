using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitecamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var distacez = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x;
        var TopBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
        var BottomBorder = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y;

        transform.position = new Vector2(
        Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
        Mathf.Clamp(transform.position.y, TopBorder, BottomBorder));
        ;
    }
}
