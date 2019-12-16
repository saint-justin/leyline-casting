using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementPattern { LeftRight, CircleCW, CircleCCW, Wander, Hooked, LineBreak, Idle };

public class FishMovement : MonoBehaviour
{
    public MovementPattern movementPattern = MovementPattern.LeftRight;

    Fish fishForGold = new Fish();
    public Gold_Manager goldManager;
    public FishingRod fishingRod;

    // Variables for moving
    public Vector2 fishPosition;
    public Vector2 direction;
    public float fishSpeed = 2.0f;
    public float fishWeight = 0.0f;
    public List<Vector2> referencePoints;
    public bool chasingHook;
    //public int gold;


    // Variables used for various movement patterns
    private int patternInt = 0;
    private Vector2 currentObjective;
    private float patternTimer = 3.0f;
    private float currentSpeed;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        fishPosition = transform.position;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        fishingRod = GameObject.Find("fishing_pole").GetComponent<FishingRod>();

        fishSpeed = Random.Range(1.5f, 2.5f);

        // determines the random weight of the given fish
        //fishForGold = gameObject.GetComponent<Fish>();
        fishWeight = RandomWeight(fishForGold.type);
        // determines the gold of this fish
        goldManager = GameObject.Find("MrManager").GetComponent<Gold_Manager>();
        //gold = goldManager.ReturnGoldByWeight(fishForGold.type, fishWeight);


