using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    float sceneSwapStartTime = -10;

    void Awake()
    {
        instance = this;
        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
            currentScene = SceneManager.GetSceneByName("MainMenu");
        }
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
                SceneManager.UnloadSceneAsync(currentScene);
                SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
                sceneSwapStartTime = Time.time;
            }
        } else
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
}
