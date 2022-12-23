using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZHJDtest : MonoBehaviour
{

    void Start()
    {
    }
    float rx;
    float ry;
    void Update()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rx -= my * Time.deltaTime;
        ry += mx * Time.deltaTime;

        rx = Clamp(rx, -75, 75);

        transform.eulerAngles = new Vector3(rx, ry, 0);

    }

    float Clamp(float value, float min, float max)
    {
        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }
}
