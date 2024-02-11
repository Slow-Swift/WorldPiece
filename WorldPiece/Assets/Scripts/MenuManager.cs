using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject quitButton;

    private void Start()
    {
#if UNITY_WEBGL
        quitButton.SetActive(false);
#endif
    }

    public void OnStartPressed()
    {
        SceneLoader.instance.SwitchScene("SampleScene");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
