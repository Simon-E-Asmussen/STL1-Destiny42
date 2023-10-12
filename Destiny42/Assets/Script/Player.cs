using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;


[RequireComponent(typeof(CharacterController))]

public class Player : NetworkBehaviour
{
    public float walkingSpeed = 10.5f;
    public float runningSpeed = 21.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    //public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    [SerializeField] 
    private float cameraYOffset;
    private Camera playerCamera2;

    private GameObject GameManager;
    private Gamemanager _gamemanager;
    
    public override void OnStartClient()
    {
        GameManager = GameObject.Find("GameManager");
        _gamemanager = GameManager.GetComponent<Gamemanager>();
        
        base.OnStartClient();
        if (base.IsOwner)
        {
            ChangeYOffset(_gamemanager.IsBoss);
            playerCamera2 = Camera.main;
            playerCamera2.transform.position = new Vector3(transform.position.x, transform.position.y + cameraYOffset,
                transform.position.z);
            playerCamera2.transform.SetParent(transform);
        }
        else
        {
            gameObject.GetComponent<Player>().enabled = false;
        }
    }

    private void ChangeYOffset(bool isBoss)
    {
        if (isBoss == false)
        {
            cameraYOffset = 1.5f;
        }
        else
        {
            cameraYOffset = 3.5f;
        }
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove && playerCamera2 != null)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera2.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if(Input.GetButton("Fire1") && canMove)
        {
           // This.Gun.shoot();
        }
    }
}