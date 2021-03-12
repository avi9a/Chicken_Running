using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 5.0f;
    public float maxSpeed = 8.0f;
    public float jumpForce = 6.0f;
    public Transform groundCheck;
    void Awake() {

    }
    void Start() {
    }
    void FixedUpdate() {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
    }
}
