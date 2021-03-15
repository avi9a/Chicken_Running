using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    public float walkSpeed = 4.0f;
    public float runSpeed = 6.0f;
    public float turnSpeed = 3.0f;
    public float maxSpeed = 8.0f;
    public float jumpForce = 6.0f;
    private float gravity = 10.0f;
    private Vector3 forwardMovement;
    public Transform groundCheck;
    private CharacterController characterController;
    void Awake() {

    }
    void Start() {
        characterController = GetComponent<CharacterController>();
    }
    void FixedUpdate() {
        Movement();
    }
    void Movement() {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        forwardMovement = new Vector3(0.0f, 0.0f, verticalInput);
        if(forwardMovement != Vector3.zero && Input.GetButton("Up")) {
            Walk();
        } else if(forwardMovement != Vector3.zero && Input.GetButton("Up")) {
            Run();
        } else if(forwardMovement == Vector3.zero) {
            Idle();
        }
        forwardMovement *= speed;
        characterController.Move(forwardMovement * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * turnSpeed * horizontalInput);
    }
    public void Idle() {

    }
    public void Walk() {
        speed = walkSpeed;
    }
    public void Run() {
        speed = runSpeed;
    }

}
