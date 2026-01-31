using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCycleSwitcher : MonoBehaviour
{
    // 主房间顺序：左 → 右
    private string[] mainScenes = { "门", "阳台", "床", "厨房" };

    // 静态变量：记录从哪个主房间进入天花板
    public static string lastMainScene = "门";

    // ===== 主房间：左右切换 =====
    public void SwitchRight()
    {
        string current = SceneManager.GetActiveScene().name;
        int index = GetIndex(mainScenes, current);
        if (index == -1) return;
        int next = (index + 1) % mainScenes.Length;
        SceneManager.LoadScene(mainScenes[next]);
    }

    public void SwitchLeft()
    {
        string current = SceneManager.GetActiveScene().name;
        int index = GetIndex(mainScenes, current);
        if (index == -1) return;
        int prev = (index - 1 + mainScenes.Length) % mainScenes.Length;
        SceneManager.LoadScene(mainScenes[prev]);
    }

    // ===== 主房间：向上进入天花板 =====
    public void SwitchUp()
    {
        string current = SceneManager.GetActiveScene().name;
        if (IsMainScene(current))
        {
            lastMainScene = current;
            SceneManager.LoadScene("天花板");
        }
    }

    // ===== 天花板：向下返回（备用）=====
    public void SwitchDownFromCeiling()
    {
        // 兜底：返回记录的房间
        string target = !string.IsNullOrEmpty(lastMainScene) ? lastMainScene : "门";
        SceneManager.LoadScene(target);
    }

    // ===== 天花板：四个方向跳转 =====
    public void CeilingGoTo_门() { SceneManager.LoadScene("门"); }
    public void CeilingGoTo_阳台() { SceneManager.LoadScene("阳台"); }
    public void CeilingGoTo_床() { SceneManager.LoadScene("床"); }
    public void CeilingGoTo_厨房() { SceneManager.LoadScene("厨房"); }

    // 工具方法
    private bool IsMainScene(string sceneName)
    {
        return GetIndex(mainScenes, sceneName) != -1;
    }

    private int GetIndex(string[] arr, string name)
    {
        for (int i = 0; i < arr.Length; i++)
            if (arr[i] == name) return i;
        return -1;
    }
}