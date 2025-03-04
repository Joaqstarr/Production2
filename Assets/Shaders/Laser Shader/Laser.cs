using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEffect : MonoBehaviour
{
    private LineRenderer lr;
    private float pulseSpeed = 2f;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        float width = Mathf.PingPong(Time.time * pulseSpeed, 0.2f) + 0.1f;
        lr.startWidth = width;
        lr.endWidth = width;
    }
}
