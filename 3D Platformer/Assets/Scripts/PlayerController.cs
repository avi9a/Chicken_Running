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
    private static float verticalInput;
    private static float horizontalInput;
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
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        forwardMovement = new Vector3(horizontalInput, 0.0f, verticalInput);
        if (isGrounded) {
            if (forwardMovement != Vector3.zero && (Input.GetKey(KeyCode.UpArrow))) {
                transform.rotation = Quaternion.LookRotation(forwardMovement);
                Walk();
            }
            else if (forwardMovement == Vector3.zero) {
                Idle();
            }
            forwardMovement *= speed;
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
            }
        }
        characterController.Move((forwardMovement * speed * Time.deltaTime)/8);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    void PlayerRotation() {
        if(horizontalInput > 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 2 * Time.deltaTime);
        } else if(horizontalInput < 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 2 * Time.deltaTime);
        }
        if(verticalInput > 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 2 * Time.deltaTime);
        } else if(verticalInput < 0) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 2 * Time.deltaTime);
        }
    }
    void Idle() {

    }
    void Walk() {
        speed = walkSpeed;
    }
    void Jump() {
        velocity.y = Mathf.Sqrt(jumpForce * -gravity);
    }
}
