using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require a Rigidbody component
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public Transform cameraTransform; // Assign your camera here in the Inspector
    private Rigidbody rb;
    private bool isGrounded = true;
    public float rotationSpeed = 3f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movement relative to camera's facing direction
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cameraTransform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = camRight * horizontal + camForward * vertical;
        move = move.normalized;

        // Set animator parameter
        if (animator != null)
        {
            animator.SetBool("IsRunning", move.magnitude > 0.1f);
        }

        Vector3 velocity = rb.velocity;
        velocity.x = move.x * speed;
        velocity.z = move.z * speed;

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }

        rb.velocity = velocity;
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0f, mouseX * rotationSpeed, 0f);

    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}