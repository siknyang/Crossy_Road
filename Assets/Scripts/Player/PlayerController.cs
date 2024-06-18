using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 curMovementInput;
    public float moveSpeed;
    public LayerMask groundLayerMask;

    Rigidbody rb;
    PlayerInput input;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input = new PlayerInput();

        input.Player.Move.performed += PlayerMove;
        input.Player.Move.canceled += PlayerStop;
        input.Player.Jump.started += PlayerJump;

        input.Enable();
    }
    
    private void OnDisable()
    {
        input.Player.Move.performed -= PlayerMove;
        input.Player.Move.canceled -= PlayerStop;
        input.Player.Jump.started -= PlayerJump;

        input.Disable();
    }

    private void FixedUpdate()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;    // transform: 방향, curMovementInput: 거리
        dir *= moveSpeed;
        dir.y = rb.velocity.y;    // 점프했을 때만 변화가 있도록 기본 값을 0으로 설정

        rb.velocity = dir;
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            rb.AddForce(Vector2.up * 5, ForceMode.Impulse);
        }
    }

    private void PlayerMove(InputAction.CallbackContext value)
    {
        curMovementInput = value.ReadValue<Vector2>();
        
    }

    private void PlayerStop(InputAction.CallbackContext value)
    {
        curMovementInput = Vector2.zero;
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
