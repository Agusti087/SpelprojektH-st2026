using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MonsterType
{
    Monster1,
    Monster2,
    Total
}
public class Monsterappeargen : MonoBehaviour
{
    //public static Dictionary<string, Monsterappeargen> GlobalMonsterappeargenMap = new Dictionary<string,Monsterappeargen>();

    public Transform SpawnLoc;
    public MonsterType CurrentMonsterSpawned = MonsterType.Total;
    public float[] AppearChance = new float[(int)MonsterType.Total];
    public int[] EventAmount = new int[(int)MonsterType.Total];
    public float[] AppearDelay = new float[(int)MonsterType.Total];
    public GameObject[] MonsterList = new GameObject[(int)MonsterType.Total];

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int index = 0;
        foreach (var appearChance in AppearChance) 
        {
            MonsterType monsterTypeBeingChecked = (MonsterType)index;
            if (Random.value < appearChance && SceneTracker.MonsterExists == false)
            {
                CurrentMonsterSpawned = monsterTypeBeingChecked; //berättar vilket monster som kommer spawna - oskar
                SceneTracker.MonsterExists = true;
                StartCoroutine(Appear());
                Debug.Log("imma spawn");
                break;

            }
            index++;
        }
    }
    
    IEnumerator Appear()
    {        
        yield return new WaitForSeconds(AppearDelay[(int)CurrentMonsterSpawned]);
        Instantiate(MonsterList[(int)CurrentMonsterSpawned], SpawnLoc);
    }
    public void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

}
//public void SetMonsterValues(MonsterType _monsterType,float _AppearChance, int _EventAmount, float _AppearDelay, GameObject _Monster)
//{
//    this.AppearChance[(int)_monsterType] = _AppearChance;
//    this.EventAmount[(int)_monsterType] = _EventAmount;
//    this.AppearDelay[(int)_monsterType] = _AppearDelay;
//    this.MonsterList[(int)_monsterType] = _Monster;
//}
