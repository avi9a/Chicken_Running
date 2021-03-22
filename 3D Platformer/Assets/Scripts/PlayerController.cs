using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 0.5f;
    public float walkSpeed = 4.0f;
    public float runSpeed = 6.0f;
    public float turnSpeed = 3.0f;
    public float jumpForce = 6.0f;
    public float gravity;
    private float checkRadius = 0.05f;
    public bool isGrounded;
    private Vector3 velocity;
    private Vector3 forwardMovement;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    private CharacterController characterController;
    void Start() {
        characterController = GetComponent<CharacterController>();
    }
    void FixedUpdate() {
        Movement();
    }
    void Movement() {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, whatIsGround);
        if(isGrounded && velocity.y > 0) {
            velocity.y = -2.0f;
        }
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        forwardMovement = new Vector3(horizontalInput, 0.0f, verticalInput);
        if (isGrounded) {
            if (forwardMovement != Vector3.zero /*&& (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))*/) {
                Walk();
            }
            else if (forwardMovement != Vector3.zero && Input.GetKeyDown(KeyCode.LeftShift)) {
                Run();
            }
            else if (forwardMovement == Vector3.zero) {
                Idle();
            }
            forwardMovement *= speed;
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
            }
        }
        characterController.Move(forwardMovement * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    void Idle() {

    }
    void Walk() {
        speed = walkSpeed;
    }
    void Run() {
        speed = runSpeed;
    }
    void Jump() {
        velocity.y = Mathf.Sqrt(jumpForce * -gravity);
    }
}
