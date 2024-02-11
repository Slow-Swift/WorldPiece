using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [HideInInspector]
    public static SceneLoader instance;

    [SerializeField]
    float sceneSwapTime = 1;

    [SerializeField]
    Image sceneFader;

    [SerializeField]
    Color sceneFaderNormalColor, sceneFaderHideColor;

    Scene currentScene;
    string nextScene;

    bool switchingScene = false;
    bool sceneLoaded = true;
    float sceneSwapStartTime = -10;

    void Awake()
    {
        instance = this;
        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
            currentScene = SceneManager.GetSceneByName("MainMenu");
        } else
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene s = SceneManager.GetSceneAt(i);
                if (s != gameObject.scene)
                {
                    currentScene = s;
                    break;
                }
            }
        }
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (switchingScene)
        {
            float swapPercent = (Time.time - sceneSwapStartTime) / sceneSwapTime;
            sceneFader.color = Color.Lerp(sceneFaderNormalColor, sceneFaderHideColor, swapPercent);
            if (swapPercent >= 1)
            {
                switchingScene = false;
                sceneLoaded = false;
                SceneManager.UnloadSceneAsync(currentScene);
                SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
                SceneManager.sceneLoaded += onSceneLoaded;
            }
        } else if (sceneLoaded)
        {
            float swapPercent = (Time.time - sceneSwapStartTime) / sceneSwapTime;
            sceneFader.color = Color.Lerp(sceneFaderHideColor, sceneFaderNormalColor, swapPercent);
        }
    }

    public void SwitchScene(string scene)
    {
        nextScene = scene;
        switchingScene = true;
        sceneSwapStartTime = Time.time;
    }

    void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == nextScene)
        {
            currentScene = scene;
            sceneLoaded = true;
            sceneSwapStartTime = Time.time + 0.2f;
        }
    }
}
