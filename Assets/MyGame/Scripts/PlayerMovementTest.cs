using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTest : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float gravityMultiplier;

    [SerializeField]
    private float turnSpeed;
    private float horizontalInput;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpButtonGracePeriod;
    private float verticalInput;
    private CharacterController characterController;
    private float yForce;
    private float originalStepOffset;
    private float? lastGroundTime;
    private float? jumpButtonPressTime;
    private bool isJumping;
    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim =GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        print($"Vector Magnitude before normalize: {movementDirection.magnitude}");

        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        playerAnim.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
        float speed = inputMagnitude * moveSpeed;
        movementDirection.Normalize();

        print($"Vector Magnitude after normalize: {movementDirection.magnitude}");
        float gravity = Physics.gravity.y * gravityMultiplier;

        if(isJumping && yForce > 0 && Input.GetButton("Jump") == false)
        {
            gravity *= 2;
        }

        yForce += gravity * Time.deltaTime;


        if (characterController.isGrounded)
        {
            lastGroundTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressTime = Time.time;
        }
        if (Time.time - lastGroundTime <= jumpButtonGracePeriod)
        {
            yForce = - 0.5f;
            characterController.stepOffset = originalStepOffset;
            playerAnim.SetBool("IsGrounded", true);
            isGrounded = true;

            playerAnim.SetBool("IsJumping", false);
            isJumping = false;

            playerAnim.SetBool("IsFalling", false);

            if (Time.time - jumpButtonPressTime <= jumpButtonGracePeriod)
            {
                yForce = Mathf.Sqrt(jumpSpeed * -3.0f * gravity);

                playerAnim.SetBool("IsJumping", true);
                isJumping = true;

                jumpButtonPressTime = null;
                lastGroundTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;

            playerAnim.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && yForce <0) || yForce < -2)
            {
                playerAnim.SetBool("IsFalling", true);
            }
        }
        

        //if (movementDirection != Vector3.zero)
        //{
        //    transform.forward = movementDirection;
        //}
        
        if (movementDirection != Vector3.zero)
        {
            playerAnim.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            playerAnim.SetBool("isMoving", false);
        }

        if (!isGrounded)
        {
            Vector3 velocity = inputMagnitude * jumpSpeed * movementDirection;
            velocity.y = yForce;

            characterController.Move(velocity * Time.deltaTime);
        }

    }

    private void OnAnimatorMove()
    {
        if (isGrounded)
        {
            Vector3 velocity = playerAnim.deltaPosition;
            velocity.y = yForce * Time.deltaTime;

            characterController.Move(velocity);
        }
    }
}
