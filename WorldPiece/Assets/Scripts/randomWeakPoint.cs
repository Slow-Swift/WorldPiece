using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class randomWeakPoint : MonoBehaviour
{
    [SerializeField]
    GameObject point;

    [SerializeField]
    float waitTime = 1.0f;

    Vector3 pos = Vector3.zero;
    private void Start()
    {
        createWeakPoint();
    }

    void createWeakPoint()
    {

        Invoke("resetPos", waitTime);
    }

    void resetPos()
    {

        pos = Random.insideUnitCircle * 3;
        point.transform.position = pos;
        Invoke("resetPos", waitTime);

    }

    public Vector2 getWeakPoint()
    {
        return point.transform.position;
    }
}

