using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //Movement Systems
    private PlayerControls _controls;
    private CharacterController _cc;

    //Movement Parameters
    private Vector2 _move;
    private bool _jump;
    private Vector3 _movement;
    private float _verticalSpeed;
    private const float Gravity = -9.8f;
    private bool _grounded;

    //Movement Values
    [SerializeField] private Transform checkPos;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpSpeed = 9;
    [SerializeField] private float playerSpeed;
   
 
    private void Awake()
    {
        TryGetComponent(out _cc);
        _controls = new PlayerControls();
        
        _controls.Player.Move.performed += tgb => _move = tgb.ReadValue<Vector2>();
        _controls.Player.Move.canceled += tgb => _move = Vector2.zero;
        _controls.Player.Jump.started += tgb => Jump();
        _controls.Player.Jump.canceled += tgb => _jump = false;
    }

    private void OnEnable()
    {
        _controls.Player.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Player.Disable(); 
    }
    
    private void Update()
    {
        _movement = Vector3.zero;
        var xSpeed = _move.y * playerSpeed * Time.deltaTime;
        _movement += transform.forward * xSpeed;
        var ySpeed = _move.x * playerSpeed * Time.deltaTime;
        _movement += transform.right * ySpeed;
        //Gravity
        _verticalSpeed += Gravity * Time.deltaTime;

        _movement += transform.up * (_verticalSpeed * Time.deltaTime);
        if (Physics.CheckSphere(checkPos.position,0.5f, groundMask) && _verticalSpeed <= 0)
        {
            _grounded = true;
            _verticalSpeed = 0;
        }
        else
        {
            _grounded = false;
        }
        
        if (_jump && _grounded)
        {
            //jumpCt = 0;
            _verticalSpeed = jumpSpeed;
            _jump = false;
        }
        
        _cc.Move(_movement);
    }
    
    private void Jump()
    {
        _jump = true;
    }
}
