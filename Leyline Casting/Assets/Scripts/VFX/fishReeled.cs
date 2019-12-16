using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishReeled : MonoBehaviour
{
    // Prototype to manipulate
    public GameObject particles;
    public GameObject moneyText;

    private List<GameObject> instantiatedObjects;
    private Queue<GameObject> destructionQueue;
    private Queue<float> objectDeaths;

    private bool running = false;           // Whether or not the script is currently running
    private float floatingSpeed = 0.50f;     // Speed at which the text rises
    private float lifeTime = 1.0f;          // Lifetime of each text object

    void Start()
    {
        instantiatedObjects = new List<GameObject>();
        destructionQueue = new Queue<GameObject>();
        objectDeaths = new Queue<float>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.O))
        {
            Begin("200", new Vector3(0, 0, 0));
            Debug.Log(Time.time);
            Debug.Log(Time.time + lifeTime);
        }
        */

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
        GameObject tempCash = moneyText;
        GameObject tempSystem = particles;

        GameObject instantiatedObject = Instantiate(moneyText, this.transform);
        Instantiate(particles, instantiatedObject.transform);


        instantiatedObjects.Add(instantiatedObject);
        destructionQueue.Enqueue(instantiatedObject);


        Invoke("DestroyObject", lifeTime);

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
            // Update the position of the object if it's still got time
            Vector3 pos = instantiatedObjects[i].transform.localPosition;
            pos.y += floatingSpeed;
            pos.x += Mathf.Sin(Time.time * 4.0f) / 4.0f;
            instantiatedObjects[i].transform.localPosition = pos;
        }
    }

    private void DestroyObject()
    {
        GameObject obj = destructionQueue.Dequeue();

        instantiatedObjects.Remove(obj);
        Destroy(obj);
    }
}
