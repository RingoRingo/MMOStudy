using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 登录窗口UI控制器
/// </summary>
public class UILogOnCtrl : UIWindowBase
{
    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private InputField m_InputNickName;
    /// <summary>
    /// 密码
    /// </summary>
    [SerializeField]
    private InputField m_InputPwd;
    /// <summary>
    /// 提示文字
    /// </summary>
    [SerializeField]
    private Text m_LblTip;

    /// <summary>
    /// 重写基类OnBtnClick
    /// </summary>
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);

        Debug.Log(go.name);

        switch (go.name)
        {
            case "LogBtn":
                LogOn();
                break;
            case "RegisterBtn":
                BtnToReg();
                break;
        }
    }

    /// <summary>
    /// 我要注册按钮方法
    /// </summary>
    private void BtnToReg() {
        this.Close();
        NextOpenWindow = WindowUIType.Register;


        // WindowUIMgr.Instance.OpenWindow(WindowUIType.Register);
    }

    private void LogOn() {
        string nickName = m_InputNickName.text.Trim();
        string pwd=m_InputPwd.text.Trim();

        if (string.IsNullOrEmpty(nickName))
        {
            m_LblTip.text = "请输入昵称";
            return;
        }

        if (string.IsNullOrEmpty(pwd))
        {
            m_LblTip.text = "请输入密码";
            return;
        }

        string oldNickName = PlayerPrefs.GetString(GlobalInit.MMO_NICKNAME);
        string oldPwd = PlayerPrefs.GetString(GlobalInit.MMO_PWD);

        if (oldNickName != nickName || oldPwd!=pwd)
        {
            m_LblTip.text = "账号或密码错误！";
            return;
        }

        SceneMgr.Instance.LoadToCity();
    }
}
