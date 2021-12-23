using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioMixerGroup audioMixerGroup;
    public bool debugMode = false;
    public int playerProgress = 0;
    int previousScene = 0;
    public GameObject loadingScreen;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene");
            return;
        }
        instance = this;
        playerProgress = PlayerPrefs.GetInt("PlayerProgress", playerProgress);

        if (!debugMode)
        {
            FirstGameLoad();
        }
    }
    #endregion

    public void FirstGameLoad()
    {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive));
        StartCoroutine(GetSceneLoadProgress((int)SceneIndexes.MAIN_MENU));
    }

    public IEnumerator GetSceneLoadProgress(int newScene)
    {

        previousScene = GetCurrentScene();

        float loadingDuration = 0f;
        foreach (var scene in scenesLoading)
        {
            while (!scene.isDone)
            {
                loadingDuration += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        scenesLoading.Clear();
        Time.timeScale = 1f;
        loadingScreen.SetActive(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(newScene));
    }


    public int GetCurrentScene()
    {
        int currentScene = 0;
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            if (SceneManager.GetSceneAt(i).buildIndex > currentScene)
                currentScene = SceneManager.GetSceneAt(i).buildIndex;
        }

        return currentScene;
    }


    public void LoadLevel(int levelId)
    {
        loadingScreen.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexes.MAIN_MENU));
        scenesLoading.Add(SceneManager.LoadSceneAsync(levelId, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress(levelId));
    }

    public void RestartLevel()
    {
        int currentScene = GetCurrentScene();

        loadingScreen.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress(currentScene));

        //StartCoroutine(ReloadScene());
    }


    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            { 
                yield return null;
            }
        }
        //loadingScreen.SetActive(false);
    }

    public void LoadNextScene()
    {
        int currentScene = GetCurrentScene();

        if (currentScene + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("NO NEXT LEVEL AVAILABLE");
        }
        else
        {
            scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
            scenesLoading.Add(SceneManager.LoadSceneAsync(currentScene + 1, LoadSceneMode.Additive));

            StartCoroutine(GetSceneLoadProgress(currentScene + 1));
        }
    }

    public bool IsThisTheLastLevel()
    {
        int currentScene = GetCurrentScene();
        return (currentScene + 1 >= SceneManager.sceneCountInBuildSettings);
    }

    public void GoToMainMenu()
    {
        int currentScene = GetCurrentScene();

        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(currentScene));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.MAIN_MENU, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress((int)SceneIndexes.MAIN_MENU));
    }

    public void GameRestart()
    {
        loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexes.PERSISTENT_SCENE, LoadSceneMode.Single));

        StartCoroutine(GetSceneLoadProgress((int)SceneIndexes.PERSISTENT_SCENE));
    }

    public void SaveProgress()
    {
        int completedLevelId = GetCurrentScene() - 1;
        if (completedLevelId > playerProgress)
        {
            playerProgress = completedLevelId;
            PlayerPrefs.SetInt("PlayerProgress", playerProgress);
        }
    }

    public void DeleteProgress()
    {
        PlayerPrefs.DeleteKey("PlayerProgress");
        playerProgress = 0;

        GameRestart();
    }

}
