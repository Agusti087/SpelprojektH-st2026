using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class BackToMainMenu : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
    public void GoBack()
    {

        StartCoroutine(LoadGame());

    }

    IEnumerator LoadGame()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene("Main Menu");
    }
}
