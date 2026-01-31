// SceneInteractPoint.cs
using UnityEngine;
using TMPro;

public class SceneInteractPoint : MonoBehaviour
{
    [Header("显示的文字")]
    public string message = "获得金币！";

    [Header("浮动文字预制体")]
    public GameObject floatingTextPrefab; // 拖入刚做的 Prefab

    [Header("偏移")]
    public Vector3 textOffset = new Vector3(0, 0.5f, 0); // 在物体上方 0.5 单位显示

    // 被点击时调用
    public void Trigger()
    {
        if (floatingTextPrefab == null) return;

        Vector3 spawnPos = transform.position + textOffset;
        spawnPos.z = 0; // 确保 Z=0

        // 实例化
        GameObject newTextObj = Instantiate(floatingTextPrefab, spawnPos, Quaternion.identity);

        // 关键：找到 TMP 文字组件并设置内容
        TMP_Text tmpText = newTextObj.GetComponent<TMP_Text>();
        if (tmpText != null)
        {
            tmpText.text = message; // 使用你填写的 message
        }
        else
        {
            Debug.LogError("FloatingText 预制体缺少 TMP_Text 组件！");
        }
    }
}