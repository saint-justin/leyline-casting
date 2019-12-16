using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiftBg : MonoBehaviour
{
    float startTime;
    float currentTime = 0;
    Vector3 startLocation;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        startLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        transform.position = new Vector3(startLocation.x + Mathf.Sin(currentTime) * 1.00f, transform.position.y, startLocation.z);
    }
}
