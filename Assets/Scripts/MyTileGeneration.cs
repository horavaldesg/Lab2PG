﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Based on https://gamedevacademy.org/complete-guide-to-procedural-level-generation-in-unity-part-1/

public class MyTileGeneration : MonoBehaviour
{
    [SerializeField]
    NoiseMapGeneration noiseMapGeneration;

    [SerializeField]
    private MeshRenderer tileRenderer;

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshCollider meshCollider;

    [SerializeField]
    private float mapScale;

    [SerializeField]
    private TerrainType[] terrainTypes;

    [SerializeField]
    private float heightMultiplier;

    [SerializeField]
    private AnimationCurve heightCurve;

    [SerializeField]
    private NoiseMapGeneration.Wave[] waves;

    void Start()
    {
        GenerateTile();
    }

    void GenerateTile()
    {
        // calculate tile depth and width based on the mesh vertices
        Vector3[] meshVertices = this.meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;

        // calculate the offsets based on the tile position
        float offsetX = -this.gameObject.transform.position.x;
        float offsetZ = -this.gameObject.transform.position.z;

        // calculate the offsets based on the tile position
        float[,] heightMap = this.noiseMapGeneration.GenerateNoiseMap(tileDepth, tileWidth, this.mapScale, offsetX, offsetZ, waves);

        // generate a heightMap using noise
        Texture2D tileTexture = BuildTexture(heightMap);
        this.tileRenderer.material.mainTexture = tileTexture;
        UpdateMeshVertices(heightMap);
    }

    private Texture2D BuildTexture(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);
        //int tileDepth = noiseMap.GetLength(0);
        //int tileWidth = noiseMap.GetLength(1);

        Color[] colorMap = new Color[tileDepth * tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                // transform the 2D map index is an Array index
                int colorIndex = zIndex * tileWidth + xIndex;
                float height = heightMap[zIndex, xIndex];
                
                TerrainType terrainType = ChooseTerrainType(height);
                // assign as color a shade of grey proportional to the height value
                //colorMap[colorIndex] = Color.Lerp(Color.black, Color.white, height);
                colorMap[colorIndex] = terrainType.color;
            }
        }

        // create a new texture and set its pixel colors
        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();

        return tileTexture;
    }

    
    [System.Serializable]
    public class TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }

    TerrainType ChooseTerrainType(float height)
    {
        // for each terrain type, check if the height is lower than the one for the terrain type
        foreach (TerrainType terrainType in terrainTypes)
        {
            // return the first terrain type whose height is higher than the generated one
            if (height < terrainType.height)
            {
                return terrainType;
            }
        }
        return terrainTypes[terrainTypes.Length - 1];
    }


    private void UpdateMeshVertices(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        Vector3[] meshVertices = this.meshFilter.mesh.vertices;

        // iterate through all the heightMap coordinates, updating the vertex index
        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                float height = heightMap[zIndex, xIndex];

                Vector3 vertex = meshVertices[vertexIndex];
                // change the vertex Y coordinate, proportional to the height value
                //meshVertices[vertexIndex] = new Vector3(vertex.x, height * this.heightMultiplier, vertex.z);
                meshVertices[vertexIndex] = new Vector3(vertex.x, this.heightCurve.Evaluate(height) * this.heightMultiplier, vertex.z);
                //we are rescaling the height according to the height curve here
                //we could write code to do this, but we will use the built in function
                vertexIndex++;
            }
        }

        // update the vertices in the mesh and update its properties
        this.meshFilter.mesh.vertices = meshVertices;
        this.meshFilter.mesh.RecalculateBounds();
        this.meshFilter.mesh.RecalculateNormals();
        // update the mesh collider
        this.meshCollider.sharedMesh = this.meshFilter.mesh;
    }
}
