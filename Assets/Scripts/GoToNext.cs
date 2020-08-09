using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNext : MonoBehaviour
{
    /*----------------------------------
     * Code to handle moving from set up scene to set up scene
     * ---------------------------------*/

    public void GoToNextScene()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        curScene++;

        if (curScene >= SceneManager.sceneCountInBuildSettings)
        {
            curScene = SceneManager.sceneCountInBuildSettings - 1;
        }

        SceneManager.LoadScene(curScene);
    }

    public void GoToPreviousScene()
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        curScene--;

        if (curScene < 0)
        {
            curScene = 0;
        }

        SceneManager.LoadScene(curScene);
    }

    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