        // Set the reference points depending on the fish's movement pattern
        referencePoints = new List<Vector2>();
        switch(movementPattern)
        {
            case MovementPattern.LeftRight: 
                // Gets a reference to the left and right positions of the screen
                if(referencePoints.Count == 0)
                {
                    referencePoints.Insert(0, fishPosition + new Vector2(-8.0f, 0));
                    referencePoints.Insert(1, fishPosition + new Vector2(8.0f, 0));
                }
                // Start with a random position on the pattern
                patternInt = Random.Range(0, referencePoints.Count);
                fishPosition = Vector2.Lerp(referencePoints[patternInt], referencePoints[(patternInt + 1) % referencePoints.Count], Random.Range(0.0f, 1.0f));
                UpdateTransformPosition();
                break;
            case MovementPattern.CircleCW:
                // Generates a clockwise circle of reference points around the fish's current position
                if (referencePoints.Count == 0)
                {
                    float referencePointRadius = Random.Range(0.8f, 1.2f); // Must be a radius of at least 0.2f
                    referencePoints.Insert(0,  fishPosition + referencePointRadius * new Vector2(0.000f,  1.000f));
                    referencePoints.Insert(1,  fishPosition + referencePointRadius * new Vector2(0.259f,  0.969f));
                    referencePoints.Insert(2,  fishPosition + referencePointRadius * new Vector2(0.500f,  0.866f));
                    referencePoints.Insert(3,  fishPosition + referencePointRadius * new Vector2(0.707f,  0.707f));
                    referencePoints.Insert(4,  fishPosition + referencePointRadius * new Vector2(0.866f,  0.500f));
                    referencePoints.Insert(5,  fishPosition + referencePointRadius * new Vector2(0.969f,  0.259f));
                    referencePoints.Insert(6,  fishPosition + referencePointRadius * new Vector2(1.000f,  0.000f));
                    referencePoints.Insert(7,  fishPosition + referencePointRadius * new Vector2(0.969f,  -0.259f));
                    referencePoints.Insert(8,  fishPosition + referencePointRadius * new Vector2(0.866f,  -0.500f));
                    referencePoints.Insert(9,  fishPosition + referencePointRadius * new Vector2(0.707f,  -0.707f));
                    referencePoints.Insert(10, fishPosition + referencePointRadius * new Vector2(0.500f,  -0.866f));
                    referencePoints.Insert(11, fishPosition + referencePointRadius * new Vector2(0.259f,  -0.969f));
                    referencePoints.Insert(12, fishPosition + referencePointRadius * new Vector2(0.000f,  -1.000f));
                    referencePoints.Insert(13, fishPosition + referencePointRadius * new Vector2(-0.259f, -0.969f));
                    referencePoints.Insert(14, fishPosition + referencePointRadius * new Vector2(-0.500f, -0.866f));
                    referencePoints.Insert(15, fishPosition + referencePointRadius * new Vector2(-0.707f, -0.707f));
                    referencePoints.Insert(16, fishPosition + referencePointRadius * new Vector2(-0.866f, -0.500f));
                    referencePoints.Insert(17, fishPosition + referencePointRadius * new Vector2(-0.969f, -0.259f));
                    referencePoints.Insert(18, fishPosition + referencePointRadius * new Vector2(-1.000f, 0.000f));
                    referencePoints.Insert(19, fishPosition + referencePointRadius * new Vector2(-0.969f, 0.259f));
                    referencePoints.Insert(20, fishPosition + referencePointRadius * new Vector2(-0.866f, 0.500f));
                    referencePoints.Insert(21, fishPosition + referencePointRadius * new Vector2(-0.707f, 0.707f));
                    referencePoints.Insert(22, fishPosition + referencePointRadius * new Vector2(-0.500f, 0.866f));
                    referencePoints.Insert(23, fishPosition + referencePointRadius * new Vector2(-0.259f, 0.969f));
                }
                // Start with a random position on the pattern
                patternInt = Random.Range(0, referencePoints.Count);
                fishPosition = Vector2.Lerp(referencePoints[patternInt], referencePoints[(patternInt + 1) % referencePoints.Count], Random.Range(0.0f, 1.0f));
                UpdateTransformPosition();
                break;
            case MovementPattern.CircleCCW:
                // Generates a counterclockwise circle of reference points around the fish's current position
                if (referencePoints.Count == 0)
                {
                    float referencePointRadius = Random.Range(0.8f, 1.2f); // Must be a radius of at least 0.2f
                    referencePoints.Insert(0,  fishPosition + referencePointRadius * new Vector2(0.000f,  1.000f));
                    referencePoints.Insert(1,  fishPosition + referencePointRadius * new Vector2(-0.259f, 0.969f));
                    referencePoints.Insert(2,  fishPosition + referencePointRadius * new Vector2(-0.500f, 0.866f));
                    referencePoints.Insert(3,  fishPosition + referencePointRadius * new Vector2(-0.707f, 0.707f));
                    referencePoints.Insert(4,  fishPosition + referencePointRadius * new Vector2(-0.866f, 0.500f));
                    referencePoints.Insert(5,  fishPosition + referencePointRadius * new Vector2(-0.969f, 0.259f));
                    referencePoints.Insert(6,  fishPosition + referencePointRadius * new Vector2(-1.000f, 0.000f));
                    referencePoints.Insert(7,  fishPosition + referencePointRadius * new Vector2(-0.969f, -0.259f));
                    referencePoints.Insert(8,  fishPosition + referencePointRadius * new Vector2(-0.866f, -0.500f));
                    referencePoints.Insert(9,  fishPosition + referencePointRadius * new Vector2(-0.707f, -0.707f));
                    referencePoints.Insert(10, fishPosition + referencePointRadius * new Vector2(-0.500f, -0.866f));
                    referencePoints.Insert(11, fishPosition + referencePointRadius * new Vector2(-0.259f, -0.969f));
                    referencePoints.Insert(12, fishPosition + referencePointRadius * new Vector2(0.000f,  -1.000f));
                    referencePoints.Insert(13, fishPosition + referencePointRadius * new Vector2(0.259f,  -0.969f));
                    referencePoints.Insert(14, fishPosition + referencePointRadius * new Vector2(0.500f,  -0.866f));
                    referencePoints.Insert(15, fishPosition + referencePointRadius * new Vector2(0.707f,  -0.707f));
                    referencePoints.Insert(16, fishPosition + referencePointRadius * new Vector2(0.866f,  -0.500f));
                    referencePoints.Insert(17, fishPosition + referencePointRadius * new Vector2(0.969f,  -0.259f));
                    referencePoints.Insert(18, fishPosition + referencePointRadius * new Vector2(1.000f,  0.000f));
                    referencePoints.Insert(19, fishPosition + referencePointRadius * new Vector2(0.969f,  0.259f));
                    referencePoints.Insert(20, fishPosition + referencePointRadius * new Vector2(0.866f,  0.500f));
                    referencePoints.Insert(21, fishPosition + referencePointRadius * new Vector2(0.707f,  0.707f));
                    referencePoints.Insert(22, fishPosition + referencePointRadius * new Vector2(0.500f,  0.866f));
                    referencePoints.Insert(23, fishPosition + referencePointRadius * new Vector2(0.259f,  0.969f));
                }
                // Start with a random position on the pattern
                patternInt = Random.Range(0, referencePoints.Count);
                fishPosition = Vector2.Lerp(referencePoints[patternInt], referencePoints[(patternInt + 1) % referencePoints.Count], Random.Range(0.0f, 1.0f));
                UpdateTransformPosition();
                break;
            case MovementPattern.Wander:
                // Sets the min/max wander boundaries
                if (referencePoints.Count == 0)
                {
                    referencePoints.Insert(0, fishPosition + new Vector2(-4.0f, -2.0f));
                    referencePoints.Insert(1, fishPosition + new Vector2(4.0f, 2.0f));
                }
                // Start with a random location within the assigned boundaries
                fishPosition = new Vector2(
                    Random.Range(referencePoints[0].x, referencePoints[1].x),
                    Random.Range(referencePoints[0].y, referencePoints[1].y)
                    );
                // Place the objective on top of the fish so it chooses a new wander point objective to start
                patternTimer = 0.0f;
                currentObjective = fishPosition;
                UpdateTransformPosition();
                break;
            default:
                break;
        }

