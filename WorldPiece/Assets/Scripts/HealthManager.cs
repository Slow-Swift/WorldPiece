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

    public float ShipHealth { get; private set; }
    public float EarthHealth { get; private set; }

    public float EarthHealthPercent => EarthHealth / earthStartHealth;

    void Start()
    {
        ShipHealth = shipStartHealth;
        EarthHealth = earthStartHealth;
    }

    void Update()
    {
        shipHealthbarMask.transform.localPosition = Vector3.left * (1 - (ShipHealth / shipStartHealth));
        earthHealthbarMask.transform.localPosition = Vector3.left * (1 - EarthHealthPercent);
    }

    public void DamageShip(float damage)
    {
        ShipHealth -= damage;
    }

    public void DamageEarth(float damage)
    {
        EarthHealth -= damage;
    }
}
