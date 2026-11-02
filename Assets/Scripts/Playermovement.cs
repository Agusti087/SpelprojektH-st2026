using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    Rigidbody2D rb;
    public float playerSpeed = 7f;
    public float speedMulti;
    private Vector2 moveInput;
    private bool sprintInput;
    
    
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
        if(sprintInput)
        {
            speedMulti = 1.4f;
        }
        else
        {
            speedMulti = 1f;
        }

    }

    

    private void MovePlayer()
    {
        moveInput = characterController.Player.Movement.ReadValue<Vector2>();
        rb.linearVelocity = new Vector2(moveInput.x * playerSpeed, rb.linearVelocity.y);
        


        
    }



}
