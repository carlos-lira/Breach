using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasUI : MonoBehaviour
{
    #region Singleton
    public static MainCanvasUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one MainCanvas in scene");
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelsMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject resetProgressMenu;

    private void Start()
    {
        ActivateMainMenu();
    }

    public void ActivateMainMenu()
    {
        mainMenu.SetActive(true);
        levelsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        resetProgressMenu.SetActive(false);
    }

    public void ActivateLevelMenu()
    { 
        mainMenu.SetActive(false);
        levelsMenu.SetActive(true);
        optionsMenu.SetActive(false);
        resetProgressMenu.SetActive(false);
    }

    public void ActivateOptionsMenu()
    {
        mainMenu.SetActive(false);
        levelsMenu.SetActive(false);
        optionsMenu.SetActive(true);
        resetProgressMenu.SetActive(false);
    }

    public void ActivateResetProgressMenu()
    {
        mainMenu.SetActive(false);
        levelsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        resetProgressMenu.SetActive(true);
    }

    public void RestartProgress()
    {
        GameManager.instance.DeleteProgress();
    }

}
