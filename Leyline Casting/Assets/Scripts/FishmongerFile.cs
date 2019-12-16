using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[Serializable]
public class FileJSON
{
    public int varietyCatchNumber; // how many different types of fish have been caught at once
    public Catch deepestCatch;
    public Catch heaviestCatch;
    public Catch varietyCatch;

    // one of each fish type that has been caught and the heaviest of that type of fish caught
    // doesn't need to be in order, but reading this should account for that not every type of fish will have an entry
    public List<Fish> biggestFishCaught;

    public FileJSON()
    {
        varietyCatchNumber = 0;
        deepestCatch = new Catch();
        heaviestCatch = new Catch();
        varietyCatch = new Catch();

        biggestFishCaught = new List<Fish>();
    }
}

[Serializable]
public class Catch
{
    public float weight; // total weight of fish in "fish"
    public float depth;
    public List<Fish> fish;

    public Catch(float depth = 0.0f, float weight = 0.0f)
    {
        this.weight = weight;
        this.depth = depth;
        fish = new List<Fish>();
    }
}

public enum FishType
{
    WooMango, // Yellow,png
    AngleLilac, // Lilac.png
    ToxicBlockhead, // Green.png
    PinkFish,
    Chad, // BlueFormar.png
    MagiCarp, // totally not a rip off Orange.png
    DogFish
}

public class FishmongerFile : MonoBehaviour
{

    public FileJSON fishFile;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log(FileIO.DirectoryAddress);
        // uncomment this to create the file
        //CreateTestFile();

        // uncomment this to read it back in
        ReadFishmongerFile();
        UpdateDisplay();

        // you can use this or the property inspector for the script to see
        // if fishFile has been populated once ReadFishmongerFile is called
        //Debug.Log(fishFile.deepestCatch.depth);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void ReadFishmongerFile()
    {
        string jsonPath = $"{FileIO.DirectoryAddress}fishmonger_file.json";

        if (!File.Exists(jsonPath))
            return;

        StreamReader readStream = new StreamReader(jsonPath);

        string line;
        string wholeText = "";

        while ((line = readStream.ReadLine()) != null)
        {
            wholeText += $"{line}\n";
        }
        readStream.Close();

        fishFile = JsonUtility.FromJson<FileJSON>(wholeText);
    }

    void UpdateDisplay()
    {
        string wholeString = "";

        // Deepest Catch
        wholeString += $"Deepest Catch: {fishFile.deepestCatch.depth}ft\n\n";

        // Heaviest Catch
        wholeString += $"Heaviest Catch: {fishFile.heaviestCatch.weight}\n";
        for(int i = 0; i < fishFile.heaviestCatch.fish.Count; ++i)
        {
            Fish currentFish = fishFile.heaviestCatch.fish[i];
            wholeString += $"Fish {i}: {currentFish.type}, {currentFish.weight}lbs\n";
        }
        wholeString += "\n";

        // Variety Catch
        wholeString += $"Most Varieties of Fish Caught at Once: {fishFile.varietyCatchNumber}\n";
        for (int i = 0; i < fishFile.varietyCatch.fish.Count; ++i)
        {
            Fish currentFish = fishFile.varietyCatch.fish[i];
            wholeString += $"Fish {i}: {currentFish.type}\n";
        }
        wholeString += "\n";

        // Fish types
        wholeString += "Piscyclopedia: \n";
        string[] fishTypes = Enum.GetNames(typeof(FishType));
        for(int i = 0; i < fishFile.biggestFishCaught.Count; ++i)
        {
            fishTypes[(int)fishFile.biggestFishCaught[i].type] += $", Biggest catch: {fishFile.biggestFishCaught[i].weight}lbs\n";
        }

        for(int i = 0; i < fishTypes.Length; i++)
        {
            if (fishTypes[i].Contains(", Biggest catch:"))
                wholeString += fishTypes[i];
            else
                wholeString += "????? \n";
        }

        wholeString += "\n";

        // set text to wholeString
        GetComponent<Text>().text = wholeString;
    }


    void WriteFishmongerFile()
    {
        // serialize to write
        string json = JsonUtility.ToJson(fishFile);

        string jsonPath = $"{FileIO.DirectoryAddress}fishmonger_file.json";

        // write file
        StreamWriter writeStream = new StreamWriter(jsonPath);
        writeStream.Write(json);
        writeStream.Close();
    }

