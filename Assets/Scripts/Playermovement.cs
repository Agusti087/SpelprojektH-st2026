using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;

    Rigidbody2D rb;
    public float playerSpeed = 7f;
    public float speedMulti;
    private Vector2 moveInput;
    private bool sprintInput;

    //stamina
    public float max_stamina = 100f;
    public float stamina;
    private float staminaDrain = 16.66f;
    private float staminaReg = 10f;
    private bool shouldReg; //bool för att veta om spelaren ska få stamina tillbaka
    private bool isSprinting;
    public bool canSprint;
    private bool stoppedSprint;
    private bool reached0;
    [SerializeField] int usePenalty; //tiden du väntar innan du kan springa igen ifall stamina tar slut - oskar





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
        stamina = max_stamina;
        canSprint = true;
    }

    // Update is called once per frame
    void Update()
    {
        StaminaHandler();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }


    public float lastMoveInputMagnitude = 0.0f;
    private void MovePlayer()
    {
        moveInput = characterController.Player.Movement.ReadValue<Vector2>(); //ger W och A ett value som går att läsa - oskar
        sprintInput = characterController.Player.Sprint.IsPressed(); //ger skift en bool - oskar
        stoppedSprint = characterController.Player.Sprint.WasReleasedThisFrame();

        rb.linearVelocity = new Vector2(moveInput.x * playerSpeed * speedMulti, rb.linearVelocity.y); //tar movement value och flyttar på spelaren - oskar
        if (lastMoveInputMagnitude > 0.0f)
        {
            if (Mathf.Abs(moveInput.magnitude) < 0.001f)
            {
                rb.linearVelocity= new Vector2(0,0);
            }
        }
        lastMoveInputMagnitude = Mathf.Abs(moveInput.magnitude);


    }
    public void StaminaHandler()
    {
        if (sprintInput && canSprint == true) // om skift = speed multi, annars är den 1 - oskar
        {
            speedMulti = 1.4f;
            isSprinting = true;
            shouldReg = false;
        }
        else
        {
            speedMulti = 1f;
        }
        if (isSprinting)
        {
            stamina -= staminaDrain * Time.deltaTime;
        }
        if (stamina <= 0f) //kollar om stamina nådde 0
        {
            canSprint = false;
            stamina = 0f;
            reached0 = true;
        }
        if (!isSprinting && shouldReg)
        {
            stamina += staminaReg * Time.deltaTime;
        }
        if (stoppedSprint == true)
        {
            if (reached0)
            {
                StartCoroutine(stoppedAt0());
            }
            StartCoroutine(StopSprint());
        }
        if (stamina > max_stamina || stamina == max_stamina) //stänger av stamina regen om du når max - oskar
        {
            stamina = max_stamina;
            shouldReg = false;
        }

    }
    IEnumerator stoppedAt0() //om stamina nådde 0 behöver du vänta ännu längre innan du kan springa - oskar
    {
        canSprint = false;
        yield return new WaitForSeconds(usePenalty);
        canSprint = true;
    }
    IEnumerator StopSprint() //väntar en stund innan stamina börjar regenerera - oskar
    {
        isSprinting = false;
        yield return new WaitForSeconds(2);
        stoppedSprint = false;
        shouldReg = true;
    }
}
