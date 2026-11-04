using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //standard Movement
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
    [SerializeField] bool isSprinting;
    public bool canSprint;
    private bool stoppedSprint;
    [SerializeField] bool reached0;
    [SerializeField] int usePenalty; //tiden du väntar innan du kan springa igen ifall stamina tar slut - oskar

    //crouch
    Transform crouch;
    [SerializeField] GameObject Player;
    private float RealHeight = 1f;
    private float currentHeight;
    private float crouchHeight = 0.6f;
    [SerializeField] bool isCrouched;
    private bool StoppedCrouch;
    private bool crouchInput;
    private float crouchSpeed;



    private void Awake()
    {
        characterController = new CharacterController(); //character controller - oskar
        rb = GetComponent<Rigidbody2D>();
        crouch = gameObject.GetComponent<Transform>();

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
        currentHeight = RealHeight;
        stamina = max_stamina;
        canSprint = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        StaminaHandler();
        CrouchHandler();
    }
    


    public float lastMoveInputMagnitude;
    private void MovePlayer()
    {
        moveInput = characterController.Player.Movement.ReadValue<Vector2>(); //ger W och A ett value som går att läsa - oskar
        sprintInput = characterController.Player.Sprint.IsPressed(); //ger skift en bool - oskar
        stoppedSprint = characterController.Player.Sprint.WasReleasedThisFrame();
        crouchInput = characterController.Player.crouch.IsPressed();
        StoppedCrouch = characterController.Player.crouch.WasReleasedThisFrame();

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
            isSprinting = true;
            shouldReg = false;
            isCrouched = false;
            StoppedCrouch = true;
        }
        if (isSprinting)
        {
            stamina -= staminaDrain * Time.deltaTime;
            speedMulti = 1.4f;
        }
        else
        {
            speedMulti = 1.0f;
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
        if (isSprinting && canSprint == false) //debug ifall något går fel - oskar
        {
            StartCoroutine(StopSprint());
        }

    }
    IEnumerator stoppedAt0() //om stamina nådde 0 behöver du vänta ännu längre innan du kan springa - oskar
    {
        canSprint = false;
        yield return new WaitForSeconds(usePenalty);
        canSprint = true;
        reached0 = false;
    }
    IEnumerator StopSprint() //väntar en stund innan stamina börjar regenerera - oskar
    {
        isSprinting = false;
        yield return new WaitForSeconds(2);
        stoppedSprint = false;
        shouldReg = true;
    }

    public void CrouchHandler()
    {
        Player.transform.localScale = new Vector3(1, currentHeight, 1);
        if (crouchInput && isSprinting == false) //kan inte börja croucha om du springer - oskar
        {
            currentHeight = crouchHeight;
            isCrouched = true;
            speedMulti = 0.6f;        
        }
        if (StoppedCrouch)
        {
            currentHeight = RealHeight; 
            if (sprintInput == false) //kollar om du börjar springa och låter den ta hand om speed iställer - oskar
            {
                speedMulti = 1f;
            }

        }
        
    }

}
