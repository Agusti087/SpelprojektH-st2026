using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoom : MonoBehaviour
{
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
        [SerializeField] int roomNumber;
        [SerializeField] Vector3 nextPlayerPosition;
        [SerializeField] GameObject player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exposed") || other.CompareTag("Hidden")) //If the player collides then load the next scene.
        {
            
            Debug.Log(other);
            if (player == null)
            {
                player = other.gameObject;
            }
            ChangeScene();
        }
    }
    private void ChangeScene() //Teleports the player before going to the next room.
    {
        //Transition.Instance.StartTransition();
        SceneManager.LoadScene(roomNumber);     
        player.transform.position = nextPlayerPosition;
        
    }
   
    
    
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
