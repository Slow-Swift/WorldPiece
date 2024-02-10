using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class randomWeakPoint : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    
    public float waitTime = 6.0f;
    public float highlightTime = 3.0f;
    public Material highlight;
    public Material defaultMaterial;
    private int curIndex = 0;
    private void Start()
    {
        createWeakPoint();
    }
    
    
    void createWeakPoint()
    {
        int weakpoint = Random.Range(0, points.Length);
        curIndex = weakpoint;
        highlightPoint(points[weakpoint]);
        
    }

    void highlightPoint(GameObject point)
    {
        SpriteRenderer sr = point.GetComponent<SpriteRenderer>();
        sr.material = highlight;
        Invoke("unHighlightPoint", highlightTime);
        
        
    }

  
    void unHighlightPoint()
    {
       
        SpriteRenderer sr = points[curIndex].GetComponent<SpriteRenderer>();
        sr.material = defaultMaterial;
        Invoke("createWeakPoint", waitTime);
    }

    public bool isHighlighted(GameObject point)
    {
        int index = System.Array.IndexOf(points, point);
        return index == curIndex;
    }
}

