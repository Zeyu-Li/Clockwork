using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour
{
    private int num = 12;
    private int rad = 540;
    private int ang = 0;
    private int tar_ang = 0;
    private int option = 0;
    private int timer = 50; 

// Start is called before the first frame update
void Start()
    {
        /*for (int i = 0; i < num; i++)
        {
            xx[i] = x + lengthdir_x(rad, 360 / num * i + ang);
            yy[i] = y + lengthdir_y(rad, 360 / num * i + ang);
            alp[i] = cos(degtorad(360 / num * i + ang));
        }

        for (i = 0; i < num; i += 1)
        {
            draw_sprite_ext(spr_options, i, xx[i], yy[i], 1, 1, 360 / num * i + ang, c_white, alp[i]);
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
