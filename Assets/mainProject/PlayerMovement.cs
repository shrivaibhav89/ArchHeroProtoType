using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement != Vector3.zero)
        {
            rb.MoveRotation(Quaternion.LookRotation(movement));
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }

    }

    public void FixedUpdate()
    {
        MovePlayer();
    }
}
