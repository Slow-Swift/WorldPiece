using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    float shipStartHealth = 200;

    [SerializeField]
    float earthStartHealth = 200;

    [SerializeField]
    Transform shipHealthbarMask, earthHealthbarMask;

    [SerializeField]
    AudioSource winSound, loseSound;

    public float ShipHealth { get; private set; }
    public float EarthHealth { get; private set; }

    public float EarthHealthPercent => EarthHealth / earthStartHealth;

    public bool Playing { get; private set; }

    void Start()
    {
        ShipHealth = shipStartHealth;
        EarthHealth = earthStartHealth;
        Playing = true;
    }

    void Update()
    {
        shipHealthbarMask.transform.localPosition = Vector3.left * (1 - (ShipHealth / shipStartHealth));
        earthHealthbarMask.transform.localPosition = Vector3.left * (1 - EarthHealthPercent);
    }

    public void DamageShip(float damage)
    {
        if (!Playing) return;

        ShipHealth -= damage;

        if (ShipHealth <= 0)
        {
            Playing = false;
            loseSound.Play();
            Invoke("switchToLoseMenu", 1f);
        }
    }

    public void DamageEarth(float damage)
    {
        if (!Playing) return;

        EarthHealth -= damage;

        if (EarthHealth <= 0)
        {
            Playing = false;
            winSound.Play();
            Invoke("switchToWinMenu", 1f);
        }
    }

    void switchToWinMenu()
    {
        SceneLoader.instance.SwitchScene("WinScene");
    }

    void switchToLoseMenu()
    {
        SceneLoader.instance.SwitchScene("LoseScene");
    }
}
