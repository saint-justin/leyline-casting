using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shiftBg : MonoBehaviour
{
    float startTime;
    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        Vector3 pos = this.gameObject.transform.position;
        pos.x += Mathf.Sin(currentTime) * 0.01f;
        this.gameObject.transform.position = pos;
    }
}
