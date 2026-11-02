using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    Rigidbody2D rb;
    public float playerSpeed = 7f;
    
    private Vector2 moveInput;
    private bool jumpInput;
    
    
    private void Awake()
    {
        characterController = new CharacterController();
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void OnEnable()
    {
        characterController.Enable();
    }

    private void OnDisable()
    {
        characterController.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

    }

    

    private void MovePlayer()
    {
        moveInput = characterController.Player.Movement.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveInput.x * playerSpeed, rb.linearVelocity.y);
        
    }



}
