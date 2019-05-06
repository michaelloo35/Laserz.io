using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform[] playerPositions = new Transform[0];
    public Camera camera;


    public void Initialize(Transform[] playerPositions)
    {
        this.playerPositions = playerPositions;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPositions.Length > 0)
            camera.transform.position = getAveragePosition(playerPositions);
    }


    private Vector3 getAveragePosition(Transform[] positions)
    {
        Vector3 average = new Vector3(0, 0, 0);
        foreach (var position in positions)
        {
            average += position.position;
        }

        average /= positions.Length;
        average.z = -10;
        return average;
    }
}