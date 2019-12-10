using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishReeled : MonoBehaviour
{
    // Prototype to manipulate
    public GameObject moneyText;
    private List<GameObject> instantiatedObjects;
    private List<float> objectLives;
    private bool running = false;           // Whether or not the script is currently running
    private float floatingSpeed = 2.0f;     // Speed at which the text rises
    private float lifeTime = 0.6f;          // Lifetime of each text object

    void Start()
    {
        instantiatedObjects = new List<GameObject>();
        objectLives = new List<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (running)
        {
            ContinueRunning();
        }
    }

    /// <summary>
    /// Function to spawn and start animating the text object
    /// </summary>
    public void Begin(string _input, Vector3 _startPos)
    {
        // Spawn in the new text object
        GameObject instantiatedObject = Instantiate(moneyText, _startPos, Quaternion.identity);
        instantiatedObjects.Add(instantiatedObject);
        objectLives.Add(0.0f);

        // Start the animating fxn
        running = true;
        ContinueRunning();
    }

    /// <summary>
    /// Internal function to continue animating the object
    /// </summary>
    private void ContinueRunning()
    {
        // Update every item in instantiatedObjects to float up
        for(int i=0; i<instantiatedObjects.Count; i++)
        {
            // Update lifetime of the object & check if it's expired
            objectLives[i] += Time.deltaTime;
            if (objectLives[i] >= lifeTime)
            {
                objectLives.RemoveAt(i);
                Destroy(instantiatedObjects[i]);
                continue;
            }

            // Update the position of the object if it's still got time
            Vector3 pos = instantiatedObjects[i].transform.position;
            pos.y += floatingSpeed;
            pos.x += Mathf.Sin(Time.deltaTime);
            instantiatedObjects[i].transform.position = pos;
        }
    }
}
