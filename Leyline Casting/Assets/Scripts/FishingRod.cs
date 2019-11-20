using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishingState { Inactive, Casting, Rising };

public class FishingRod : MonoBehaviour
{
    public GameObject fishingHook; // Reference to the fishing rod's hook
    public LineDrawer fishingLine; // Draws the fishing rod line
    public Vector2 hookInactivePosition; // Reference to where the hook is when not casting
    public Vector2 hookPosition; // Current position of the hook
    private Vector2 hookPrevPosition; // Reference to the previous position of the hook for collision detection
    public Vector2 hookVelocity; // Current velocity of the hook

    public float maxCastStrength; // Speed multiplier for the initial velocity of casting
    public float lineStrength; // Max weight that the line can support
    public float hookRadius; // Distance from the hook that fish will attach to the line

    // Variables for casting the hook
    public FishingState finiteState; // Finite state machine
    private float castStrength; // Initial speed of the fishing rood cast
    private Vector2 castAngle; // Angle that the fishing rod should be cast at
    private float castTime; // Time counter used for calculating the exact location to position the hook over time

    public List<GameObject> oceanFish; // List of all fish in the sea that aren't on the fishing line
    private List<GameObject> hookedFish; // List of all fish attached to the hook

    // Start is called before the first frame update
    void Start()
    {
        fishingLine = new LineDrawer(0.075f);

        hookInactivePosition = fishingHook.transform.position;
        hookPosition = hookInactivePosition;
        hookPrevPosition = hookPosition;
        hookVelocity = new Vector2(0.0f, 0.0f);

        maxCastStrength = 3.0f;
        lineStrength = 1.0f;
        hookRadius = 2.0f;

        finiteState = FishingState.Inactive;
        castStrength = 0.0f;
        castAngle = new Vector2(0.0f, -1.0f);
        castTime = 0.0f;

        hookedFish = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        hookPrevPosition = hookPosition;

        switch (finiteState)
        {
            case FishingState.Inactive:
                // Finite state handled in CastLine(...)
                break;
            case FishingState.Casting:
                if (hookVelocity.y >= 0.0f)
                {
                    hookVelocity = new Vector2(0.0f, 2.0f);
                    finiteState = FishingState.Rising;
                }
                else
                {
                    if(hookPosition.y > transform.position.y - 2.0f)
                    {
                        hookVelocity += new Vector2(0.0f, -4.0f) * Time.deltaTime;
                    }

                    hookPosition += hookVelocity * Time.deltaTime;
                    hookVelocity -= hookVelocity.normalized * 3.0f * Time.deltaTime; // Reduces velocity by 1.0 per second
                }
                break;
            case FishingState.Rising:
                if (hookPosition.y > transform.position.y - 1.0f)
                {
                    foreach(GameObject fish in hookedFish)
                    {
                        Destroy(fish);
                    }
                    hookedFish.Clear();

                    hookPosition = hookInactivePosition;
                    finiteState = FishingState.Inactive;
                }
                else
                {
                    hookPosition += hookVelocity * Time.deltaTime;
                    HandleFishCollision();
                    for(int i = 0; i < hookedFish.Count; i++)
                    {
                        hookedFish[i].transform.position = hookPosition + new Vector2(0.0f, -0.2f);
                    }
                }
                break;
        }
        fishingHook.transform.position = hookPosition;
        DrawFishingLine();
    }

    /// <summary>
    /// Casts the fishing rod down into the water
    /// </summary>
    /// <param name="castStrength">Multiplier of the casting power, from 0.0 to 1.0</param>
    /// <param name="castAngle">Angle in degrees that the hook should be cast, from 0.0 (left) to 180.0 (right)</param>
    public void CastLine(float castStrength, float castAngle)
    {
        this.castAngle = new Vector2(
            Mathf.Cos(Mathf.Deg2Rad * (180.0f + castAngle)),
            Mathf.Sin(Mathf.Deg2Rad * (180.0f + castAngle))
            );
        hookVelocity = this.castAngle * castStrength * maxCastStrength;
        hookPosition = hookInactivePosition;
        finiteState = FishingState.Casting;
    }

    /// <summary>
    /// Draws the line from the end of the fishing rod to the top of the fishing hook
    /// TODO: Replace with more complex code drawing a texture
    /// </summary>
    private void DrawFishingLine()
    {
        fishingLine.DrawLineInGameView(transform.position, hookPosition, Color.white);
    }

    /// <summary>
    /// Checks all unhooked fish to see if it's colliding with the fishing rod; if so, add it to the fishing rod list
    /// </summary>
    private void HandleFishCollision()
    {
        foreach (GameObject fish in oceanFish)
        {
            if(Vector2.Distance(fish.transform.position, new Vector2(hookPosition.x, hookPosition.y)) <= hookRadius)
            {
                hookedFish.Add(fish);
                oceanFish.Remove(fish);
            }
        }
    }
}
