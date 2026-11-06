using UnityEngine;

public class QuickTimeEventTrigger : MonoBehaviour
{
    public GameObject QuickTimeEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void startQuick()
    {
            QuickTimeEvent.SetActive(true);    

    }
}
