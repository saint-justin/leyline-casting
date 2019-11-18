using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    // Getting refs to external items
    public GameObject angle;
    public GameObject powerBar;

    private bool increasingPower = false;       // Used to denote if the power bar is moving up or down
    private float powerMod = 1.0f;              // Used with increasingPower to move the power indicator up/down
    private float currentPower = 0.0f;
    private float maxPower = 2.0f;
    private float powerPercentage = -1.0f;

    private enum fishingState
    {
        powering,
        angling,
        none
    }
    private fishingState currentState = fishingState.none;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        { 
            case fishingState.powering:
                if (currentPower >= maxPower)
                    powerMod = -1.0f;

                else if (currentPower <= 0.0f)
                    powerMod = 1.0f;

                currentPower += Time.deltaTime * powerMod;

                UpdatePowerBarPos();
                break;

            case fishingState.angling:
                break;
            case fishingState.none:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Function to call each frame the power button is held down
    /// </summary>
    void UpdatePowerBarPos()
    {
        
    }

    /// <summary>
    /// Fxn called when the player releases the pressed button
    /// </summary>
    void FullyPowered()
    {

    }
}
