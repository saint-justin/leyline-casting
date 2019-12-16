using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchShop : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject GuiGO;
    private ShopGuiManager GuiManager;

    void Start()
    {
        GuiManager = GuiGO.GetComponent<ShopGuiManager>();
    }

    public void ClickEvent()
    {
        if (GamePaused)
            Resume();
        else
            Pause();
    }

    /// <summary>
    ///  Pauses the game and launches pause menu
    /// </summary>
    void Pause()
    {
        GamePaused = !GamePaused;
        GuiManager.ToggleDisplay();
        Time.timeScale = 0.0f;
    }

    /// <summary>
    ///  Resumes the game and hides pause menu
    /// </summary>
    void Resume()
    {
        GamePaused = !GamePaused;
        GuiManager.ToggleDisplay();
        Time.timeScale = 1.0f;
    }
}
