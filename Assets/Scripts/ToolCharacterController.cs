using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolCharacterController : MonoBehaviour
{
    CharacterController2D character;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TileMapReadController tileMapReadController;

    private void Awake()
    {
        character = GetComponent<CharacterController2D>();  
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Marker();

        if(Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    private void Marker()
    {
        Vector3Int gridPosition =  tileMapReadController.GetGridPosition(Input.mousePosition, true);
        markerManager.markedCellPostion = gridPosition;
    }

    private void UseTool()
    {
        Vector2 position = rgbd2d.position + character.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}
