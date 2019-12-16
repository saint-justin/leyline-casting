using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_Manager : MonoBehaviour
{
    // this is the player’s overall gold 
    public int gold;

    // Start is called before the first frame update
    void Start()
    {
        Fish fish = new Fish();
        gold = 0;
    }

    // Update is called once per frame
    void Update()
    {


    }

    // adds the amount of value to the public variable gold
    public int AddGold(int value)
    {
        gold = gold + value;
        return gold;
    }

    // subtracts the amount of value from the public variable gold 
    public int SubtractGold(int value)
    {
        gold = gold - value;
        return gold;
    }


    // returns gold value from caught fish based on the fish and it's weight 
    public int ReturnGoldByWeight(FishType type, float weight)
    {
        if (type == FishType.WooMango) // range is from 7 to 9.6 pounds 
        {
            if (weight >= 9.0f)
            {
                gold = gold + 30;
            }
            else if (weight >= 7.9f)
            {
                gold = gold + 25;
            }
            else
            {
                gold = gold + 20;
            }
        }

        else if (type == FishType.AngleLilac) // range is from 3 to 7 pounds
        {
            if (weight >= 6.0f)
            {
                gold = gold + 17;
            }
            else if (weight >= 5.0f)
            {
                gold = gold + 15;
            }
            else
            {
                gold = gold + 12;
            }
        }

        else if (type == FishType.PinkFish) // range is from 8 to 11 pounds
        {
            if (weight >= 10.0f)
            {
                gold = gold + 35;
            }
            else if (weight >= 9.0f)
            {
                gold = gold + 20;
            }
            else
            {
                gold = gold + 22;
            }
        }

        else if (type == FishType.MagiCarp) // range is from 1 to 6 pounds
        {
            if (weight >= 5.0f)
            {
                gold = gold + 20;
            }
            else if (weight >= 3.8)
            {
                gold = gold + 15;
            }
            else
            {
                gold = gold + 12;
            }
        }

        else if (type == FishType.ToxicBlockhead) // range is from 2 to 12 pounds
        {
            if (weight >= 11.0f)
            {
                gold = gold + 80;
            }
            else if (weight >= 7.5f)
            {
                gold = gold + 65;
            }
            else
            {
                gold = gold + 50;
            }
        }

        else if (type == FishType.Chad) // range is from 70 to 96 pounds
        {
            if (weight >= 90.0f)
            {
                gold = gold + 120;
            }
            else if (weight >= 85.0f)
            {
                gold = gold + 90;
            }
            else
            {
                gold = gold + 70;
            }
        }
        else if (type == FishType.DogFish)
        {
            gold = gold + 500; 
        }
        else if(type == FishType.TreasureChest)
        {
            gold = gold + 500;
        }

        return gold;
    }
}

