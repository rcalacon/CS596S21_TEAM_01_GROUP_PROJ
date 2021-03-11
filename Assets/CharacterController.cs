using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float jumpForce = .01f;
    bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(-transform.right);
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(transform.right);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(transform.forward);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(-transform.forward);
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    void OnCollisionEnter(Collision other)
    {
            isGrounded = true;
    }

    void OnCollisionExit(Collision other)
    {
            isGrounded = false;
    }
}
