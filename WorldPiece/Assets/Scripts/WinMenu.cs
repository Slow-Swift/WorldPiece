using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public void PlayAgainPressed()
    {
        SceneLoader.instance.SwitchScene("SampleScene");
    }

    public void MainMenuPressed()
    {
        SceneLoader.instance.SwitchScene("MainMenu");
    }
}
