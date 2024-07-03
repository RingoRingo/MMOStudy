using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 注册窗口UI控制器
/// </summary>
public class UIRegCtrl : UIWindowBase
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
    /// 确认密码
    /// </summary>
    [SerializeField]
    private InputField m_InputPwd2;
    /// <summary>
    /// 提示
    /// </summary>
    [SerializeField]
    private Text m_LblTip;
    #region OnBtnClick 重写基类OnBtnClick
    /// <summary>
    /// 重写基类OnBtnClick
    /// </summary>
    /// <param name="go"></param>
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnToLogOn":
                BtnToLogOn();
                break;
            case "RegisterBtn":
                Reg();
                break;
            default:
                break;
        }
    }
    #endregion

    /// <summary>
    /// 去登录窗口
    /// </summary>
    private void BtnToLogOn()
    {
        Close();
        NextOpenWindow = WindowUIType.LogOn;

        // WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }

    private void Reg() {
        string nickName = m_InputNickName.text.Trim();
        string pwd = m_InputPwd.text.Trim();
        string pwd2 = m_InputPwd2.text.Trim();

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

        if (string.IsNullOrEmpty(pwd2))
        {
            m_LblTip.text = "请输入确认密码";
            return;
        }

        if (pwd!= pwd2)
        {
            m_LblTip.text = "两次输入的密码不一致";
            return;
        }

        PlayerPrefs.SetString(GlobalInit.MMO_NICKNAME, nickName);
        PlayerPrefs.SetString(GlobalInit.MMO_PWD, pwd);

        SceneMgr.Instance.LoadToCity();
    }

}
