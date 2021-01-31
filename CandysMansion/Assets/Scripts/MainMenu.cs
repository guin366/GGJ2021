using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{
   
  public void PlayGame ()
    {
        Loader.Load(Loader.Scene.PostOffice1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

}
