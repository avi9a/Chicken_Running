using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5.0f;
    public float turnSpeed = 3.0f;
    public float maxSpeed = 8.0f;
    public float jumpForce = 6.0f;
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
        forwardMovement = transform.TransformDirection(forwardMovement);
        characterController.Move(forwardMovement * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * turnSpeed * horizontalInput);
    }
}
