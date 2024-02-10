using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthHealth : MonoBehaviour
{
    [SerializeField]
    float startHealth = 200;

    [SerializeField]
    Transform healthbarMask;

    float currentHealth;

    void Start()
    {
        currentHealth = startHealth;
    }

    void Update()
    {
        healthbarMask.transform.localPosition = Vector3.left * (1 - (currentHealth / startHealth));
    }

    public void DealDamage(float damage)
    {
        Debug.Log(damage);
        currentHealth -= damage;
    }
}
