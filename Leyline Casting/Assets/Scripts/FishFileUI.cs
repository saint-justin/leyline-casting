using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishFileUI : MonoBehaviour
{
    TextMeshProUGUI fishName;
    Image fishImage;
    TextMeshProUGUI fishWeight;

    static int length = Enum.GetValues(typeof(FishType)).Length;

    public Sprite[] silhouettes = new Sprite[length];
    public Sprite[] fish = new Sprite[length];


    // Start is called before the first frame update
    void Start()
    {
        fishName = GameObject.Find("FISH_NAME").GetComponent<TextMeshProUGUI>();
        fishImage = GameObject.Find("FISH_IMG").GetComponent<Image>();
        fishImage.preserveAspect = true;
        fishWeight = GameObject.Find("FISH_WGT").GetComponent<TextMeshProUGUI>();

        ShowFishData(0); // show first fish (woo mango) data
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowFishData(int fishType)
    {
        if(!FishmongerFile.fishFile.fish[fishType].found)
        {
            fishName.text = "???";
            fishImage.sprite = silhouettes[fishType];
            fishWeight.text = "Biggest Catch: ???";
            return;
        }
        fishName.text = FishmongerFile.fishFile.fish[fishType].name;
        fishImage.sprite = fish[fishType];
        fishWeight.text = $"Biggest Catch: {FishmongerFile.fishFile.fish[fishType].weight:F1}kg";
    }
}
