﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region SceneType场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    LogOn,
    City
}
#endregion

#region WindowUIType窗口类型
/// <summary>
/// 窗口类型
/// </summary>
public enum WindowUIType
{
    None,
    /// <summary>
    /// 登录窗口
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Register,
    /// <summary>
    /// 角色信息窗口
    /// </summary>
    RoleInfo,
}
#endregion

#region WindowUIContainerType UI容器类型
/// <summary>
/// UI容器类型
/// </summary>
public enum WindowUIContainerType
{
    /// <summary>
    /// 左上
    /// </summary>
    TopLeft,
    /// <summary>
    /// 右上
    /// </summary>
    TopRight,
    /// <summary>
    /// 左下
    /// </summary>
    BottomLeft,
    /// <summary>
    /// 右下
    /// </summary>
    BottomRight,
    /// <summary>
    /// 居中
    /// </summary>
    Center

}
#endregion

#region WindowShowStyle 窗口打开方式
/// <summary>
/// 窗口打开方式
/// </summary>
public enum WindowShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal,
    /// <summary>
    /// 从中间放大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上往下
    /// </summary>
    FromTop,
    /// <summary>
    /// 从下往上
    /// </summary>
    FromDown,
    /// <summary>
    /// 从左向右
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右向左
    /// </summary>
    FromRight
}
#endregion

#region RoleType
/// <summary>
/// 角色类型
/// </summary>
public enum RoleType
{
    None=0,
    /// <summary>
    /// 当前玩家
    /// </summary>
    MainPlayer = 1,
    /// <summary>
    /// 怪
    /// </summary>
    Monster = 2
}
#endregion

