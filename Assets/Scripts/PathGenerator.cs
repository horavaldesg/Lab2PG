using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PathGenerator : MonoBehaviour
{
    public float pathWidth = 2f; // The width of the path
    public float pathLength = 10f; // The length of each path segment
    public float turnChance = 0.5f; // The probability of turning at each segment
    public GameObject meshPrefab; // The MeshRenderer prefab

    private List<Vector3> pathPoints = new(); // The list of path points
    private List<int> pathTriangles = new(); // The list of path triangles

    private void Start()
    {
        // Instantiate the MeshRenderer prefab and get its MeshRenderer component
        var meshObject = Instantiate(meshPrefab, transform);
        var meshRenderer = meshObject.GetComponent<MeshRenderer>();

        // Generate the path
        GeneratePath(meshRenderer);
    }

    private void GeneratePath(MeshRenderer meshRenderer)
    {
        // Add the first point at the bottom left corner of the MeshRenderer
        var startPoint = new Vector3(meshRenderer.bounds.min.x, meshRenderer.bounds.min.y, 0f);
        pathPoints.Add(startPoint);

        // Set the current direction to right
        var direction = Vector3.right;

        // Generate the path segments
        while (pathPoints[pathPoints.Count - 1].x < meshRenderer.bounds.max.x)
        {
            // Generate the next segment
            var segmentEnd = pathPoints[pathPoints.Count - 1] + direction * pathLength;

            // Randomly decide whether to turn or not
            if (Random.value < turnChance)
                // Turn left or right
                direction = Quaternion.Euler(0f, 0f, Random.Range(-90f, 90f)) * direction;

            // Add the new segment end point
            pathPoints.Add(segmentEnd);

            // Generate the path width vertices
            var rightOffset = Quaternion.Euler(0f, 0f, -90f) * direction * (pathWidth / 2f);
            var leftOffset = Quaternion.Euler(0f, 0f, 90f) * direction * (pathWidth / 2f);

            // Add the four vertices for the new segment
            pathPoints.Add(segmentEnd + rightOffset);
            pathPoints.Add(segmentEnd + leftOffset);
            pathPoints.Add(pathPoints[pathPoints.Count - 2] + rightOffset);
            pathPoints.Add(pathPoints[pathPoints.Count - 2] + leftOffset);

            // Add the triangles for the new segment
            var startIndex = pathPoints.Count - 6;
            pathTriangles.Add(startIndex);
            pathTriangles.Add(startIndex + 1);
            pathTriangles.Add(startIndex + 2);
            pathTriangles.Add(startIndex + 2);
            pathTriangles.Add(startIndex + 1);
            pathTriangles.Add(startIndex + 3);
        }

        // Create the Mesh and add it to the MeshFilter component of the instantiated prefab
        var pathMesh = new Mesh();
        pathMesh.SetVertices(pathPoints);
        pathMesh.SetTriangles(pathTriangles, 0);
        meshRenderer.GetComponent<MeshFilter>().mesh = pathMesh;
    }
}