    // creates test json file for fishmonger file @ /fishmonger_file_test.json
    void CreateTestFile()
    {
        // populate json with info
        FileJSON fishFile = new FileJSON();

        fishFile.deepestCatch = new Catch();

        fishFile.deepestCatch.weight = 10.2f;
        fishFile.deepestCatch.depth = 100.0f;

        fishFile.deepestCatch.fish = new List<Fish>();
        fishFile.deepestCatch.fish.Add(new Fish());
        fishFile.deepestCatch.fish[0].type = FishType.WooMango;
        fishFile.deepestCatch.fish[0].weight = 10.2f;

        fishFile.heaviestCatch = new Catch();

        fishFile.heaviestCatch.weight = 57.2f;
        fishFile.heaviestCatch.depth = 40.0f;

        fishFile.heaviestCatch.fish = new List<Fish>();
        fishFile.heaviestCatch.fish.Add(new Fish());
        fishFile.heaviestCatch.fish[0].type = FishType.AngleLilac;
        fishFile.heaviestCatch.fish[0].weight = 10.3f;
        fishFile.heaviestCatch.fish.Add(new Fish());
        fishFile.heaviestCatch.fish[1].type = FishType.Chad;
        fishFile.heaviestCatch.fish[1].weight = 30.2f;
        fishFile.heaviestCatch.fish.Add(new Fish());
        fishFile.heaviestCatch.fish[2].type = FishType.ToxicBlockhead;
        fishFile.heaviestCatch.fish[2].weight = 16.7f;

        fishFile.varietyCatchNumber = 4;

        fishFile.varietyCatch = new Catch();

        fishFile.varietyCatch.weight = 46.8f;
        fishFile.varietyCatch.depth = 70.6f;

        fishFile.varietyCatch.fish = new List<Fish>();
        fishFile.varietyCatch.fish.Add(new Fish());
        fishFile.varietyCatch.fish[0].type = FishType.AngleLilac;
        fishFile.varietyCatch.fish[0].weight = 9.8f;
        fishFile.varietyCatch.fish.Add(new Fish());
        fishFile.varietyCatch.fish[1].type = FishType.Chad;
        fishFile.varietyCatch.fish[1].weight = 20.2f;
        fishFile.varietyCatch.fish.Add(new Fish());
        fishFile.varietyCatch.fish[2].type = FishType.ToxicBlockhead;
        fishFile.varietyCatch.fish[2].weight = 10.4f;
        fishFile.varietyCatch.fish.Add(new Fish());
        fishFile.varietyCatch.fish[2].type = FishType.WooMango;
        fishFile.varietyCatch.fish[2].weight = 6.4f;

        fishFile.biggestFishCaught = new List<Fish>();

        fishFile.biggestFishCaught.Add(new Fish());
        fishFile.biggestFishCaught[0].type = FishType.AngleLilac;
        fishFile.biggestFishCaught[0].weight = 10.3f;

        fishFile.biggestFishCaught.Add( new Fish());
        fishFile.biggestFishCaught[1].type = FishType.WooMango;
        fishFile.biggestFishCaught[1].weight = 10.2f;

        fishFile.biggestFishCaught.Add(new Fish());
        fishFile.biggestFishCaught[2].type = FishType.ToxicBlockhead;
        fishFile.biggestFishCaught[2].weight = 16.7f;

        fishFile.biggestFishCaught.Add(new Fish());
        fishFile.biggestFishCaught[3].type = FishType.Chad;
        fishFile.biggestFishCaught[3].weight = 30.2f;

        // serialize to write
        string json = JsonUtility.ToJson(fishFile);

        string jsonPath = $"{FileIO.DirectoryAddress}fishmonger_file_test.json";

        // write file
        StreamWriter writeStream = new StreamWriter(jsonPath);
        writeStream.Write(json);
        writeStream.Close();

    }

    // file format
    // deepest cast
    // heaviest catch / most expensive catch
    // catch with the biggest variety of fish -> hard to quantify
    // 6 different fish
    // 5 fish, 1 fomor
}
