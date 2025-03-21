using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour 
{
    public CharacterController PlayerController;

    public GameObject FireBallPoint;

    public GameObject ProjectilePrefab;

    public AudioSource AudioSource;

    public GameObject InventoryWindow;

    public GameObject CameraObject;

    public PlayerInput PlayerInput;

    public Animator PlayerAnimator;

    public Vector3 MoveVector;

    public Vector2 RotateVector;

    public Vector2 InputVector;

    public float PlayerWalkSpeed;

    public float PlayerRotationSpeed;

    private Vector3 gravityVector;

    private float targetRotationY;

    private Vector3 movementInput;

    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerController = GetComponent<CharacterController>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerAnimator = GetComponent<Animator>();

        PlayerWalkSpeed = 6f;
        PlayerRotationSpeed = 10;

        gravityVector = new Vector3(0, -9.81f, 0);

    }

    private void Update()
    {
        RotateTowardsCamera();

        MovePlayer();
        
        ApplyGravityToPlayer();

        SetWalkingAnimation();



    }

    private void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();

        movementInput = new Vector3(InputVector.x, 0, InputVector.y).normalized;
        PlayerAnimator.SetFloat("moveX", movementInput.x);
        PlayerAnimator.SetFloat("moveY", movementInput.z);

    }

    private void OnAttack()
    {
        PlayerAnimator.SetTrigger("pAttacking");
        StartCoroutine(WaitForAnimation());

        
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject fireball = Instantiate(ProjectilePrefab, FireBallPoint.transform.position, transform.rotation);
        fireball.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 60));

    }

    private void OnLook(InputValue value)
    {
        RotateVector = value.Get<Vector2>();
        targetRotationY += RotateVector.x;
        
    }

    public void MovePlayer()
    {

        if (movementInput != Vector3.zero)
        {
            PlayerController.Move(MoveVector * PlayerWalkSpeed * Time.deltaTime);
        }
    }

    public void SetWalkingAnimation()
    {

        if (movementInput != Vector3.zero)
        {
            PlayerAnimator.SetBool("pWalking", true);
        }
        else
        {
            PlayerAnimator.SetBool("pWalking", false);
        }
    }

    public void ApplyGravityToPlayer()
    {
        PlayerController.Move(gravityVector * Time.deltaTime);
    }

    public void RotateTowardsCamera()
    {
        Quaternion targetRotation = Quaternion.Euler(0, targetRotationY, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * PlayerRotationSpeed);

        if (movementInput != Vector3.zero)
        {
            Vector3 cameraForward = CameraObject.transform.forward;
            Vector3 cameraRight = CameraObject.transform.right;

            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            MoveVector = cameraForward * movementInput.z + cameraRight * movementInput.x;
        }
    }

    private void OnToggleInventory()
    {
        if (!InventoryWindow.activeSelf)
        {
            AudioSource.Play();
            InventoryWindow.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            AudioSource.Play();
            InventoryWindow.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }
}
