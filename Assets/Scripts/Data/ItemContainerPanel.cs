using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerPanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.OnClick(inventory.slots[id]);
        Show();
    }
}
