using UnityEngine;

public class KeycardDoor : MonoBehaviour
{
    public KeycardHolder playerKeycardScript = null;
    public KeycardTypes keycardNumberToOpenThisDoor = KeycardTypes.Door0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player == null)
        {
            Debug.Log("Door0 trigger collided with non player object");
            return;
        }

        if (player.KeycardHolderPlayer.keycards[(int)keycardNumberToOpenThisDoor])
        {
            OpenDoor();
        }
        else
        {
            //BeepMissingKeycard();
        }
    }
    public void OpenDoor()
    {
        gameObject.SetActive(false);

    }

}
