using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Player player;
    public GameObject obj_selectOptions;
    public GameObject entire_title;

    private int timer = 0;
    private bool move = false;
    private int angle = 0;
    private int angle_change = 0;
    private int angle_degree = 0;
    private int index = 0;

    // Start is called before the first frame update
    // NOTE: override this if you want to use this on screen
    void Start()
    {
        move = true;
    }

    private void OnEnable()
    {
        index = 0;
        angle = 0;
        timer = 0;
        move = true;
        angle_change = 0;
        angle_degree = 0;
        Quaternion target = Quaternion.Euler(0, 0, angle);
        obj_selectOptions.GetComponent<RectTransform>().rotation = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            if (move)
            {
                float angle = obj_selectOptions.GetComponent<RectTransform>().localRotation.eulerAngles.z;
                LoadOption();
            }
            else
            {
                move = true;
            }
        }

        //if (move == true && timer < 25)
        //{
        //    timer += 1;
        //    obj_selectOptions.SetActive(true);
        //    entire_title.GetComponent<RectTransform>().position = new Vector2(entire_title.GetComponent<RectTransform>().position.x - timer, entire_title.GetComponent<RectTransform>().position.y);
        //}

        if (angle_change == 0)
        {
            float yValue = Input.GetAxisRaw("Vertical");

            if (yValue > 0)
            {
                index--;
                angle_change = 1;
            }
            else if (yValue < 0)
            {
                index++;
                angle_change = 2;
            }

            if (index < 0)
            {
                index += 3;
            }

            Debug.Log("New value: " + index.ToString());
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


    // if you want to implement this, just override this
    public void LoadOption()
    {
        Debug.Log("Our index: " + index.ToString());
        Time.timeScale = 1.0f;
        switch (Mathf.Abs(index) % 3)
        {
            case 0:
                player.ExitPause();
                break;
            case 1:
                player.RestartLevel();
                break;
            case 2:
                SceneManager.LoadScene(0);
                break;
            default:
                Debug.LogError("Pause index should not be outside 0-2. Current index is: " + (Mathf.Abs(index) % 3).ToString());
                break;
        }
    }
}