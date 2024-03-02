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

    [SerializeField]
    AudioSource warningSound;

    float lastLaunchTime = 0;
    
    void Update()
    {
        float healthPercent = healthManager.EarthHealthPercent;
        float launchRate = launchRateCurve.Evaluate(healthPercent);

        if (healthPercent >= 0.98f) return;
        if (Time.time >= lastLaunchTime + launchRate)
        {
            LaunchMissile();
            lastLaunchTime = Time.time;
        }
    }

    void LaunchMissile()
    {
        warningSound.Play();
        Invoke("stopSound", 2);
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

    void stopSound()
    {
        warningSound.Stop();
    }
}
