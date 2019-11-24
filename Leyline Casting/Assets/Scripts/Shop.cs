using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeLevels
{

    //mcs = 3, 6, 9, 12
    //line = 1, 2, 3, 4
    //maxFishOnHook = 4, 8, 12, 16
    //lureR = 1, 1.5, 2.0, 2.5
    private static Upgrade[] maxCastStrength =
    {
        new Upgrade(100, 3.0f),
        new Upgrade(200, 6.0f),
        new Upgrade(400, 9.0f),
        new Upgrade(800, 12.0f),
    };

    private static Upgrade[] lineStrength =
    {
        new Upgrade(100, 1.0f),
        new Upgrade(200, 2.0f),
        new Upgrade(400, 3.0f),
        new Upgrade(800, 4.0f),
    };

    private static Upgrade[] maxFish =
    {
        new Upgrade(100, 4.0f),
        new Upgrade(200, 8.0f),
        new Upgrade(400, 12.0f),
        new Upgrade(800, 16.0f),
    };

    private static Upgrade[] lureRadius =
    {
        new Upgrade(100, 1.0f),
        new Upgrade(200, 1.5f),
        new Upgrade(400, 2.0f),
        new Upgrade(800, 2.5f),
    };
}

public struct Upgrade
{
    public readonly int cost;
    public readonly float value;

    public Upgrade(int cost, float value)
    {
        this.cost = cost;
        this.value = value;
    }
}

public class Shop : MonoBehaviour
{
    // all stat values are abbreviated
    public int mcsIndex = 0;
    public int lsIndex = 0;
    public int mfIndex = 0;
    public int lureRadius = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool AttemptPurchase()
    {
        // TODO: gold manager check for money
        // if money is less than value
        return false;
    }
}
