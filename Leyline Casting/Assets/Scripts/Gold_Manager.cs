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
            if (weight == 9.6f)
            {
                gold = gold + 30;
            }
            else if (weight >= 7.9f && weight != 9.6f)
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
            if (weight == 7.0f)
            {
                gold = gold + 17;
            }
            else if (weight >= 5.9f && weight != 7.0f)
            {
                gold = gold + 15;
            }
            else
            {
                gold = gold + 12;
            }
        }

        else if (type == FishType.MagiCarp) // range is from 1 to 6 pounds
        {
            if (weight == 6.0f)
            {
                gold = gold + 12;
            }
            else if (weight >= 4.8 && weight != 6.0f)
            {
                gold = gold + 10;
            }
            else
            {
                gold = gold + 7;
            }
        }

        else if (type == FishType.ToxicBlockhead) // range is from 2 to 12 pounds
        {
            if (weight == 12.0f)
            {
                gold = gold + 50;
            }
            else if (weight >= 9.5 && weight != 12.0f)
            {
                gold = gold + 40;
            }
            else
            {
                gold = gold + 30;
            }
        }

        else if (type == FishType.Chad) // range is from 70 to 96 pounds
        {
            if (weight == 96.0f)
            {
                gold = gold + 90;
            }
            else if (weight >= 89 && weight != 96.0f)
            {
                gold = gold + 75;
            }
            else
            {
                gold = gold + 50;
            }
        }

        return gold;
    }
}

