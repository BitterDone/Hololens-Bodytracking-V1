using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;

public class GoToNext : MonoBehaviour
{
    /*----------------------------------
     * Code to handle moving from set up scene to set up scene
     * 
     * MUST BE SET UP MANUALLY IN THE INSPECTOR
     * ---------------------------------*/

    public List<SceneLoadInformation> LoaderInformation = new List<SceneLoadInformation>();//holds data for all ways to load scenes


    public void Start()
    {
        Debug.Log("Starting scene Loader set up");
        for (int i = 0; i < LoaderInformation.Count; i++)
        {
            EnumSwitchSetUp(LoaderInformation[i]);
        }
    }

    private void EnumSwitchSetUp(SceneLoadInformation LI)
    {
        switch (LI.loadMethod)
        {
            case SceneLoadInformation.LoadMethod.Next:
                AddGoToNext(LI);
                break;
            case SceneLoadInformation.LoadMethod.Previous:
                AddGoToPrevious(LI);
                break;
            case SceneLoadInformation.LoadMethod.Direct:
                AddGoToScene(LI);
                break;
            default:
                Debug.LogWarning("No Valid Load Method");
                break;
        }
    }
    
    public void AddGoToNext(SceneLoadInformation li)
    {
        li.Interactable.OnClick.AddListener(() => GoToNextScene(li.loadSceneMode));
    }

    public void AddGoToPrevious(SceneLoadInformation li)
    {
        li.Interactable.OnClick.AddListener(() => GoToPreviousScene(li.loadSceneMode));
    }

    public void AddGoToScene(SceneLoadInformation li)
    {
        li.Interactable.OnClick.AddListener(() => GoToScene(li.sceneName, li.loadSceneMode));
    }

    IEnumerator GoToNextScene(LoadSceneMode loadType = LoadSceneMode.Single)
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        curScene++;

        if (curScene >= SceneManager.sceneCountInBuildSettings)
        {
            curScene = SceneManager.sceneCountInBuildSettings - 1;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(curScene, loadType);

        while(!asyncLoad.isDone)
        {
            yield return null;
        }
        
        if (loadType == LoadSceneMode.Additive)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(curScene));
        }
    }

    public void GoToPreviousScene(LoadSceneMode loadType = LoadSceneMode.Single)
    {
        int curScene = SceneManager.GetActiveScene().buildIndex;
        curScene--;

        if (curScene < 0)
        {
            curScene = 0;
        }

        SceneManager.LoadSceneAsync(curScene, loadType);

        if (loadType == LoadSceneMode.Additive)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(curScene));
        }
    }

    IEnumerator GoToScene(string name, LoadSceneMode loadType = LoadSceneMode.Single)
    {
        if(name.Length <= 0)
        {
            Debug.Log("Invalid Scene Name");
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name, loadType);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (loadType == LoadSceneMode.Additive)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
        }
    }
}

[System.Serializable]
public struct SceneLoadInformation
{
    public Interactable Interactable;
    public enum LoadMethod {Next, Previous, Direct };//ways to move about scenes
    public LoadMethod loadMethod;//select what way to move about scenes
    public LoadSceneMode loadSceneMode;//single load scene methods
    public string sceneName;//only used for direct loading
}
