using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeLevels
{

    //mcs = 3, 6, 9, 12 - rod
    //line = 1, 2, 3, 4 - line
    //maxFishOnHook = 4, 8, 12, 16 - hook
    //lureR = 1, 1.5, 2.0, 2.5 - lure
    public enum Types
    {
        MaxCastStrength,
        LineStrength,
        MaxFish,
        LureRadius,
    }

    public static void SetUpgrade(FishingRod rod, Types type, int index)
    {
        switch (type)
        {
            case Types.MaxCastStrength:
                rod.SetMaxCastStrength(maxCastStrength[index].value);
                break;
            case Types.LineStrength:
                rod.SetLineStrength(lineStrength[index].value);
                break;
            case Types.MaxFish:
                rod.SetMaxFishOnHook(maxFish[index].value);
                break;
            case Types.LureRadius:
                rod.SetLureRadius(maxCastStrength[index].value);
                break;
        }
    }

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
    // indices match with the number in the enum
    // index 0 is mcs (rod)
    // index 1 is ls (line)
    // index 2 is mf (hook)
    // index 3 is lr (lure)
    public int[] upgradeIndices = { 0, 0, 0, 0 };

    // objects
    // public GameObject goldManagerObject;
    public GameObject fishingRodObject;

    // references
    // private Gold_Manager goldManager;
    private FishingRod fishingRod;

    // Start is called before the first frame update
    void Start()
    {
        // goldManager = goldManagerObject.GetComponent<Gold_Manager>();
        fishingRod = fishingRodObject.GetComponent<FishingRod>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Attempts to purchase upgrade of fishing rod part if player has enough money
    /// </summary>
    /// <param name="typeOfUpgrade">Part to upgrade</param>
    public void AttemptPurchase(UpgradeLevels.Types typeOfUpgrade)
    {
        // TODO: player/gold manager check for money
        // if money is less than value
        //if(false)
        //    return false;
        Debug.Log(typeOfUpgrade);

        // set upgradevalue
        UpgradeLevels.SetUpgrade(
            fishingRod, // rod reference
            typeOfUpgrade, // type of upgrade from enum in UpgradeLevels (written above this class)
            upgradeIndices[(int)typeOfUpgrade] // index of current upgrade
            );

        // return true;
    }

    // same as above function, works with integral representation of enum instead
    public void AttemptPurchase(int typeOfUpgrade)
    {
        AttemptPurchase((UpgradeLevels.Types)typeOfUpgrade);
    }
}
