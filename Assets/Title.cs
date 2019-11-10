﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject obj_selectOptions;
    public GameObject entire_title;

    private int timer = 0;
    private bool move = false;
    private int angle = 0;
    private int angle_change = 0;
    private int angle_degree = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Submit") && !move)
        {
            move = true; 
        }

        if (move == true && timer < 25)
        {
            timer += 1;
            obj_selectOptions.SetActive(true);
            entire_title.GetComponent<RectTransform>().position = new Vector2(entire_title.GetComponent<RectTransform>().position.x - timer, entire_title.GetComponent<RectTransform>().position.y);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) 
        {
            angle_change = 1; 
        } else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            angle_change = 2;
        }

        if (angle_change == 1) {
            if (angle_degree < 30)
            {
                angle_degree += 2;
                Quaternion target = Quaternion.Euler(0, 0, angle + angle_degree);
                obj_selectOptions.GetComponent<RectTransform>().rotation = target;
            }
            else {
                angle = angle + angle_degree;
                angle_degree = 0;
                angle_change = 0;
            }
        } else if (angle_change == 2)
        {
            if (angle_degree > -30)
            {
                angle_degree -= 2;
                Quaternion target = Quaternion.Euler(0, 0, angle + angle_degree);
                obj_selectOptions.GetComponent<RectTransform>().rotation = target;
            }
            else
            {
                angle = angle + angle_degree;
                angle_degree = 0;
                angle_change = 0;
            }
        }

    }
}