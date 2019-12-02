using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGuiManager : MonoBehaviour
{
    public GameObject shopGameObject;
    private bool shopDisplaying = false;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            shopGameObject.SetActive(!shopDisplaying);
        }
    }
}
