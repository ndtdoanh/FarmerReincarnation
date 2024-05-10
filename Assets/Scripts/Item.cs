using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string name;
    public bool stackable;
    public Sprite icon;

}