using UnityEngine;
using TMPro;
public class Staminabar : MonoBehaviour
{
    
    public GameObject player;
    [SerializeField] TMP_Text uitext;
    PlayerMovement StaminaChecker;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       StaminaChecker = player.GetComponent<PlayerMovement>(); //fr movement script - oskar  
    }

    // Update is called once per frame
    void Update()
    {
        uitext.text = "stamina = " + StaminaChecker.stamina; //skriver ut staminan - oskar
    }
}
