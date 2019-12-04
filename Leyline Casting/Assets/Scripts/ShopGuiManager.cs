using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopGuiManager : MonoBehaviour
{
    public GameObject shopGameObject;
    public GameObject shopGameIcon;
    private bool shopDisplaying = true;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (shopDisplaying)
            {
                Vector3 p = shopGameObject.transform.position;
                p.x -= 3000;
                shopGameObject.transform.position = p;

                Vector3 p2 = shopGameIcon.transform.position;
                p2.x += 3000;
                shopGameIcon.transform.position = p2;
            }
            else
            {
                Vector3 p = shopGameObject.transform.position;
                p.x += 3000;
                shopGameObject.transform.position = p;

                Vector3 p2 = shopGameIcon.transform.position;
                p2.x -= 3000;
                shopGameIcon.transform.position = p2;
            }

            shopDisplaying = !shopDisplaying;
        }
    }
}
