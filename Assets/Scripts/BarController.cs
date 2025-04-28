using System;
using UnityEngine;

public class BarController : MonoBehaviour
{
    private float span = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        span += Time.deltaTime;
        this.transform.position -= new Vector3(1.788f * Time.deltaTime, 0, 0);
        if(span >= 10.0f)
        {
            this.transform.position = new Vector3(0, -5.0f, 0);
            span = 0;
        }
    }
}
