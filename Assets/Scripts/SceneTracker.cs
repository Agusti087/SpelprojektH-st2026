using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTracker : MonoBehaviour
{
    public Monsterappeargen myMonsterappeargen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Awake()
    {
        Debug.Log("awake");
        DontDestroyOnLoad(this);
    }
    public static bool MonsterExists = false;
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        MonsterExists = false;
    }
    private void OnEnable()
    {
        Debug.Log("onEnableCalled");
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded += myMonsterappeargen.OnSceneLoaded;
    }

    private void OnDisable()
    {
        Debug.Log("onDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded -= myMonsterappeargen.OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
