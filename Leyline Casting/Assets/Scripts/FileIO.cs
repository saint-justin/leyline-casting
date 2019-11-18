using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileIO
{
    private static string filePath;
    public static string DirectoryName = "Leyline Casting";

    public static string DirectoryAddress
    {
        get
        {
            return filePath;
        }
    }
    //private Text textObject;
    //public string stringToWrite;

    // Start is called before the first frame update
    static FileIO()
    {
        // get path to user's documents
        filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        // adds directoryname to filepath
        filePath += $"{Path.DirectorySeparatorChar}{DirectoryName}{Path.DirectorySeparatorChar}";

        // the following method only creates directories if they don't already exist
        Directory.CreateDirectory(filePath); 
    }

    //// Update is called once per frame
    //void static Update()
    //{
        
    //}

    //public void WriteFile(string _path, string toWrite)
    //{
    //    StreamWriter writeStream = new StreamWriter(_path);

    //    writeStream.Write(toWrite);

    //    writeStream.Close();
    //}

    //public string ReadScores(string _path)
    //{
    //    StreamReader readStream = new StreamReader(_path);

    //    string line;
    //    string wholeText = "";

    //    while ((line = readStream.ReadLine()) != null)
    //    {
    //        wholeText += $"{line}\n";
    //    }
    //    readStream.Close();

    //    return wholeText;

    //}
}
