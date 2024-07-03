using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInit : MonoBehaviour
{
    #region 常量
    /// <summary>
    /// 昵称KEY
    /// </summary>
    public const string MMO_NICKNAME = "MMO_NICKNAME";
    /// <summary>
    /// 密码KEY
    /// </summary>
    public const string MMO_PWD = "MMO_PWD";
    #endregion

    public static GlobalInit Instance;
    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimationCurve;
    void Awake() { 

        Instance = this;
        DontDestroyOnLoad(this);
    }
}
