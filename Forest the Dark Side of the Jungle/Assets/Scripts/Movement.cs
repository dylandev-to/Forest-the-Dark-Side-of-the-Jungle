using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float movementSpeed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    [SerializeField] private GameObject player;

    void Start()
    {
        controller = GetComponent<CharacterController>();


        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        MovePlayer();


        LookAround();


        ApplyGravity();

        CheckHightShowDeath();


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer()
    {

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : movementSpeed;

        if (PlayerShoot.IsStunned) currentSpeed /= 10;

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void CheckHightShowDeath()
    {
        float PlayerHight = player.transform.position.y;
        if(PlayerHight < -6.5)
        {
            UIManager.Instance.ShowDeadScreen(true);
        }
    }

    void LookAround()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;


        transform.Rotate(Vector3.up * mouseX);


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void Jump()
    {

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    void ApplyGravity()
    {

        velocity.y += gravity * Time.deltaTime;


        controller.Move(velocity * Time.deltaTime);
    }
}
