using UnityEngine;
using UnityEngine.SceneManagement;
public class MainPlayButton : MonoBehaviour
{
   public void Play()
   {

        SceneManager.LoadScene("Game");

   }
}
