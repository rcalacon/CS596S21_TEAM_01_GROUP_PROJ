using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speedLimit = 10f;
    public float jumpForce = .001f;
    bool isGrounded;
    float ballSpeed = 10.0f;
    float slowDownSpeed = .9f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // Disable Movement In Mid Air
        if (!isGrounded) return;

        Rigidbody player = GetComponent<Rigidbody>();

        // General Movement
        if (Input.GetKey(KeyCode.W))
        {
            player.AddForce(Camera.main.transform.forward * ballSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.AddForce(-Camera.main.transform.right * ballSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.AddForce(Camera.main.transform.right * ballSpeed);
        }

        // Slow Down
        if (Input.GetKey(KeyCode.S))
        {
            player.velocity = new Vector3(player.velocity.x * slowDownSpeed, player.velocity.y, player.velocity.z * slowDownSpeed);
        }
        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            player.velocity = new Vector3(player.velocity.x, jumpForce, player.velocity.z);

        throttleVelocity(player);
        print(player.velocity);
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
