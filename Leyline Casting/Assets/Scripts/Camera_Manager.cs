using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Manager : MonoBehaviour
{
    // External References
    public Camera activeCamera;
    public FishingRod fishingRod;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private  float duration;
    private float timeTotal;

    private bool moving = false;

    public AnimationCurve movementCurve;

    // Start is called once
    void Start()
    {
        activeCamera = Camera.main;
        fishingRod = GameObject.Find("fishing_pole").GetComponent<FishingRod>();
    }

    // Update is called once per frame
    void Update()
    {
        // Testing camera movement
        /*if (Input.GetKeyDown(KeyCode.L))
        {
            Vector3 newPos = new Vector3(activeCamera.transform.position.x, activeCamera.transform.position.y + 5, activeCamera.transform.position.z);
            TrackTo(newPos, 1.0f);
        }

        if (moving)
            ContinueMoving();*/

        TrackTo(fishingRod.hookPosition, 2.0f);

        if (moving)
            ContinueMoving();
    }

    /// <summary>
    /// Use to move the camera to a new position over a given duration
    /// </summary>
    /// <param name="_targetPosition"></param>
    /// <param name="_duration"></param>
    public void TrackTo(Vector3 _targetPosition, float _duration)
    {
        // Make sure the camera doesn't change z values
        _targetPosition.z = activeCamera.transform.position.z;

        // Make sure we're moving to a new position
        if (_targetPosition == activeCamera.transform.position)
            return;


        // Set up all variables needed to run continue track
        duration = _duration;
        targetPosition = _targetPosition;
        startPosition = activeCamera.transform.position;
        timeTotal = 0.0f;
        moving = true;
    }

    /// <summary>
    /// Internal function to keep moving to the target position;
    /// </summary>
    private void ContinueMoving()
    {
        //  Check if we've hit our destination
        if (timeTotal >= duration)
        {
            activeCamera.transform.position = targetPosition;
            moving = false;
            timeTotal = 0.0f;
        }

        // If not, continue moving toward target position
        timeTotal += Time.deltaTime;
        float timePercentage = timeTotal / duration;
        float movePercentage = movementCurve.Evaluate(timePercentage);

        activeCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, movePercentage);
    }
}
