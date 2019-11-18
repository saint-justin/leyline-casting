using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    // Getting refs to external items
    public GameObject angleSprite;
    public GameObject arrowSprite;

    private bool powerMeasured = false;
    private bool angleMeasuring = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!powerMeasured)
        {
            if (Input.GetMouseButtonDown(0))
            {

            }
            else if (Input.GetMouseButtonUp(0))
            {

            }
        }

    }


}
