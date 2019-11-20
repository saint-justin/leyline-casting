using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float timeTillFlipFlop = 3.0f;
    public float fishSpeed = 2.0f;
    private float directionMod = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTillFlipFlop -= Time.deltaTime;
        if (timeTillFlipFlop <= 0)
        {
            directionMod *= -1.0f;
            timeTillFlipFlop = 3.0f;
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        }

        Vector3 newPosition = gameObject.transform.position;
        newPosition.x += Time.deltaTime * fishSpeed * directionMod;
        gameObject.transform.position = newPosition;
    }
}
