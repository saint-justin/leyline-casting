using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                rod.SetLureRadius(lureRadius[index].value);
                break;
        }
    }

    public static Upgrade GetUpgrade(Types type, int index)
    {
        switch (type)
        {
            case Types.MaxCastStrength:
            default:
                return maxCastStrength[index];
            case Types.LineStrength:
                return lineStrength[index];
            case Types.MaxFish:
                return maxFish[index];
            case Types.LureRadius:
                return maxCastStrength[index];
        }
    }

    public static int GetUpgradesLength(Types type)
    {
        switch (type)
        {
            case Types.MaxCastStrength:
            default:
                return maxCastStrength.Length;
            case Types.LineStrength:
                return lineStrength.Length;
            case Types.MaxFish:
                return maxFish.Length;
            case Types.LureRadius:
                return maxCastStrength.Length;
        }
    }

    private static Upgrade[] maxCastStrength =
    {
        new Upgrade(100, 12.0f),
        new Upgrade(200, 15.0f),
        new Upgrade(400, 18.0f),
        new Upgrade(800, 21.0f),
    };

    private static Upgrade[] lineStrength =
    {
        new Upgrade(100, 150.0f),
        new Upgrade(200, 300.0f),
        new Upgrade(400, 500.0f),
        new Upgrade(800, 1000.0f),
    };

    private static Upgrade[] maxFish =
    {
        new Upgrade(100, 8.0f),
        new Upgrade(200, 12.0f),
        new Upgrade(400, 16.0f),
        new Upgrade(800, 20.0f),
    };

    private static Upgrade[] lureRadius =
    {
        new Upgrade(100, 4.0f),
        new Upgrade(200, 5.0f),
        new Upgrade(400, 6.0f),
        new Upgrade(800, 8.0f),
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

    // objects for updating amounts
    public GameObject goldManagerObject;
    public GameObject fishingRodObject;

    // references
    private Gold_Manager goldManager;
    private FishingRod fishingRod;

    // game object costs
    public GameObject rodButton;
    public GameObject lineButton;
    public GameObject hookButton;
    public GameObject lureButton;

    // text component references
    private Text goldAmt;
    private Button[] upgradeButtons;
    private Text[] upgradeCosts;

    // Start is called before the first frame update
    void Start()
    {
        goldManager = goldManagerObject.GetComponent<Gold_Manager>();
        fishingRod = fishingRodObject.GetComponent<FishingRod>();

        goldAmt = GameObject.Find("Current Gold").GetComponent<Text>();

        upgradeButtons = new Button[]
        {
            rodButton.GetComponent<Button>(),
            lineButton.GetComponent<Button>(),
            hookButton.GetComponent<Button>(),
            lureButton.GetComponent<Button>()
        };

        upgradeCosts = new Text[]
        {
            rodButton.transform.Find("cost").GetComponent<Text>(),
            lineButton.transform.Find("cost").GetComponent<Text>(),
            hookButton.transform.Find("cost").GetComponent<Text>(),
            lureButton.transform.Find("cost").GetComponent<Text>()
        };

        for(int i = 0; i < upgradeCosts.Length; i++)
        {
            upgradeCosts[i].text = 
                $"${UpgradeLevels.GetUpgrade((UpgradeLevels.Types)i, upgradeIndices[i]).cost}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // might be updating this more than it needs to by having this in update
        goldAmt.text = $"Gold: ${goldManager.gold}";

        for(int i = 0; i < upgradeIndices.Length; i++)
        {
            // check if there are anymore upgrades left
            // might want to move some of this into SetUpgrade if 
            // # of upgrades per obj becomes variable
            if (upgradeIndices[i] >= UpgradeLevels.GetUpgradesLength((UpgradeLevels.Types)i))
                upgradeButtons[i].interactable = false;
            // player/gold manager check if there's enough money for the next upgrade
            // if money is less than value, do nothing
            else if (goldManager.gold < UpgradeLevels.GetUpgrade((UpgradeLevels.Types)i, upgradeIndices[i]).cost)
            {
                upgradeButtons[i].interactable = false;
                upgradeCosts[i].color = Color.red;
            }
            else
            {
                upgradeButtons[i].interactable = true;
                upgradeCosts[i].color = Color.white;
            }
        }
    }

    /// <summary>
    /// Attempts to purchase upgrade of fishing rod part if player has enough money
    /// </summary>
    /// <param name="typeOfUpgrade">Part to upgrade</param>
    public void AttemptPurchase(UpgradeLevels.Types typeOfUpgrade)
    {
        // check if there are anymore upgrades left
        // might want to move some of this into SetUpgrade if 
        // # of upgrades per obj becomes variable
        if (upgradeIndices[(int)typeOfUpgrade] >= UpgradeLevels.GetUpgradesLength(typeOfUpgrade))
            return; // if no future upgrade exists don't do anything... no upgrade is left


        // player/gold manager check if there's enough money for the next upgrade
        // if money is less than value, do nothing
        if (goldManager.gold < UpgradeLevels.GetUpgrade(typeOfUpgrade, upgradeIndices[(int)typeOfUpgrade]).cost)
            return;

        Debug.Log(typeOfUpgrade);


        // subtract the cost of the upgrade from the player's current gold tally
        goldManager.SubtractGold(UpgradeLevels.GetUpgrade(typeOfUpgrade, upgradeIndices[(int)typeOfUpgrade]).cost);

        // set upgradevalue
        UpgradeLevels.SetUpgrade(
            fishingRod, // rod reference
            typeOfUpgrade, // type of upgrade from enum in UpgradeLevels (written above this class)
            upgradeIndices[(int)typeOfUpgrade] // index of current upgrade
            );

        // increase index of upgrade
        upgradeIndices[(int)typeOfUpgrade]++;

        // set cost text
        if(upgradeIndices[(int)typeOfUpgrade] < UpgradeLevels.GetUpgradesLength(typeOfUpgrade))
            upgradeCosts[(int)typeOfUpgrade].text =
                $"${UpgradeLevels.GetUpgrade(typeOfUpgrade, upgradeIndices[(int)typeOfUpgrade]).cost}";
        else
            upgradeCosts[(int)typeOfUpgrade].text = "$---";
    }

    // same as above function, works with integral representation of enum instead
    public void AttemptPurchase(int typeOfUpgrade)
    {
        AttemptPurchase((UpgradeLevels.Types)typeOfUpgrade);
    }
}
