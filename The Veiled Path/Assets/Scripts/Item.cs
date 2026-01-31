using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public ItemName itemName;
    public void ItemClicked()
    {
        InventoryManager.Instance.AddItem(itemName);
        this.gameObject.SetActive(false);
    }
}
