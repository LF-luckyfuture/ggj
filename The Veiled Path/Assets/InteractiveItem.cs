// InteractiveItem.cs
using System.Collections;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    [Header("物品类型")]
    public string itemName = "花朵";

    [Header("移动设置")]
    public float moveDuration = 0.6f;

    private bool isInInventory = false;
    private Vector3 originalScenePosition; // 记录它在场景中的原始位置

    void Start()
    {
        // 记录初始位置（假设一开始在场景中）
        originalScenePosition = transform.position;
    }

    // Unity 内置点击方法（需带 Collider2D）
    void OnMouseDown()
    {
        if (isInInventory)
        {
            // 从物品栏 → 放置到目标点
            PlaceItem();
        }
        else
        {
            // 从场景 → 拾取到物品栏
            PickUpItem();
        }
    }

    void PickUpItem()
    {
        InventoryBar inventory = InventoryBar.Instance;
        if (inventory == null)
        {
            Debug.LogError("❌ 未找到 InventoryBar！");
            return;
        }

        if (inventory.AddItem(this))
        {
            isInInventory = true;
            Debug.Log($"📥 {itemName} 已拾取到物品栏");
        }
    }

    public void PlaceItem()
    {
        // 你可以在这里指定“放置目标”
        // 方案1：固定位置（如 transform.parent 的某个子物体）
        // 方案2：最近的交互点
        // 方案3：鼠标点击位置（但你要求“可选定位置”，我们用一个公共方法）

        Vector3 targetPosition = GetPlacementTarget();

        StartCoroutine(MoveToPosition(targetPosition, () =>
        {
            isInInventory = false;
            InventoryBar.Instance?.RemoveItem(this);
            Debug.Log($"📤 {itemName} 已放置到场景");
        }));
    }

    // 你可以重写这个方法来决定放哪里
    protected virtual Vector3 GetPlacementTarget()
    {
        // 默认：放回原始位置
        return originalScenePosition;
    }

    public IEnumerator MoveToPosition(Vector3 target, System.Action onComplete = null)
    {
        Vector3 startPos = transform.position;
        target.z = 0;
        float elapsed = 0;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            transform.position = Vector3.Lerp(startPos, target, t);
            yield return null;
        }

        transform.position = target;
        onComplete?.Invoke();
    }
}