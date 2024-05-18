using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trading : MonoBehaviour
{
    [SerializeField] GameObject storePanel;
    [SerializeField] GameObject inventoryPanel;

    Store store;

    Currency money;
    ItemStorePanel itemStorePanel;
    [SerializeField] ItemContainer playerInventory;

    [SerializeField] ItemPanel inventoryItemPanel;


    private void Awake()
    {
        money = GetComponent<Currency>();   
        itemStorePanel = storePanel.GetComponent<ItemStorePanel>(); 
    }

    public void BeginTrading(Store store)
    {
        this.store = store;
        itemStorePanel.SetInventory(store.storeContent);

        storePanel.SetActive(true);
        inventoryPanel.SetActive(true);
    }
    internal void BuyItem(int id)
    {
        Item itemtoBuy = store.storeContent.slots[id].item;
        int totalPrice = (int)(itemtoBuy.price * store.sellToPlayerMultip);
        if(money.Check(totalPrice) == true) 
        
        { 
            money.Decrease(totalPrice);
            playerInventory.Add(itemtoBuy);
            inventoryItemPanel.Show();  
        }
    }
    public void SellItem()
    {
        if (GameManager.instance.dragAndDropController.CheckForSale() == true)
        {
            ItemSlot itemToSell = GameManager.instance.dragAndDropController.itemSlot;
            int moneyGain = itemToSell.item.stackable == true ?
                (int)(itemToSell.item.price * itemToSell.count * store.buyFromPlayerMultip):
                (int)(itemToSell.item.price * store.buyFromPlayerMultip);
            money.Add(moneyGain);
            itemToSell.Clear();
            GameManager.instance.dragAndDropController.UpdateIcon();
        }
    }

    public void StopTradding()
    {
        store = null;
        storePanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }
}