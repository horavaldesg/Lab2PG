//Code From https://gist.github.com/goldennoodles/7cc02c0f92fd126e24cb50e6f153d5be
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteLevelGen : MonoBehaviour
{
   public GameObject plane;
    public GameObject player;

    private const int Radius = 5;
    private const int PlaneOffset = 10;

    private Vector3 _startPos = Vector3.zero;

    private int xPlayerMove => (int)(player.transform.position.x - _startPos.x);
    private int zPlayerMove => (int)(player.transform.position.z - _startPos.z);

    private int xPlayerLocation => (int)Mathf.Floor(player.transform.position.x / PlaneOffset) * PlaneOffset;
    private int zPlayerLocation => (int)Mathf.Floor(player.transform.position.z / PlaneOffset) * PlaneOffset;

    Hashtable _tilePlane = new Hashtable();

    private void Update()
    {
        GenerateWorld();
    }

    private void GenerateWorld () {
        if(_startPos == Vector3.zero){
            for (int x = -Radius; x < Radius; x++)
            {
                for (int z = -Radius; z < Radius; z++)
                {
                    Vector3 pos = new Vector3((x * PlaneOffset + xPlayerLocation),
                    0,
                    (z * PlaneOffset + zPlayerLocation));

                    if (!_tilePlane.Contains(pos))
                    {
                        GameObject tile = Instantiate(plane, pos, Quaternion.identity);
                        _tilePlane.Add(pos, tile);
                    }
                }
            }
        }

        if (!HasPlayerMoved(xPlayerMove, zPlayerMove)) return;
        {
            for (int x = -Radius; x < Radius; x++)
            {
                for (int z = -Radius; z < Radius; z++)
                {
                    Vector3 pos = new Vector3((x * PlaneOffset + xPlayerLocation),
                        0,
                        (z * PlaneOffset + zPlayerLocation));

                    if (!_tilePlane.Contains(pos))
                    {
                        GameObject tile = Instantiate(plane, pos, Quaternion.identity);
                        _tilePlane.Add(pos, tile);
                    }
                }
            }
        }
    }

    private bool HasPlayerMoved(int playerX, int playerZ)
    {
        return Mathf.Abs(xPlayerMove) >= PlaneOffset || Mathf.Abs(zPlayerMove) >= PlaneOffset;
    }
}