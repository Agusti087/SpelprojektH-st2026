using System.Collections;
using System;
using UnityEngine;
using UnityEditor.SceneManagement;

using UnityEngine.SceneManagement;

public class FreezeMechanic : MonoBehaviour
{
    public bool freezing;
    public float freezeTime = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (freezeTime <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("Death Screen");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cold")
        {
            freezing = true;
            StartCoroutine(freezeTimer());
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cold")
        {
            freezing = false;
        }
    }
    IEnumerator freezeTimer()
    {
        if (freezing == true)
        {
            yield return new WaitForSeconds(1);
            freezeTime --;
            StartCoroutine(freezeTimer());
            
        }
        else
        {
            freezeTime ++;
            if(freezeTime < 15) StartCoroutine(freezeTimer());
            else yield break;
        }
    }

}
