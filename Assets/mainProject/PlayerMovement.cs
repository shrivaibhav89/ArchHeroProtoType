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
            // clamp the postion to x and z so it will not go outside the arena
            rb.position = new Vector3(Mathf.Clamp(rb.position.x, -24f, 24f), rb.position.y, Mathf.Clamp(rb.position.z, -34f, 34f));
            rb.MoveRotation(Quaternion.LookRotation(movement));
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }

    }

    public void FixedUpdate()
    {
        MovePlayer();
    }
}
