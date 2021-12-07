using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{

    public GameObject[] levels;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateActiveQuests();
    }

    public void UpdateActiveQuests()
    {
        int playerProgress = GameManager.instance.playerProgress;

        DeactivateAllQuests();

        for (int i = 0; i < levels.Length; i++)
        {
            if (i <= playerProgress)
                levels[i].GetComponent<Button>().interactable = true;
            else
                levels[i].GetComponent<Button>().interactable = false;
        }
    }

    void DeactivateAllQuests()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].GetComponent<Button>().interactable = false;
        }
    }


    private SceneIndexComponent selectedLevel;
    public void SetSelectedLevel(SceneIndexComponent sceneIndexComponent)
    {
        selectedLevel = sceneIndexComponent;
    }

    public void LoadScene()
    {
        if (selectedLevel != null)
        {
            GameManager.instance.LoadLevel((int)selectedLevel.sceneIndex);
        }
    }


}
