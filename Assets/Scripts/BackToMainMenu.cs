using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

//READ ME: Jag ändrar den här funktionen till att starta om scenen. Förut var denna script gjord för att ta dig till menyn.


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

        SceneManager.LoadScene("Game");
    }
}
