using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using UnityEngine.UI;


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

    private Slider healthBar;
    [SerializeField] private bool isDead = false;
    private GameObject pauseMenu;

    private CharacterSelection _characterSelection;

    public GameObject healthbar;
    public GameObject GameManager;

    public override void OnStartClient()
    {
        _characterSelection = GameObject.Find("Client(Clone)").GetComponent<CharacterSelection>();
        base.OnStartClient();
        if (base.IsOwner)
        {
            Debug.Log("Emotional Damage!");
            ChangeYOffset(_characterSelection.isBoss);
            playerCamera2 = Camera.main;
            if (!isDead && !GameObject.Find("GameManager").GetComponent<PauseMenu>().isPaused)
            {
                playerCamera2.transform.position = new Vector3(transform.position.x, transform.position.y + cameraYOffset,
                    transform.position.z);
            }
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
        isDead = false;
        characterController = GetComponent<CharacterController>();
        healthBar = GameObject.Find("Healthbar").GetComponent<Slider>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        healthbar = GameObject.Find("Health Bar");
        GameManager = GameObject.Find("GameManager");
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

        // Check if the player is either dead or is in the pause menu
        MenuCheck();

        // Player and Camera rotation
        if (canMove && playerCamera2 != null)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera2.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        
        // Calls the death method when health reaches 0
        if (healthBar.value <= 0 && !isDead)
        {
            Debug.Log("Deded again again");
            Death();
        }

        if(Input.GetButton("Fire1") && canMove)
        {
           // This.Gun.shoot();
        }
    }
    
    public void MenuCheck()
    {
        if (!isDead && !GameObject.Find("GameManager").GetComponent<PauseMenu>().isPaused)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    [ServerRpc (RequireOwnership = false)]
    // Disable mouse cursor
    public void Death()
    {
        isDead = true;
        canMove = false;
        GameManager.GetComponent<GameOver>().PlayerReference(gameObject);
        gameObject.SetActive(false);
    }

    [ServerRpc (RequireOwnership = false)]
    public void Alive(bool Respawn)
    {
        if (Respawn)
        {
            //this.gameObject.GetComponent<HealthSync>().SetHealth();
            isDead = false;
            canMove = true;
        }
    }

    public void GotHit(float damage)
    {
        healthbar.GetComponent<Healthbar>().TakeDamage(damage);
    }
}
