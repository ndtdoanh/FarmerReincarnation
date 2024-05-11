using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] TileBase tile;
    public Vector3Int markedCellPostion;
    Vector3Int oldCellPosition;

    private void Update()
    {
        targetTilemap.SetTile(oldCellPosition, null);
        targetTilemap.SetTile(markedCellPostion, tile);
        oldCellPosition = markedCellPostion;

    }
}
