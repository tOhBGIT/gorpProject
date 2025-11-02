using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{

    public string[] scenes = new string[0];
    public AudioSource source;
    public AudioClip selectSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void loadScene(string sceneName)
    {
        int toLoad = 0;
        //read the name goober
        for (int i = 0;  i < scenes.Length; i++)
        {
            if (scenes[i] == sceneName)
            {
                toLoad = i;
            }
            else
            {
                Debug.Log("no match");
            }
        }
        StartCoroutine(waitSFX(toLoad));
        
    }

    IEnumerator waitSFX(int load)
    {
        source.PlayOneShot(selectSound);
        yield return new WaitForSeconds(selectSound.length);
        SceneManager.LoadScene(load);
    }
}
