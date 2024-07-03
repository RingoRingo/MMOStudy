using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景UI管理器
/// </summary>
public class SceneUIMgr:Singleton<SceneUIMgr>
{
    /// <summary>
    /// 场景UI类型
    /// </summary>
    public enum SceneUIType {
        /// <summary>
        /// 登录
        /// </summary>
        LogOn,
        /// <summary>
        /// 加载
        /// </summary>
        Loading,
        /// <summary>
        /// 主城
        /// </summary>
        MainCity
    }
    /// <summary>
    /// 当前场景UI
    /// </summary>
    public UISceneBase CurrectUIScene;

    #region LoadScenUI 加载场景UI
    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="type"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject LoadSceneUI(SceneUIType type)
    {
        GameObject obj = null;

        switch (type)
        {
            case SceneUIType.LogOn:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene, "UIRoot_LogOnScene");
                CurrectUIScene = obj.GetComponent<UISceneLogOnCtrl>();
                break;
            case SceneUIType.Loading:

                break;
            case SceneUIType.MainCity:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene, "UIRoot_City");
                CurrectUIScene = obj.GetComponent<UISceneCityCtrl>();
                break;
        }
        return null;
    }
    #endregion

}
