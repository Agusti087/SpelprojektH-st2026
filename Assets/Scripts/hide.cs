using UnityEngine;
using UnityEngine.Android;
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

        if (hidden == true)
        {
            spriterenderer.enabled = false;
            gameObject.transform.GetComponent<PlayerMovement>().enabled = false;
        }
        else
        {
            spriterenderer.enabled = true;
            gameObject.transform.GetComponent<PlayerMovement>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && enterable == true && EnterSwitch == false)
        {
            hidden = !hidden;
        }
    }
       
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enterable")
        {
            enterable = true;
            Debug.Log("gå in fy fan");
        }     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        enterable = false;
    }
}
