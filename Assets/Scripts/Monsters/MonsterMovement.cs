using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] QuickTimeEventTrigger QuickStart;
    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    private void Awake()
    {
        Player = FindAnyObjectByType<PlayerMovement>().gameObject;
        QuickStart = FindAnyObjectByType<QuickTimeEventTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        Debug.Log(Player.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Player && Player.tag == "Hidden")
        {
            QuickStart.startQuick();
            Destroy(gameObject);
        }
        if (other.gameObject == Player &&  Player.tag == "Exposed")
        {
            SceneManager.LoadScene("Death Screen");
            Player.SetActive(false);
        }
    }
    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);
    }
}
