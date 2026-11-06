using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoom : MonoBehaviour
{
    public Animator Crossfade;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
        [SerializeField] int roomNumber;
        [SerializeField] Vector3 nextPlayerPosition;
        GameObject Player;
    private void Awake()
    {
        Player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exposed") || other.CompareTag("Hidden")) //If the player collides then load the next scene.
        {
            
            Debug.Log(other);
            if (Player == null)
            {
                Player = other.gameObject;
            }
            ChangeScene();
        }
    }
    private void ChangeScene() //Teleports the player before going to the next room.
    {

        StartCoroutine(TimeDelay());

        //Transition.Instance.StartTransition();
        SceneManager.LoadScene(roomNumber);     
        Player.transform.position = nextPlayerPosition;
        

    }
   
    
    IEnumerator TimeDelay()
    {
        Crossfade.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(roomNumber);
        Player.transform.position = nextPlayerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
