using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Loading场景UI控制器
/// </summary>
public class UISceneLoadingCtrl : UISceneBase
{
    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField]
    private Slider m_Progress;

    /// <summary>
    /// 进度条上的文本
    /// </summary>
    [SerializeField]
    private Text m_LblProgress;
    /// <summary>
    /// 设置进度条的值
    /// </summary>
    /// <param name="value"></param>
    public void SetProgressValue(float value) {
        m_Progress.value= value;
        Debug.Log(value);
        m_LblProgress.text = string.Format("{0}%", (int)(value * 100));
    }
}
