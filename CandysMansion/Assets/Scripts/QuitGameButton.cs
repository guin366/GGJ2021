using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public Button quitButton;

    public void quitButtonPressed()
    {
       
        Loader.Load(Loader.Scene.Menu);
   
    }
}
