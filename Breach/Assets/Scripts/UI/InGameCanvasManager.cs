using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvasManager : MonoBehaviour
{
    public void ReloadScreen()
    { 
        GameManager.instance.RestartLevel();
    }

    public void GoToMainMenu()
    { 
        GameManager.instance.GoToMainMenu();
    }

    public void GoToNextLevel()
    { 
        GameManager.instance.LoadNextScene();
    }


}
