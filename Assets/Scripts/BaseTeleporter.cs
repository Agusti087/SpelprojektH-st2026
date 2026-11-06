using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseTeleporter : MonoBehaviour
{
    public AudioSource mainTheme;
    public AudioSource ambiance;
    public GameObject telePos;
    public GameObject GLight;
    public GameObject camera1;
    public GameObject camera2;
    public Animator Crossfade;

    void Start()
    {
        camera2.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BaseTeleporter")
        {
            StartCoroutine(LongAssFunctionThatIHateWriting());
        }

        IEnumerator LongAssFunctionThatIHateWriting()
        {
            Crossfade.SetTrigger("Start");
            gameObject.transform.GetComponent<PlayerMovement>().enabled = false;
            yield return new WaitForSeconds(1);
            Crossfade.SetTrigger("End");
            gameObject.transform.GetComponent<PlayerMovement>().enabled = true;
            transform.position = telePos.transform.position;
            mainTheme.Stop();
            ambiance.Play();
            GLight.SetActive(false);
            camera1.SetActive(false);
            camera2.SetActive(true);
        }
    }
}
