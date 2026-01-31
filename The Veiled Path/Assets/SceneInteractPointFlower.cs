using System.Collections;
using TMPro;
using UnityEngine;

public class SceneInteractPointFlower : MonoBehaviour
{
    [Header("物品（初始在物品栏）")]
    public GameObject itemObject; // 指向物品栏中的 FlowerIcon

    [Header("移动设置")]
    public float moveDuration = 0.8f;

    [Header("浮动文字")]
    public string message = "放置花朵！";
    public GameObject floatingTextPrefab;
    public Vector3 textOffset = new Vector3(0, 0.5f, 0);

    private bool hasBeenTriggered = false;
    private Vector3 originalItemPosition; // 记住物品栏位置（用于重置）

    void Start()
    {
        if (itemObject != null)
        {
            // 记录初始位置（物品栏）
            originalItemPosition = itemObject.transform.position;
            // 确保物品初始在物品栏（每次重开都重置）
            itemObject.transform.position = originalItemPosition;
            EnableItem(true); // 确保可见
        }
    }

    void Update()
    {
        if (hasBeenTriggered) return;

        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = Camera.main;
            if (cam == null) return;

            Vector2 worldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(worldPos);

            if (hit != null && hit.gameObject == gameObject)
            {
                Trigger();
            }
        }
    }

    void Trigger()
    {
        hasBeenTriggered = true;

        // 显示文字
        if (floatingTextPrefab != null)
        {
            Vector3 pos = transform.position + textOffset;
            pos.z = 0;
            GameObject textObj = Instantiate(floatingTextPrefab, pos, Quaternion.identity);
            TMP_Text tmp = textObj.GetComponent<TMP_Text>();
            if (tmp != null) tmp.text = message;
        }

        // 开始移动：从物品栏 → 交互点
        if (itemObject != null)
        {
            StartCoroutine(MoveItemToInteractionPoint());
        }
    }

    IEnumerator MoveItemToInteractionPoint()
    {
        Vector3 startPos = itemObject.transform.position;
        Vector3 endPos = transform.position; // 交互点位置
        endPos.z = 0;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;
            itemObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        itemObject.transform.position = endPos;
        Debug.Log("✅ 物品已放置到交互点");
    }

    void EnableItem(bool enable)
    {
        SpriteRenderer sr = itemObject.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.enabled = enable;
        }
        else
        {
            itemObject.SetActive(enable);
        }
    }
}