using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMethods : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // loads scene specified by scene name
    // sceneName is the scene filename minus the extension (no ".unity")
    public void ChangeScene(string sceneName)
    {
        // if no scene name specified
        if (sceneName == "" || sceneName == null)
            return; // do nothing

        SceneManager.LoadScene(sceneName);
    }
}
