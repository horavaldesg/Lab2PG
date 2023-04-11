using UnityEngine;

public class ProceduralHut : MonoBehaviour
{
    public GameObject fencePrefab;
    public GameObject fenceClosed;
    public GameObject fenceOpen;
    public int fenceLength = 10;
    public int fenceWidth = 10;
    public int fenceDepth = 2;
    public float postWidth = 0.1f;
    public float fenceSeparation = 5;
    
    private void Start()
    {
        GenerateFence();
    }

    private void GenerateFence()
    {
        // Create a fence parent object
        var fenceParent = new GameObject("Fence")
        {
            transform =
            {
                parent = transform
            }
        };
        // Generate the horizontal fence posts
        for (var i = 0; i < fenceLength; i++)
        {
            for (var j = 0; j < fenceWidth; j++)
            {
                if (j == 0)
                {
                    var position = new Vector3(transform.position.x + i * fenceSeparation, transform.position.y + 1,
                        transform.position.z + j);
                    var post = Instantiate(fenceClosed, position, Quaternion.identity, fenceParent.transform);
                    post.transform.localScale = new Vector3(postWidth * 5, 2.5f, (float)fenceDepth / 10.0f);
                }
                else if (j == fenceWidth -1)
                {
                    var position = new Vector3(transform.position.x + i * fenceSeparation, transform.position.y + 1,
                        transform.position.z + j);
                    var post = Instantiate(fenceOpen, position, Quaternion.identity, fenceParent.transform);
                    post.transform.localScale = new Vector3(postWidth * 5, 2.5f, (float)fenceDepth / 10.0f);
                }
                else
                {
                    var position = new Vector3(transform.position.x + i * fenceSeparation, transform.position.y + 1,
                        transform.position.z + j);
                    var post = Instantiate(fencePrefab, position, Quaternion.identity, fenceParent.transform);
                    post.transform.localScale = new Vector3(postWidth * 5, 2.5f, (float)fenceDepth / 10.0f);
                }
            }
        }

        // Generate the vertical fence posts on the edges
        /*for (int i = 0; i <= fenceLength; i++)
        {
            Vector3 position1 = new Vector3(transform.position.x + i, transform.position.y + fenceDepth / 2, transform.position.z);
            var post1 = Instantiate(fencePrefab, position1, Quaternion.identity, fenceParent.transform);
            post1.transform.localScale = new Vector3(postWidth, (float)fenceDepth / 10.0f, 1.0f);

            Vector3 position2 = new Vector3(transform.position.x + i, transform.position.y + fenceDepth / 2, transform.position.z + fenceWidth);
            GameObject post2 = Instantiate(fencePrefab, position2, Quaternion.identity, fenceParent.transform);
            post2.transform.localScale = new Vector3(postWidth, (float)fenceDepth / 10.0f, 1.0f);
        }
        */

        /*for (int i = 0; i <= fenceWidth; i++)
        {
            Vector3 position1 = new Vector3(transform.position.x, transform.position.y + fenceDepth / 2, transform.position.z + i);
            GameObject post1 = Instantiate(fencePrefab, position1, Quaternion.identity, fenceParent.transform);
            post1.transform.localScale = new Vector3(postWidth, (float)fenceDepth / 10.0f, 1.0f);

            Vector3 position2 = new Vector3(transform.position.x + fenceLength, transform.position.y + fenceDepth / 2, transform.position.z + i);
            GameObject post2 = Instantiate(fencePrefab, position2, Quaternion.identity, fenceParent.transform);
            post2.transform.localScale = new Vector3(postWidth, (float)fenceDepth / 10.0f, 1.0f);
        }*/
    }
}