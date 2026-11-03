using UnityEngine;
using System.Collections;

public class hide : MonoBehaviour
{
    public bool enterable;
    public bool hidden;
    public bool EnterSwitch = false;
    
    public SpriteRenderer spriterenderer;
    
    private bool EnterInput;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        
        spriterenderer = gameObject.GetComponent<SpriteRenderer>();
        
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //när hidden är true blir du onsynlig, slutar kunna röra dig och spelarens tag ändras från exposed till hidden. annars funkar allt som normalt - Jack
        if (hidden == true)
        {
            spriterenderer.enabled = false;
            gameObject.transform.GetComponent<PlayerMovement>().enabled = false;
            gameObject.tag = "Hidden";
        }
        else
        {
            spriterenderer.enabled = true;
            gameObject.transform.GetComponent<PlayerMovement>().enabled = true;
            gameObject.tag = "Exposed";
        }
        if (Input.GetKeyDown(KeyCode.E) && enterable == true)
        {
            hidden = !hidden;
        }
        //debug för om man hamnar iväg från skåpet medans spelet tror att du är i det - Jack
        if (hidden && enterable == false)
        {
            hidden = false;
        }
    }
       
    private void OnTriggerEnter2D(Collider2D other)//när du går in i en trigger tittar den taggen av triggern och om det är enterable så gör den boolen enterable till true - Jack
    {

        if (other.gameObject.tag == "Enterable")
        {
            enterable = true;
            Debug.Log("gå in fy fan");
        }     
    }
    private void OnTriggerExit2D(Collider2D collision)//när du lämnar enterable triggern så gör den enterable till false igen - Jack
    {
        enterable = false;
    }
}
