using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tinter : MonoBehaviour
{

    public Player player;
    private Color col;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
         sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        col = sr.color;
        if (player.isSoulTime)
        {
            if (col.a < 0.4)
            {
                col.a += 0.02F;
            }
            
        }
        else
        {
            if (col.a > 0.0)
            {
                col.a -= 0.02F;
            }
        }

        sr.color = col;
    }
}
