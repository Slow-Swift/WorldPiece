using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RetaliationManager : MonoBehaviour
{
    [SerializeField]
    HealthManager healthManager;

    [SerializeField]
    Missile missilePrefab;

    [SerializeField]
    Transform missileParent;

    [SerializeField]
    AnimationCurve launchRateCurve;

    [SerializeField]
    float damage;

    [SerializeField]
    float earthRadius = 4;

    [SerializeField]
    float targetXMin, targetXMax;

    [SerializeField]
    float targetYMin, targetYMax;

    float lastLaunchTime = 0;
    float launchBreak = 0;
    
    void Update()
    {
        float healthPercent = healthManager.EarthHealthPercent;
        float launchRate = launchRateCurve.Evaluate(healthPercent);

        if (launchRate <= 0) return;

        if (Time.time >= lastLaunchTime + launchBreak)
        {
            LaunchMissile();
            launchBreak = Random.Range(0.8f / launchRate, 1.5f / launchRate);
            lastLaunchTime = Time.time;
        }
    }

    void LaunchMissile()
    {
        Debug.Log("Firing Missile");
        float angle = Random.Range(0, Mathf.PI);
        float distance = Random.Range(0, earthRadius);
        Vector3 launchPos = new Vector3(distance * Mathf.Cos(angle), distance * Mathf.Sin(angle), 0);

        float targetX = Random.Range(targetXMin, targetXMax);
        float targetY = Random.Range(targetYMin, targetYMax);

        Debug.Log(new Vector2(targetX, targetY));

        if (launchPos.x < 0) targetX = -targetX;

        Missile missile = Instantiate(missilePrefab, launchPos, Quaternion.identity, missileParent);
        missile.InitializeMissile(new Vector2(targetX, targetY), damage, healthManager.DamageShip);
    }
}
