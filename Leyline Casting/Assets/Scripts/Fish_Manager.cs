using UnityEngine;
using System.Collections.Generic;

public class Fish_Manager : MonoBehaviour
{
    public GameObject FishPrefab;
    public GameObject FishParent;
    Random rand = new Random();


    // Default depths for each of the fish types
    public float GreenDepth = -5.0f;
    public float LilacDepth = -10.0f;
    public float OrangeDepth = -15.0f;
    public float PinkDepth = -25.0f;
    public float YellowDepth = -30.0f;

    public float depthDeviation = -10.0f;

    // Importing fish textures
    public Sprite[] fishSprites = new Sprite[6]; // In order: Blue Fomor, Greenboi, Lilac, Orange, Pink, Yellow
    public Dictionary<FishType, Sprite> spriteIndexes;


    // Start is called before the first frame update
    void Start()
    {
        spriteIndexes = new Dictionary<FishType, Sprite>();

        // Setting up the sprite dictionary 
        spriteIndexes.Add(FishType.AngleLilac, fishSprites[2]);
        spriteIndexes.Add(FishType.Chad, fishSprites[0]);
        spriteIndexes.Add(FishType.MagiCarp, fishSprites[3]);
        spriteIndexes.Add(FishType.PinkFish, fishSprites[4]);
        spriteIndexes.Add(FishType.ToxicBlockhead, fishSprites[1]);
        spriteIndexes.Add(FishType.WooMango, fishSprites[5]);

        // Spawning the sets of fish
        SpawnFishSet(GreenDepth, 25, FishType.WooMango);
        SpawnFishSet(LilacDepth, 15, FishType.AngleLilac);
        SpawnFishSet(OrangeDepth, 15, FishType.PinkFish);
        SpawnFishSet(PinkDepth, 15, FishType.MagiCarp);
        SpawnFishSet(YellowDepth, 15, FishType.ToxicBlockhead);
        SpawnFishSet(YellowDepth, 15, FishType.Chad);
    }

    /// <summary>
    /// Spawns a fish in a random xPos within the depthDeviation distance of the depthMean passed in
    /// </summary>
    public void SpawnFish(float _depthMean, FishType _type)
    {
        // Generate a copy of the fish prefab
        GameObject newFish = FishPrefab;

        // Calculate it's position
        float yPos = Gaussian(0.0f, depthDeviation) - _depthMean;
        float xPos = Random.Range(-2.0f, 2.0f);
        if (yPos > 0.0f)
            yPos -= 5.0f;

        // Adjust type
        newFish.GetComponent<Fish>().type = _type;
        newFish.GetComponent<SpriteRenderer>().sprite = spriteIndexes[_type];
        newFish.transform.position = new Vector3(xPos, yPos, 0);

        // Instantiate & adjust transform parent
        Instantiate(newFish, FishParent.transform);
        //newFish.transform.SetParent(FishParent.transform);
    }
    
    /// <summary>
    /// Used to spawn a set of fish w/ a given depth mean
    /// </summary>
    public void SpawnFishSet(float _depthMean, int _howMany, FishType _type)
    {
        for (int i = 0; i < _howMany; i++)
        {
            SpawnFish(_depthMean, _type);
        }
    }

    /// <summary>
    /// Returns a float distributed about a given mean according to the passed standard deviation
    /// </summary>
    float Gaussian(float _mean, float _stdDev)
    {
        float u1 = 1.0f - Random.Range(0.0f, 1.0f);
        float u2 = 1.0f - Random.Range(0.0f, 1.0f);
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
        return _mean + _stdDev * randStdNormal; //random normal(mean,stdDev^2)

    }
}