        UpdateDirectionAndSpeed();

        fishingRod.oceanFish.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFish();
        FishSwim();
        UpdateDirectionFlipX();
        UpdateTransformPosition();
    }

    /// <summary>
    /// Moves the fish based on its movement pattern and deltaTime
    /// </summary>
    public void UpdateFish()
    {
        float dist = Vector2.Distance(fishPosition, fishingRod.hookPosition);
        if (fishingRod.finiteState == FishingState.Rising && dist < fishingRod.lureRadius)
        {
            // The closer the fish gets to the fishing rod, the more it attracts
            direction = Vector2.Lerp((fishingRod.hookPosition - fishPosition).normalized,
                                    (currentObjective - fishPosition).normalized, 
                                     dist / fishingRod.lureRadius);
            chasingHook = true;
        }
        // The hook moved out of range of the fish, find the nearest objective
        else if(chasingHook)
        {
            chasingHook = false;
            switch(movementPattern)
            {
                case MovementPattern.LeftRight:
                case MovementPattern.CircleCW:
                case MovementPattern.CircleCCW:
                    // Start moving towards the closest reference point
                    patternInt = 0;
                    float tempDist = Vector2.Distance(fishPosition, referencePoints[patternInt]);
                    for(int i = 0; i < referencePoints.Count; i++)
                    {
                        if(Vector2.Distance(fishPosition, referencePoints[i]) < tempDist)
                        {
                            patternInt = i;
                            tempDist = Vector2.Distance(fishPosition, referencePoints[patternInt]);
                        }
                    }
                    UpdateDirectionAndSpeed();
                    break;
                case MovementPattern.Wander:
                    UpdateDirectionAndSpeed();
                    break;
                case MovementPattern.Hooked:
                    fishPosition = fishingRod.hookPosition;
                    break;
                case MovementPattern.Idle:
                    break;
                default:
                    break;
            }
        }

        // Move the fish towards the next reference point
        switch(movementPattern)
        {
            case MovementPattern.LeftRight:
                if(Vector2.Distance(fishPosition, referencePoints[patternInt]) < 0.20f)
                {
                    patternInt = (patternInt + 1) % referencePoints.Count;
                    UpdateDirectionAndSpeed();
                }
                break;
            case MovementPattern.CircleCW:
            case MovementPattern.CircleCCW:
                if (Vector2.Distance(fishPosition, referencePoints[patternInt]) < 0.20f)
                {
                    patternInt = (patternInt + 1) % referencePoints.Count;
                    UpdateDirectionAndSpeed();
                }
                break;
            case MovementPattern.Wander:
                if (Vector2.Distance(fishPosition, currentObjective) < 0.60f)
                {
                    UpdateDirectionAndSpeed();
                }
                break;
            case MovementPattern.Hooked:
                fishPosition = fishingRod.hookPosition;
                UpdateDirectionAndSpeed();
                break;
            case MovementPattern.Idle:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Has the fish move forward as a function of its direction, velocity and deltaTime
    /// </summary>
    public void FishSwim()
    {
        fishPosition += direction * currentSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Set the objective, direction and speed based on the fish's movement pattern and current pattern step
    /// </summary>
    private void UpdateDirectionAndSpeed()
    {
        switch (movementPattern)
        {
            case MovementPattern.LeftRight:
                // Set the objective point to the patternInt indexed reference point
                currentObjective = referencePoints[patternInt];
                direction = (currentObjective - fishPosition).normalized;
                currentSpeed = fishSpeed;
                break;
            case MovementPattern.CircleCW:
            case MovementPattern.CircleCCW:
                // Set the objective point to the patternInt indexed reference point
                currentObjective = referencePoints[patternInt];
                direction = (currentObjective - fishPosition).normalized;
                currentSpeed = fishSpeed;
                break;
            case MovementPattern.Wander:
                // Select a new wander objective point within the given bounds that's at least 0.5 units away from the fish
                if (Vector2.Distance(fishPosition, currentObjective) < 0.60f)
                {
                    // If the fish has a movement cooldown, wait out the timer first before moving again
                    if(patternTimer > 0.0f)
                    {
                        currentSpeed = fishSpeed * (Vector2.Distance(fishPosition, currentObjective) / 0.75f);
                        patternTimer -= Time.deltaTime;
                    }
                    else
                    {
                        do
                        {
                            currentObjective = new Vector2(
                                Random.Range(referencePoints[0].x, referencePoints[1].x),
                                Random.Range(referencePoints[0].y, referencePoints[1].y)
                            );
                        }
                        while (Vector2.Distance(fishPosition, currentObjective) < 0.75f);
                        patternTimer = Random.Range(0.75f, 2.0f);
                        direction = (currentObjective - fishPosition).normalized;
                        currentSpeed = fishSpeed;
                    }
                }
                else
                {
                    direction = (currentObjective - fishPosition).normalized;
                    currentSpeed = fishSpeed;
                }
                break;
            case MovementPattern.Hooked:
                direction = new Vector2(0.0f, 0.0f);
                currentSpeed = 0.0f;
                break;
            case MovementPattern.Idle:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Updates whether the fish is facing left or right based on its velocity
    /// </summary>
    public void UpdateDirectionFlipX()
    {
        if(direction.x < -0.001f)
        {
            spriteRenderer.flipX = false;
        }
        else if(direction.x > 0.001f)
        {
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// Moves the transform.position to reflect fishPosition
    /// </summary>
    private void UpdateTransformPosition()
    {
        transform.position = new Vector3(fishPosition.x, fishPosition.y, transform.position.z);
    }


    // returns a random weight within a range based on the type of fish 
    float RandomWeight(FishType type)
    {
        switch (type)
        {
            case FishType.WooMango:
                return Random.Range(7.0f, 9.6f);
            case FishType.AngleLilac:
                return Random.Range(3.0f, 7.0f);
            case FishType.MagiCarp:
                return Random.Range(1.0f, 6.0f);
            case FishType.ToxicBlockhead:
                return Random.Range(2.0f, 12.0f);
            case FishType.Chad:
                return Random.Range(70.0f, 96.0f);
            case FishType.PinkFish:
                return Random.Range(8.0f, 11.0f);
            default:
                return 0.0f;
        }
    }
}
