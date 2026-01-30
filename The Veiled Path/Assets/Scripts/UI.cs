using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject settingUI;
    public GameObject producerUI;
    public GameObject musicUI;
    //用于打开各种界面
    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }
    //开始游戏的代码
    public void SettingButton()
    {
        settingUI.SetActive(true);
    }
    //打开设置界面
    public void ProducerButton()
    {
        producerUI.SetActive(true);
    }
    //打开创作者界面
    public void ExitSetting()
    {
        settingUI.SetActive(false);
        musicUI.SetActive(false);
    }
    public void ExitProducer()
    {
        producerUI.SetActive(false);
    }
    //离开界面
    public void Music()
    {
        musicUI.SetActive(true);
    }
    //打开音乐设置
}