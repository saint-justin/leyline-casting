using UnityEngine;

public class CastingUI_Manager : MonoBehaviour
{
    // Getting refs to external items
    public GameObject angleBarParent;
    private GameObject angleBarIndicator;
    public GameObject powerBarParent;
    private GameObject powerBarIndicator;
    public FishingRod fishingRod;
    public LaunchShop shopState;

    // Power and power indicator info
    private bool increasingPower = false;       // Used to denote if the power bar is moving up or down
    private float powerMod = 1.0f;              // Used with increasingPower to move the power indicator up/down
    private float currentPower = 0.0f;
    private float maxPower = 2.0f;
    private float powerPercentage = -1.0f;      // If this returns -1 something's bust

    private Vector3 powerBarMax;    // Two points for the power bar indicator to move back and forth between
    private Vector3 powerBarMin;

    // Angle and angle indicator info
    private float angleMod = -1;     // Denotes the rotation direction (cw vs. ccw) of the angle indicator
    private float currentAngle = 90.0f;
    private float maxAngle = 180.0f;
    private float minAngle = 0.0f;

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
        fishingRod = GameObject.Find("fishing_pole").GetComponent<FishingRod>();
        shopState = GameObject.Find("Image").GetComponent<LaunchShop>();
        powerBarIndicator = powerBarParent.transform.GetChild(0).gameObject;
        angleBarIndicator = angleBarParent.transform.GetChild(0).gameObject;

        float heightMax = powerBarParent.GetComponent<SpriteRenderer>().bounds.max.y;
        float heightMin = powerBarParent.GetComponent<SpriteRenderer>().bounds.min.y;

        powerBarMax = new Vector3(powerBarIndicator.transform.position.x, heightMax, powerBarIndicator.transform.position.z);
        powerBarMin = new Vector3(powerBarIndicator.transform.position.x, heightMin, powerBarIndicator.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        // State machine for casting and casting UI
        switch (currentState)
        { 
            case fishingState.powering:
                // This section updates the position of the indicator on the screen and underlying values
                if (currentPower >= maxPower)
                    powerMod = -1.0f;
                else if (currentPower <= 0.0f)
                    powerMod = 1.0f;

                currentPower += Time.deltaTime * powerMod;
                UpdatePowerBarPos();

                // Checks for the end to powering
                if (Input.GetMouseButtonDown(0) && shopState.IsGamePaused() == false)
                {
                    currentState = fishingState.angling;

                    // Disable power bar visuals & enable angle bar visuals
                    powerBarParent.GetComponent<SpriteRenderer>().enabled = false;
                    powerBarIndicator.GetComponent<SpriteRenderer>().enabled = false;

                    angleBarParent.GetComponent<SpriteRenderer>().enabled = true;
                    angleBarIndicator.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    powerPercentage = currentPower / maxPower;
                }
                break;

            case fishingState.angling:
                if (currentAngle >= maxAngle)
                {
                    angleMod = -1.0f; 
                }
                else if (currentAngle <= minAngle)
                {
                    angleMod = 1.0f; 
                }

                currentAngle += Time.deltaTime * 90.0f * angleMod;      // Increase the float in the middle here to increase speed
                //Debug.Log(currentAngle);
                

                angleBarIndicator.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(-90.0f, 90.0f, currentAngle / 180.0f));
                // Debug.Log(angleBarIndicator.transform.rotation.z);

                if (Input.GetMouseButtonDown(0) && shopState.IsGamePaused() == false)
                {
                    // The angle and power are finalized
                    angleBarParent.GetComponent<SpriteRenderer>().enabled = false;
                    angleBarIndicator.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    fishingRod.CastLine(1.0f - currentPower, currentAngle);
                    currentState = fishingState.none;
                }

                break;

            case fishingState.none:
                // Checks if we need to start powering state
                if(Input.GetMouseButtonDown(0) && fishingRod.finiteState == FishingState.Inactive)
                {
                    currentState = fishingState.powering;
                    powerBarParent.GetComponent<SpriteRenderer>().enabled = true;
                    powerBarIndicator.GetComponent<SpriteRenderer>().enabled = true;
                }
                // Reels in the fishing rod faster if the mouse is clicked
                else if(fishingRod.finiteState == FishingState.Rising && Input.GetMouseButton(0))
                {
                    fishingRod.HandleMouseDown();
                }
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
        powerBarIndicator.transform.position = Vector3.Lerp(powerBarMin, powerBarMax, currentPower / maxPower);
    }

    /// <summary>
    /// Fxn called when the player releases the pressed button
    /// </summary>
    void FullyPowered()
    {

    }
}
