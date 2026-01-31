// InventoryBar.cs （更新版）
using System.Collections.Generic;
using UnityEngine;

public class InventoryBar : MonoBehaviour
{
    public static InventoryBar Instance;

    [Header("配置")]
    public int maxSlots = 8;
    public float slotSpacing = 1.0f;

    private List<InteractiveItem> slots = new List<InteractiveItem>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Initialize();
    }

    void Initialize()
    {
        slots.Clear();
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(null);
        }
    }

    // 添加物品到第一个空位
    public bool AddItem(InteractiveItem item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                Vector3 pos = GetSlotWorldPosition(i);
                item.transform.SetParent(transform);
                item.StartCoroutine(item.MoveToPosition(pos));
                return true;
            }
        }
        return false;
    }

    // 从物品栏移除（当物品被放置出去时调用）
    public void RemoveItem(InteractiveItem item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == item)
            {
                slots[i] = null;
                item.transform.SetParent(null); // 脱离物品栏
                break;
            }
        }
    }

    Vector3 GetSlotWorldPosition(int index)
    {
        float totalWidth = (maxSlots - 1) * slotSpacing;
        float startX = -totalWidth / 2f;
        float x = startX + index * slotSpacing;
        return transform.position + new Vector3(x, 0, 0);
    }
}