using System;
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
    bool show;

    private void Update()
    {
        if (show == false) { return; }
        targetTilemap.SetTile(oldCellPosition, null);
        targetTilemap.SetTile(markedCellPostion, tile);
        oldCellPosition = markedCellPostion;

    }

    internal void Show(bool selectable)
    {
        show = selectable;
        targetTilemap.gameObject.SetActive(show);
    }
}
