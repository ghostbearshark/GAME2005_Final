using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void EnterSimulation ()
     {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

     }

     public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }


    public void Play()
    {
        PlayerBehaviour.isPlaying = true;

    }

}
