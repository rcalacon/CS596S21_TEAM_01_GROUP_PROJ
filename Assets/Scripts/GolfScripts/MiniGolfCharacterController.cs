using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class MiniGolfCharacterController : MonoBehaviour
{
    public float forceMultiplier = 5.0f;
    private float sinFunctionCounter = 0f;
    private float force = 0;
    private Vector3 teePoint;
    private float distToGround;

    Rigidbody playerRigidbody;
    Collider playerCollider;
    public Transform playerCamera;

    public PowerBar powerBar;
    public PlayerScore playerScore;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        teePoint = playerRigidbody.position;
    }

    private void Update()
    {
        //if Ball is not moving
        if (playerRigidbody.IsSleeping())
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                force = 0f;
            }
            else if (Input.GetMouseButton(0))
            {
                force += Input.GetAxis("Mouse Y");
                //force = Mathf.Clamp(Input.mousePosition.y - mouseStart.y, 0f, 1000f);
                force = Mathf.Clamp(force, 0f, 100f);
                powerBar.SetPower(force);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                playerRigidbody.AddForce(Camera.main.transform.forward * force * forceMultiplier);
                playerScore.IncrementStrokes();

            }
            else
            {
                powerBar.SetPower(0f);
                teePoint = playerRigidbody.position;
            }
        }
        if (!IsGrounded())
        {
            playerRigidbody.drag = 0f;
        }
        else
        {
            playerRigidbody.drag = 1f;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8)
        {
            ResetPlayer();
        }
        
    }

    public void ResetPlayer()
    {
        playerRigidbody.position = teePoint;
        playerRigidbody.velocity = Vector3.zero;
        
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.6f);
    }
}
