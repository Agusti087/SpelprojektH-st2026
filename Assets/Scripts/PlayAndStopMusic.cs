using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayAndStopMusic : MonoBehaviour
{
    public AudioSource ambiance;
    public AudioSource mainTheme;
    public bool InBase = false;

    void Start()
    {
        if (InBase == false)
        {
            mainTheme.Play();
        }
        else if (InBase == true)
        {
            ambiance.Play();
        }
    }
}
