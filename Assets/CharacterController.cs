using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speedLimit = 1f;
    public float jumpForce = .01f;
    bool isGrounded;
    float ballSpeed = 10.0f;
    float slowDownSpeed = .9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        // General Movement
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Camera.main.transform.forward * ballSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-Camera.main.transform.right * ballSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Camera.main.transform.right * ballSpeed);
        }

        // Slow Down
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(rb.velocity.x * slowDownSpeed, rb.velocity.y, rb.velocity.z * slowDownSpeed);
        }
        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

        throttleVelocity(rb);
        print(rb.velocity);
    }

    void throttleVelocity(Rigidbody player)
    {
        if (player.velocity.x > speedLimit)
        {
            player.velocity = new Vector3(speedLimit, player.velocity.y, player.velocity.z);
        }
        else if (player.velocity.x < -speedLimit)
        {
            player.velocity = new Vector3(-speedLimit, player.velocity.y, player.velocity.z);
        }
        if (player.velocity.z > speedLimit)
        {
            player.velocity = new Vector3(player.velocity.x, player.velocity.y, speedLimit);
        }
        else if (player.velocity.z < -speedLimit)
        {
            player.velocity = new Vector3(player.velocity.x, player.velocity.y, -speedLimit);
        }
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
