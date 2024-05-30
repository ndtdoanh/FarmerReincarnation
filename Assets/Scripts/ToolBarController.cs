using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ToolBarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 12;
    int selectedTool;

    public Action<int> onChange;

    public Item GetItem
    {
        get
        {
            return GameManager.instance.inventoryContainer.slots[selectedTool].item;
        }
    }

    internal void Set(int id)
    {
        selectedTool = id;
    }

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            int oldSelectedTool = selectedTool;
            if (delta > 0)
            {
                selectedTool = (selectedTool + 1) % toolbarSize;
            }
            else
            {
                selectedTool = (selectedTool - 1 + toolbarSize) % toolbarSize;
            }

            if (oldSelectedTool != selectedTool)
            {
                onChange?.Invoke(selectedTool);
            }
        }
    }
}
