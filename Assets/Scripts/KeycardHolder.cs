using UnityEngine;

public enum KeycardTypes
{
    Door0,
    wall0,
    Total
}
public class KeycardHolder : MonoBehaviour
{
    public bool[] keycards = new bool[(int) KeycardTypes.Total];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
