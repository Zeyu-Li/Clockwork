using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private HingeJoint2D hinge;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
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
        }
    }
}
