using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    float flyTime = 1f;

    [SerializeField]
    AnimationCurve xCurve, yCurve, scaleCurve;

    Vector2 startPosition;
    Vector2 targetPosition;
    float startTime;

    void Start()
    {
        startPosition = new Vector2(transform.position.x, transform.position.y);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float flightProgress = (Time.time - startTime) / flyTime;

        if (flightProgress >= 1)
        {
            Explode();
            return;
        }

        float xPercent = xCurve.Evaluate(flightProgress);
        float yPercent = yCurve.Evaluate(flightProgress);
        float xPos = startPosition.x + (targetPosition.x - startPosition.x) * xPercent;
        float yPos = startPosition.y + (targetPosition.y - startPosition.y) * yPercent;
        Vector3 newPos = new Vector3(xPos, yPos, 0);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, newPos - transform.position);
        transform.position = newPos;
        transform.localScale = Vector3.one * (1-scaleCurve.Evaluate(flightProgress));
    }

    void Explode()
    {
        Destroy(gameObject);
    }

    public void SetTarget(Vector2 target)
    {
        targetPosition = target;
    }
}
