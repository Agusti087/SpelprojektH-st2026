using UnityEngine;

public class StairScript : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] Collider2D StairCol;
    [SerializeField] Collider2D FloorCol;
    [SerializeField] Collider2D underCheck;

    [SerializeField] bool Activate;
    private bool SomeoneIsUnder;

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StairCol.enabled = false;
        FloorCol.enabled = true;
    }
    private void Awake()
    {
        characterController = new CharacterController(); //character controller - oskar
        
    }

    private void OnEnable()
    {
        characterController.Enable(); //sätter igång character controllern - oskar
    }

    private void OnDisable()
    {
        characterController.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        StairHandler();
    }

    private void StairHandler()
    {
        Activate = characterController.Stair.Stair.IsPressed();

        if (Activate && SomeoneIsUnder == false) //om ingen är bakom trappan och du har aktiverat stängs collision av för
        {                                        //golvet över och collision sätts på för trappan
            Debug.Log("imma walk up 'ere");
            StairCol.enabled = true;
            FloorCol.enabled = false;
        }
        else
        {
            FloorCol.enabled = true;
            StairCol.enabled = false;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision) //om triggern märker av någon bakom trappan
    {
        Debug.Log("Im undah here");
        if (collision.gameObject.tag == "Exposed" || collision.gameObject.tag == "Hidden") 
        {
            SomeoneIsUnder = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Im Outta here");
        if (collision.gameObject.tag == "Exposed" || collision.gameObject.tag == "Hidden")
        {
            SomeoneIsUnder = false;
        }
    }
}
