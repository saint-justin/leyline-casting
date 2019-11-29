using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Fish fish = new Fish();
    }

    // Update is called once per frame
    void Update()
    {
        // Woo Mango Fish 

    }


    // returns gold value from caught fish based on the fish and it's weight 
    public int ReturnGold(FishType type, float weight)
    {
        int gold = 0;

        if (type == FishType.WooManngo) // range is from 7 to 9.6 pounds 
        {
            if (weight <=9.0f)
            {
                gold = 25;
            }
            else
            {
                gold = 20;
            }
        }

        if (type == FishType.AngleLilac) // range is from 3 to 7 pounds
        {
            if (weight <= 6.0f)
            {
                gold = 15;
            }
            else
            {
                gold = 12;
            }
        }

        if (type == FishType.MagiCarp) // range is from 1 to 6 pounds
        {
            if (weight <= 4.8)
            {
                gold = 10;
            }
            else
            {
                gold = 7;
            }
        }

        if (type == FishType.ToxicBlockhead) // range is from 2 to 12 pounds
        {
            if (weight <= 9)
            {
                gold = 40;
            }
            else
            {
                gold = 30;
            }
        }

        if (type == FishType.Chad) // range is from 70 to 96 pounds
        {
            if (weight <= 89)
            {
                gold = 75;
            }
            else
            {
                gold = 50;
            }
        }

        return gold;
    }
}
