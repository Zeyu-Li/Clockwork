using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private HingeJoint2D hinge;
    private JointMotor2D motor;
    public Player player;
    public int type;

    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
        counter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isSoulTime)
        {
            rb.freezeRotation = true;
            hinge.useMotor = false;
        } 
        else
        {
            rb.freezeRotation = false;
            hinge.useMotor = true;
            if (type == 1)
            {
                counter += 0.02f;
                transform.Rotate(0, 0, Mathf.Cos(counter));
            }
        }

        
    }
}
