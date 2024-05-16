using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject panel;
    //[SerializeField] GameObject statusPanel;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject storePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(panel.activeInHierarchy == false)
            {
                Open();
            } else
            {
                Close();    
            }
        }
    }

    public void Open()
    {
        panel.SetActive(true);
        //tatusPanel.SetActive(true);
        toolbarPanel.SetActive(false);
        storePanel.SetActive(false);    
    }

    public void Close()
    {
        panel.SetActive(false);
        //statusPanel.SetActive(false);
        toolbarPanel.SetActive(true);
        storePanel.SetActive(false);

    }
}
