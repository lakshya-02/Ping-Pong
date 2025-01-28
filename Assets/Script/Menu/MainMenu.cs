using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public int val;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        val = 0;

    }
    public void PlayGame2()
    {   //crazymode
        SceneManager.LoadSceneAsync(1);
        val = 1;

    }

    public int GetVal()
    {
        return val;
    }

    public void MultiPlayer()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
