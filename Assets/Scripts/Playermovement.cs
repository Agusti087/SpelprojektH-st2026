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
        characterController = new CharacterController(); //character controller - oskar
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void OnEnable() 
    {
        characterController.Enable(); //sätter igång character controllern - oskar
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
        moveInput = characterController.Player.Movement.ReadValue<Vector2>(); //ger W och A ett value som går att läsa - oskar
        sprintInput = characterController.Player.Sprint.IsPressed(); //ger skift en bool - oskar
        if (sprintInput) // om skift = speed multi, annars är den 1 - oskar
        {
            speedMulti = 1.4f;
        }
        else
        {
            speedMulti = 1f;
        }
        rb.linearVelocity = new Vector2(moveInput.x * playerSpeed * speedMulti, rb.linearVelocity.y); //tar movement value och flyttar på spelaren - oskar
        


        
    }



}
