using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene1");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
