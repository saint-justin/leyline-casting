using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialGameMax;
    public GameObject gameTextMesh;
    public string[] gameTexts;
    public int gameTextIndex;

    public GameObject shopIcon;

    // Start is called before the first frame update
    void Start()
    {
        gameTextIndex = 0;
        gameTexts = new string[6];
        gameTexts[0] = "Tap the screen to start casting the fishing rod!";
        gameTexts[1] = "Now tap again to set the power of the cast!";
        gameTexts[2] = "Tap once more for the angle, and...";
        gameTexts[3] = "Nice work! You can tap and hold to reel in faster too!";
        gameTexts[4] = "Check out the shop by clicking the chest above me!";
        gameTexts[5] = "In the shop you can buy all sorts of upgrades to your gear.";
        
        gameTextMesh.GetComponent<TextMeshPro>().text = gameTexts[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(!shopIcon.GetComponent<LaunchShop>().IsGamePaused())
            {
                gameTextIndex++;
                if(gameTextIndex < gameTexts.Length)
                {
                    gameTextMesh.GetComponent<TextMeshPro>().text = gameTexts[gameTextIndex];
                }
                else if(gameTextIndex == gameTexts.Length)
                {
                    Destroy(tutorialGameMax);
                    Destroy(gameTextMesh);
                }
            }
        }
    }
}
