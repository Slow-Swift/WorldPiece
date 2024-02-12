using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    [SerializeField]
    LayerMask earthLayer;

    [SerializeField]
    KeycodeManager keycodeManager;

    [SerializeField]
    Transform leftLaunchPos, rightLaunchPos;

    [SerializeField]
    Missile missilePrefab;

    [SerializeField]
    Transform missileParent;

    [SerializeField]
    HealthManager healthManager;

    [SerializeField]
    randomWeakPoint point;

    [SerializeField]
    float weakpointDamage;

    [SerializeField]
    float baseDamage;

    [SerializeField]
    float maxSqrDistance;

    [SerializeField]
    ShieldManager shieldManager;

    // Update is called once per frame
    void Update()
    {
        if (!keycodeManager.Loaded) return;

        if (Input.GetMouseButtonDown(0))
        {
            HandleEarthClicks();
        }
    }

    void HandleEarthClicks()
    {
        Vector3 worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector3(worldMouse.x, worldMouse.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 1f, earthLayer);
        if (hit.collider != null)
            LaunchMissile(mousePos2D);
    }

    void LaunchMissile(Vector2 target)
    {
        float distance = Vector2.SqrMagnitude(target - point.getWeakPoint());
        float damage = Mathf.Max(weakpointDamage * (1 - distance / maxSqrDistance), baseDamage);
        print(damage);

        Transform launchPos = target.x < 0 ? leftLaunchPos : rightLaunchPos;
        Missile missile = Instantiate(missilePrefab, launchPos.position, launchPos.rotation, missileParent);
        missile.InitializeMissile(target, damage, healthManager.DamageEarth);

        keycodeManager.Unload();
    }
}
