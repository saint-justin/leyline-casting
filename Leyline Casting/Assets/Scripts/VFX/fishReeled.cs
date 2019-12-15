using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishReeled : MonoBehaviour
{
    // Prototype to manipulate
    public GameObject moneyText;
    private Queue<GameObject> instantiatedObjects;
    private Queue<float> objectDeaths;
    private bool running = false;           // Whether or not the script is currently running
    private float floatingSpeed = 2.0f;     // Speed at which the text rises
    private float lifeTime = 1.0f;          // Lifetime of each text object

    public ParticleSystem particles;

    void Start()
    {
        instantiatedObjects = new Queue<GameObject>();
        objectDeaths = new Queue<float>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Begin("200", new Vector3(0, 0, 0));
            Debug.Log("NEW VFX");
        }

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
        GameObject instantiatedObject = Instantiate(moneyText, this.transform.position, Quaternion.identity);
        instantiatedObjects.Enqueue(instantiatedObject);
        objectDeaths.Enqueue(Time.time + lifeTime);

        Debug.Log("Object instantiated...");
        Debug.Log(instantiatedObject);

        // Run the particle system
        particles.Play();

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
            GameObject obj = instantiatedObjects.Dequeue();
            float life = objectDeaths.Dequeue();

            // Update lifetime of the object & check if it's expired
            if (life <= Time.time)
            {
                Destroy(obj);
                Debug.Log("Destroying text object...");
                continue;
            }

            // Update the position of the object if it's still got time
            Vector3 pos = obj.transform.position;
            pos.y += floatingSpeed;
            pos.x += Mathf.Sin(Time.deltaTime);
            obj.transform.position = pos;

            instantiatedObjects.Enqueue(obj);
            objectDeaths.Enqueue(life);
        }
    }
}
