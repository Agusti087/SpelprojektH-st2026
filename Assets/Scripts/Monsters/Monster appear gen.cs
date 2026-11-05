using UnityEngine;

public class Monsterappeargen : MonoBehaviour
{
    float AppearChance;
    int EventAmount; //
    float AppearDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Monsterappeargen(float _AppearChance, int _EventAmount, float _AppearDelay)
    {
        this.AppearChance = _AppearChance;
        this.EventAmount = _EventAmount;
        this.AppearDelay = _AppearDelay;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
