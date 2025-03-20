using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour 
{
    public CharacterController PlayerController;

    public PlayerInput PlayerInput;

    public Vector3 MoveVector;

    public Vector2 RotateVector;

    public Vector2 InputVector;

    public float PlayerWalkSpeed;

    public float PlayerRotationSpeed;

    private Vector3 gravityVector;

    private void Awake()
    {
        PlayerController = GetComponent<CharacterController>();
        PlayerInput = GetComponent<PlayerInput>();
        PlayerWalkSpeed = 0.05f;
        PlayerRotationSpeed = 10;

        gravityVector = new Vector3(0, -9.81f, 0);

    }

    private void Update()
    {
        MovePlayer();
        RotateTowardsVector();
        ApplyGravityToPlayer();
    }

    private void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();
        MoveVector.x = InputVector.x;
        MoveVector.z = InputVector.y;
    }

    private void OnLook(InputValue value)
    {
        RotateVector = value.Get<Vector2>();
    }

    public void MovePlayer()
    {
        PlayerController.Move(PlayerWalkSpeed * MoveVector);
    }

    public void ApplyGravityToPlayer()
    {
        PlayerController.Move(gravityVector * Time.deltaTime);
    }

    public void RotateTowardsVector()
    {
        Vector3 xzDirection = new Vector3(MoveVector.x, 0, MoveVector.z);

        if(xzDirection.magnitude == 0)
        {
            return;
        }

        Quaternion rotation = Quaternion.LookRotation(xzDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * PlayerRotationSpeed);

        //transform.localRotation = Quaternion.Euler(0, RotateVector.x, 0);
    }
}
