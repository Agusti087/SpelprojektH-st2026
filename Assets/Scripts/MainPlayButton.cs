using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class MainPlayButton : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    public void Play()
   {

        StartCoroutine(LoadGame());

    }

    IEnumerator LoadGame()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Jacks");
    }
}
