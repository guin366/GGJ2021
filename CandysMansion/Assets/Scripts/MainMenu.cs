using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
   
  public void PlayGame ()
    {
        Debug.Log("load");
        Loader.Load(Loader.Scene.PostOffice1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

}
