using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 0.5f;
    public float walkSpeed = 4.0f;
    public float turnSpeed = 5.0f;
    public float jumpForce = 6.0f;
    public float gravity;
    private float checkRadius = 0.05f;
    public bool isGrounded;
    private static float verticalInput;
    private static float horizontalInput;
    [SerializeField] private Transform player;
    private Vector3 velocity;
    private Vector3 forwardMovement;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    private CharacterController characterController;
    private Animator characterAnimator;
    private AudioSource characterAudio;
    void Start() {
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();
        characterAudio = GetComponent<AudioSource>();
    }
    void FixedUpdate() {
        Movement();
    }
    void OnControllerColliderHit(ControllerColliderHit hit) {

        //if (other.gameObject.CompareTag("Chick")) {
        //    Debug.Log("Chick");
        //    Destroy(other.gameObject);
        //}
    }
        void Movement() {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, whatIsGround);
        if(isGrounded && velocity.y > 0) {
            velocity.y = -2.0f;
        }
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        forwardMovement = new Vector3(horizontalInput, 0.0f, verticalInput);
        forwardMovement.Normalize();
        if (horizontalInput > 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), turnSpeed * Time.deltaTime);
            }
            else if (horizontalInput < 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), turnSpeed * Time.deltaTime);
            }
            if (verticalInput > 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), turnSpeed * Time.deltaTime);
            }
            else if (verticalInput < 0) {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), turnSpeed * Time.deltaTime);
            }
            if (isGrounded) {
                if (forwardMovement != Vector3.zero) {
                    characterAnimator.SetFloat("Speed", speed);
                Walk();
                }
                else if (forwardMovement == Vector3.zero) {
                    characterAnimator.SetFloat("Speed", 0);
                    Idle();
                IEnumerator coroutine = WaitAndTurn(6);
                StartCoroutine(coroutine);
                }
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Jump();
                }
            }
        characterController.Move(forwardMovement * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
    IEnumerator WaitAndTurn(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        characterAnimator.SetBool("Eat", true);
    }
    void Idle() {
        characterAudio.Stop();
    }
    void Walk() {
        speed = walkSpeed;
        if (!characterAudio.isPlaying) {
            characterAudio.Play();
        }
        characterAnimator.SetBool("Eat", false);
    }
    void Jump() {
        velocity.y = Mathf.Sqrt(jumpForce * -gravity);
    }
}
