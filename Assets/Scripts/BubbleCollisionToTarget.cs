using UnityEngine;

public class BubbleCollisionToTarget : MonoBehaviour
{
    public StairScript StairParent = null;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StairParent.OnTriggerEnter2D(collision); //kollar vad som collida med triggern och skickar till parent - oskar
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StairParent.OnTriggerExit2D(collision); 
    }
}
