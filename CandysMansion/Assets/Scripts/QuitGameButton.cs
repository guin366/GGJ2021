using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuitGameButton : MonoBehaviour
{
    public Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        quitButton.onClick.AddListener(quitButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void quitButtonPressed()
    {
        
        print("CLICKED!!!");
        Loader.Load(Loader.Scene.PostOffice1);
   
    }
}
