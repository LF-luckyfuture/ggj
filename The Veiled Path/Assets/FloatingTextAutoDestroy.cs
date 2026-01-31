// FloatingTextAutoDestroy.cs
using UnityEngine;
using TMPro;

public class FloatingTextAutoDestroy : MonoBehaviour
{
    public float lifetime = 1f;
    public float moveSpeed = 1f; // 向上飘动速度
    public float fadeDuration = 3f;
    public float floatingtime = 0.6f;

    private TMP_Text textComponent;
    private float timer = 0f;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        if (textComponent == null)
            Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer < floatingtime)
        {
            // 向上移动
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        // 淡出
        if (timer > lifetime - fadeDuration)
        {
            float alpha = 1 - ((timer - (lifetime - fadeDuration)) / fadeDuration);
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, alpha);
        }

        // 销毁
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}