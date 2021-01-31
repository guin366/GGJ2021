using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public static class Loader
{
    public enum Scene{
        Menu, PostOffice1, BjornLevel, PostOffice2, SamsWildAdventure, PostOffice3, SoapLevel, Loading,
    }

    private static Action onLoaderCallback;
    // Start is called before the first frame update
    public static void Load(Scene scene)
    {

        onLoaderCallback = () => {
            SceneManager.LoadScene(scene.ToString());
        };
        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    public static void LoaderCallback()
    {
        if(onLoaderCallback !=null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
