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
    public float lastMoveInputMagnitude;
    private Animator playerAnim;

    //stamina
    public float max_stamina = 100f;
    public float stamina;
    private float staminaDrain = 16.66f;
    private float staminaReg = 10f;
    private bool shouldReg; //bool f�r att veta om spelaren ska f� stamina tillbaka
    [SerializeField] bool isSprinting;
    public bool canSprint;
    private bool stoppedSprint;
    [SerializeField] bool reached0;
    [SerializeField] int usePenalty; //tiden du v�ntar innan du kan springa igen ifall stamina tar slut - oskar

    //Hide
    private bool EPressed;
    public AudioSource PlayerAudio;
    public AudioClip LockerEnter;
    public AudioClip LockerExit;
    public bool enterable;
    public bool hidden;
    public bool EnterSwitch = false;
    [SerializeField] bool canChangeState = true;

    public GameObject playerLantern;
    public GameObject playerSpriteLight;
    public GameObject playerLanternDimmed;

    private GameObject LockerPos;

    private Animator LockerAnims;
    


    public SpriteRenderer spriterenderer;

    private bool EnterInput;

    public KeycardHolder KeycardHolderPlayer = null;

    public static PlayerMovement GlobalPlayer = null;

    private void Awake()
    {
        GlobalPlayer = this;
        characterController = new CharacterController(); //character controller - oskar
        rb = GetComponent<Rigidbody2D>();
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        

    }

    private void OnEnable()
    {
        characterController.Enable(); //s�tter ig�ng character controllern - oskar
    }

    private void OnDisable()
    {
        characterController.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnim = GetComponent<Animator>();

        DontDestroyOnLoad(this.gameObject);
        stamina = max_stamina;
        canSprint = true;
        playerLanternDimmed.SetActive(false);        
    }


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        StaminaHandler();      
        HidePlayer();
    }

    private void MovePlayer()
    {
        if (gameObject.tag == "Exposed")
        {
            moveInput = characterController.Player.Movement.ReadValue<Vector2>(); //ger W och A ett value som g�r att l�sa - oskar
            moveInput = characterController.Player.Movement.ReadValue<Vector2>(); //ger W och A ett value som går att läsa - oskar

            sprintInput = characterController.Player.Sprint.IsPressed(); //ger skift en bool - oskar
            stoppedSprint = characterController.Player.Sprint.WasReleasedThisFrame(); //kollar om du nyss slutade hålla in - oskar
        }
        

        rb.linearVelocity = new Vector2(moveInput.x * playerSpeed * speedMulti, rb.linearVelocity.y); //tar movement value och flyttar p� spelaren - oskar
        if (lastMoveInputMagnitude > 0.0f)
        {
            if (Mathf.Abs(moveInput.magnitude) < 0.001f)
            {
                rb.linearVelocity = new Vector2(0, 0);
            }
        }
        lastMoveInputMagnitude = Mathf.Abs(moveInput.magnitude);


    }
    public void StaminaHandler()
    {

        if (sprintInput && canSprint == true) // om skift = speed multi, annars �r den 1 - oskar
        {
            isSprinting = true;
            shouldReg = false;
            
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
        if (stamina <= 0f) //kollar om stamina n�dde 0
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
        if (stamina > max_stamina || stamina == max_stamina) //st�nger av stamina regen om du n�r max - oskar
        {
            stamina = max_stamina;
            shouldReg = false;
        }
        if (isSprinting && canSprint == false) //debug ifall n�got g�r fel - oskar
        {
            StartCoroutine(StopSprint());
        }

    }
    IEnumerator stoppedAt0() //om stamina n�dde 0 beh�ver du v�nta �nnu l�ngre innan du kan springa - oskar
    {
        canSprint = false;
        yield return new WaitForSeconds(usePenalty);
        canSprint = true;
        reached0 = false;
    }
    IEnumerator StopSprint() //v�ntar en stund innan stamina b�rjar regenerera - oskar
    {
        isSprinting = false;
        yield return new WaitForSeconds(2);
        stoppedSprint = false;
        shouldReg = true;
    }

    
    private void HidePlayer()
    {
        EPressed = characterController.Player.Hide.WasPressedThisFrame();
        //när hidden är true blir du onsynlig, slutar kunna röra dig och spelarens tag ändras från exposed till hidden. annars funkar allt som normalt - Jack
        if (EPressed)
        {
            Debug.Log("JÄVLA SKITUNGE");
        }
        if (hidden == true)
        {
            spriterenderer.enabled = false;   
            gameObject.tag = "Hidden";
            rb.linearVelocity = Vector2.zero;
            LockerAnims.SetBool("Hidden", true);
            LockerAnims.SetBool("CurrentlyInside", true);
        }
        else
        {
            spriterenderer.enabled = true;
            gameObject.transform.GetComponent<PlayerMovement>().enabled = true;
            gameObject.tag = "Exposed";
            LockerAnims.SetBool("Hidden", false);
            LockerAnims.SetBool("CurrentlyInside", false);
        }
        if (EPressed && hidden == false && enterable == true && canChangeState == true)
        {
            PlayerAudio.PlayOneShot(LockerEnter);
            playerLantern.SetActive(false);
            playerSpriteLight.SetActive(false);
            playerLanternDimmed.SetActive(true);
            transform.position = LockerPos.transform.position;

        }
        if (EPressed && hidden == true && enterable == true && canChangeState == true)
        {
            PlayerAudio.PlayOneShot(LockerExit);
            playerLantern.SetActive(true);
            playerSpriteLight.SetActive(true);
            playerLanternDimmed.SetActive(false);
        }
        if (EPressed && enterable == true && canChangeState == true)
        {
            hidden = !hidden;
            StartCoroutine(TimeDelay());
        }
        //debug för om man hamnar iväg från skåpet medans spelet tror att du är i det - Jack
        if (hidden && enterable == false)
        {
            hidden = false;

        }
    }
    IEnumerator TimeDelay()
    {
        canChangeState = false;
        yield return new WaitForSeconds(2);
        canChangeState = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enterable")
        {
            LockerAnims = other.gameObject.GetComponent<Animator>();
            LockerPos = other.gameObject;
            enterable = true;
            Debug.Log("gå in fy fan");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enterable = false;
    }
}
    


