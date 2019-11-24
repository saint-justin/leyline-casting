using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Fish : MonoBehaviour
{
    public float weight;
    public FishType type;

    public Fish(float weight = 0.0f, FishType type = FishType.WooManngo)
    {
        this.weight = weight;
        this.type = type;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
