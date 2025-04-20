using System;
using UnityEngine;

public class SinewaveScaler : MonoBehaviour
{
    public float speed = 8;
    public float newScale = 0.003f;
    private Vector3 newScaleVector;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newScaleVector = new Vector3(newScale, newScale, newScale);
    }

    // Update is called once per frame
    void Update()
    {
       float sineFactor = Mathf.Sin(Time.time* speed);
       transform.localScale += newScaleVector * sineFactor;
    }
}
