using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;


public class CropTile
{
    public int growTimer;
    public int growStage;
    public Crop crop;
    public SpriteRenderer renderer;
    public float damage;
    public Vector3Int position;

    public bool Complete
    {
        get
        {
            if (crop == null) { return false; }
            return growTimer >= crop.timeToGrow;
        }
    }

    internal void Harvested()
    {
        growTimer = 0;
        growStage = 0;  
        crop = null;    
        renderer.gameObject.SetActive(false);
        damage = 0;
    }
}

public class CropsManager : TimeAgent
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;

    Tilemap TargetTilemap
    {
        get
        {
            if(targetTilemap == null)
            {
               targetTilemap = GameObject.Find("CropsTileMap").GetComponent<Tilemap>();
            }
            return targetTilemap;
        }
    }

    [SerializeField] GameObject cropsSpritePrefab;

    Dictionary<Vector2Int, CropTile> crops;

    private void Start()
    {
        crops = new Dictionary<Vector2Int, CropTile>();
        onTimeTick += Tick;
        Init();    
    }

    

    public void Tick()
    {
        if (TargetTilemap == null) { return; }

        foreach (CropTile cropTile in crops.Values)
        {
            if(cropTile.crop == null) { continue; }

            cropTile.damage += 0.02f;

            if (cropTile.damage>1f)
            {
                cropTile.Harvested();
                TargetTilemap.SetTile(cropTile.position, plowed);
                continue;
            }

            if (cropTile.Complete)
            {
                Debug.Log("Done growing");
                continue;
            }

            cropTile.growTimer += 1;

            if(cropTile.growTimer >= cropTile.crop.growthStateTime[cropTile.growStage])
            {
                cropTile.renderer.gameObject.SetActive(true);
                cropTile.renderer.sprite = cropTile.crop.sprites[cropTile.growStage];

                cropTile.growStage += 1;
            }

            
        }
    }

    public bool Check(Vector3Int position)
    {
        if (TargetTilemap == null) { return false; }

        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        if (TargetTilemap == null) { return; }

        if (crops.ContainsKey((Vector2Int)position))
        {
            return;    
        }

        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position, Crop toSeed)
    {
        if (TargetTilemap == null) { return; }

        TargetTilemap.SetTile(position, seeded);

        crops[(Vector2Int)position].crop = toSeed;
    }

    private void CreatePlowedTile(Vector3Int position)
    {
        if (TargetTilemap == null) { return; }

        CropTile crop = new CropTile();
        crops.Add((Vector2Int)position, crop);

        GameObject go = Instantiate(cropsSpritePrefab);
        go.transform.position = TargetTilemap.CellToWorld(position);
        go.transform.position -= Vector3.forward * 0.01f;
        go.SetActive(false);    
        crop.renderer = go.GetComponent<SpriteRenderer>();

        crop.position = position;

        TargetTilemap.SetTile(position, plowed);
    }

    internal void PickUp(Vector3Int gridPosition)
    {

        if(TargetTilemap == null) { return; }
        Vector2Int position = (Vector2Int)gridPosition; 
        if(crops.ContainsKey(position) == false) { return; }

        CropTile cropTile = crops[position];
        if (cropTile.Complete)
        {
            ItemSpawnManager.instance.SpawnItem(
                TargetTilemap.CellToWorld(gridPosition),
                cropTile.crop.yield,
                cropTile.crop.count
                );
            TargetTilemap.SetTile(gridPosition, plowed );
            cropTile.Harvested();

            
        }
    }
}
