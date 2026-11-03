using UnityEngine;

public class MainSettingsButton : MonoBehaviour
{
    public GameObject settingsPanel;
    private bool open = false;

    public void Settings()
    {
        if (open == false)
        {
        settingsPanel.SetActive(true);
        open = true;
        }
        else if (open == true)
        {
            settingsPanel.SetActive(false);
            open = false;
        }

    }
}
