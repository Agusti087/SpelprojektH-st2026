using System.Linq;
using UnityEngine;

public class BaseTeleporter : MonoBehaviour
{
    public AudioSource mainTheme;
    public AudioSource ambiance;
    public GameObject telePos;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BaseTeleporter")
        {
            transform.position = telePos.transform.position;
            mainTheme.Stop();
            ambiance.Play();
        }
        
    }
}
