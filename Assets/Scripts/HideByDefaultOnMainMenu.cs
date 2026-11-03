using UnityEngine;

public class HideByDefaultOnMainMenu : MonoBehaviour
{
    public GameObject settingsPanel;



    void Start()
    {
        settingsPanel.SetActive(false);
    }

   
}
