using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Based on https://gamedevacademy.org/complete-guide-to-procedural-level-generation-in-unity-part-1/

public class MyLevelGen : MonoBehaviour
{
    [SerializeField]
    private int mapWidthInTiles, mapDepthInTiles;

    [SerializeField]
    private GameObject tilePrefab;
    
 [SerializeField]
    private GameObject objectiveObj;

    private MeshRenderer _tileMeshRenderer;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        // get the tile dimensions from the tile Prefab
        tilePrefab.TryGetComponent(out _tileMeshRenderer);
        var tileSize = _tileMeshRenderer.bounds.size;
        var tileWidth = (int)tileSize.x;
        var tileDepth = (int)tileSize.z;

        // for each Tile, instantiate a Tile in the correct position
        for (var xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++)
        {
            for (var zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++)
            {
                // calculate the tile position based on the X and Z indices
                var tilePosition = new Vector3(
                    this.gameObject.transform.position.x + xTileIndex * tileWidth,
                    this.gameObject.transform.position.y,
                    this.gameObject.transform.position.z + zTileIndex * tileDepth);
                // instantiate a new Tile
                var tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                tile.transform.parent = this.transform;
            }
        }
        
        GenerateObjective();
    }

    private void GenerateObjective()
    {
        var bounds = _tileMeshRenderer.bounds;
        var whereToGenerateX = Random.Range(-bounds.size.x, bounds.size.x);
        var whereToGenerateZ = Random.Range(-bounds.size.z, bounds.size.z);

        var objectivePos = new Vector3(whereToGenerateX, transform.position.y + 0.5f, whereToGenerateZ);
        
//        Instantiate(objectiveObj, objectivePos, Quaternion.identity);
    }
}
