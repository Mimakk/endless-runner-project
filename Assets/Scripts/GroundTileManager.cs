using System.Collections.Generic;
using UnityEngine;

public class GroundTileManager : MonoBehaviour
{
    public Transform player;
    public List<GameObject> groundSegments;
    public float groundLength = 100f; // The length of cube along the Z axis

    void Update()
    {
        // Check the first ground in the list
        GameObject firstGround = groundSegments[0];

        if (player.position.z > firstGround.transform.position.z + groundLength)
        {
            MoveGroundToFront(firstGround);
        }
    }

    void MoveGroundToFront(GameObject ground)
    {
        // Find the position of the LAST ground in the list
        GameObject lastGround = groundSegments[groundSegments.Count - 1];

        // Calculate the new position (end of the last ground)
        Vector3 newPos = new Vector3(ground.transform.position.x, ground.transform.position.y, lastGround.transform.position.z + groundLength);

        // Teleport the ground
        ground.transform.position = newPos;

        // Update the List: Remove from front, Add to back
        groundSegments.RemoveAt(0);
        groundSegments.Add(ground);
    }
}
