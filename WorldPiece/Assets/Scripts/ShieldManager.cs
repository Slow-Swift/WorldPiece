using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    [SerializeField]
    KeycodeManager keycodeManager;

    [SerializeField]
    GameObject shield;

    [SerializeField]
    float shieldLength = 2;

    public bool ShieldActive { get; private set; }

    float shieldActivatedTime = 0;

    private void Start()
    {
        shield.SetActive(false);
        ShieldActive = false;
    }

    private void Update()
    {
        if (ShieldActive)
        {
            if (Time.time > shieldActivatedTime + shieldLength) {
                shield.SetActive(false);
                ShieldActive = false;
            }
        }

        if (!keycodeManager.Loaded) return;

        if (Input.GetKeyDown(KeyCode.Space)) {
            keycodeManager.Unload();
            shield.SetActive(true);
            ShieldActive=true;
            shieldActivatedTime = Time.time;
        }
    }
}
