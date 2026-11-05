using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Variabler
    [SerializeField] Transform target; //Refererar till player.
    [SerializeField] float followSpeed; //Hur lång tid kameran får på sig att flytta sig mot playern.
    Vector3 speed;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position; //Förklarar för datorn om hur kameran ska röra sig.
    }

    
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref speed, followSpeed); //Hela ekvationen sammanställd och instruktionen för att göra kamerans rörelser mer "mjuka".
    }
}
