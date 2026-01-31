// SessionItemManager.cs
using System.Collections.Generic;
using UnityEngine;

public class SessionItemManager : MonoBehaviour
{
    private static SessionItemManager _instance;
    public static SessionItemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("SessionItemManager");
                _instance = obj.AddComponent<SessionItemManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    // 记录本局游戏中已解锁的物品ID
    private HashSet<string> unlockedItems = new HashSet<string>();

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsUnlocked(string id)
    {
        return unlockedItems.Contains(id);
    }

    public void Unlock(string id)
    {
        unlockedItems.Add(id);
        Debug.Log($"🔓 会话内解锁: {id}");
    }

    // 可选：手动重置（用于调试）
    public void ResetForNewGame()
    {
        unlockedItems.Clear();
    }
}