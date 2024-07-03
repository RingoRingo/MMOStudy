using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗口UI管理器
/// </summary>
public class WindowUIMgr : Singleton<WindowUIMgr>
{
    private Dictionary<WindowUIType, UIWindowBase> m_DicWindow = new Dictionary<WindowUIType, UIWindowBase>();

    /// <summary>
    /// 已经打开的窗口数量
    /// </summary>
    public int OpenWindowCount {
        get {
            return m_DicWindow.Count; 
        }
    }


    #region LoadWindow 打开窗口
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="type">窗口类型</param>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameObject OpenWindow(WindowUIType type)
    {
        if (type==WindowUIType.None)
        {
            return null;
        }
        GameObject obj = null;

        ///如果窗口不存在，则进行加载处理
        if (!m_DicWindow.ContainsKey(type))
        {
            ///如果枚举的名称要和预设的名称对应
            /////switch (type)
            //{
            //    case WindowUIType.LogOn:
            //        obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindows, "LogOnWindow", cache: true);
            //        break;
            //    case WindowUIType.Register:
            //        obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindows, "RegisterWindow", cache: true);
            //        break;
            //}
            obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindows, string.Format("{0}Window", type.ToString()), cache: true);
            if (obj == null)
            {
                return null;
            }
            UIWindowBase windowBase = obj.GetComponent<UIWindowBase>();
            if (windowBase == null)
            {
                return null;
            }
            m_DicWindow.Add(type, windowBase);

            windowBase.CurrectUIType = type;

            Transform transParent = null;

            switch (windowBase.containerType)
            {
                case WindowUIContainerType.TopLeft:
                    break;
                case WindowUIContainerType.TopRight:
                    break;
                case WindowUIContainerType.BottomLeft:
                    break;
                case WindowUIContainerType.BottomRight:
                    break;
                case WindowUIContainerType.Center:
                    transParent = SceneUIMgr.Instance.CurrectUIScene.container_Center;
                    break;
            }


            obj.transform.parent = transParent;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;

            obj.SetActive(false);
            StartShowWindow(windowBase, true);
        }
        else {
            obj = m_DicWindow[type].gameObject;
        }
        

        

       
       
        //层级管理
        LayerUIMgr.Instance.SetLayer(obj);

        
       
        return obj;
    }
    #endregion

    #region CloseWindow 关闭窗口
    /// <summary>
    /// 关闭窗口
    /// </summary>
    /// <param name="type"></param>
    public void CloseWindow(WindowUIType type)
    {
        if (m_DicWindow.ContainsKey(type))
        {
            StartShowWindow(m_DicWindow[type], false);
        }
    }
    #endregion



    #region StartShowWindow 开始打开窗口
    /// <summary>
    /// 开始打开窗口
    /// </summary>
    /// <param name="windowBase"></param>
    /// <param name="isOpen">是否打开</param>
    private void StartShowWindow(UIWindowBase windowBase, bool isOpen)
    {
        switch (windowBase.showStyle)
        {
            case WindowShowStyle.Normal:
                ShowNormal(windowBase, isOpen);
                break;
            case WindowShowStyle.CenterToBig:
                ShowCenterToBig(windowBase, isOpen);
                break;
            case WindowShowStyle.FromTop:
                ShowFromDir(windowBase, 0, isOpen);
                break;
            case WindowShowStyle.FromDown:
                ShowFromDir(windowBase, 1, isOpen);
                break;
            case WindowShowStyle.FromLeft:
                ShowFromDir(windowBase, 2, isOpen);
                break;
            case WindowShowStyle.FromRight:
                ShowFromDir(windowBase, 3, isOpen);
                break;
        }
    }
    #endregion
    

    #region 各种打开效果
    private void ShowNormal(UIWindowBase windowBase, bool isOpen)
    {
        if (isOpen)
        {
            windowBase.gameObject.SetActive(true);
        }
        else {
            DestroyWindow(windowBase);
        }
    }
    /// <summary>
    /// 从中间变大
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="isOpen"></param>
    private void ShowCenterToBig(UIWindowBase windowBase, bool isOpen) {
        if (isOpen)
        {
            windowBase.gameObject.SetActive(true);
            windowBase.gameObject.transform.localScale = Vector3.zero;
            windowBase.gameObject.transform.DOScale(Vector3.one, windowBase.duration);
        }
        else {
            //Debug.Log(windowBase.gameObject.name);
            windowBase.gameObject.transform.DOScale(Vector3.zero, windowBase.duration).SetEase(GlobalInit.Instance.UIAnimationCurve).OnComplete(() => { 
                DestroyWindow(windowBase);
            });
        }
        
    }
    /// <summary>
    /// 从不同的方向加载
    /// </summary>
    /// <param name="windowBase"></param>
    /// <param name="dirType">0=从上，1=从下，2=从左，3=从右</param>
    /// <param name="isOpen"></param>
    private void ShowFromDir(UIWindowBase windowBase,int dirType,bool isOpen) {
        if (isOpen)
        {
            windowBase.gameObject.SetActive(true);
            switch (dirType)
            {
                case 0:
                    windowBase.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 1000);
                    windowBase.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, windowBase.duration);
                    break;
                case 1:
                    windowBase.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -1000);
                    windowBase.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, windowBase.duration);
                    break;
                case 2:
                    windowBase.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, 0);
                    windowBase.gameObject.GetComponent<RectTransform>().DOAnchorPosX(0, windowBase.duration);
                    break;
                case 3:
                    windowBase.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(1000, 0);
                    windowBase.gameObject.GetComponent<RectTransform>().DOAnchorPosX(0, windowBase.duration);
                    break;
            }
        }
        else
        {
            switch (dirType)
            {
                case 0:
                    windowBase.gameObject.GetComponent<RectTransform>().DOAnchorPosY(1000, windowBase.duration).SetEase(GlobalInit.Instance.UIAnimationCurve).OnComplete(() => {
                        DestroyWindow(windowBase);
                    });
                    break;
                case 1:
                    windowBase.gameObject.GetComponent<RectTransform>().DOAnchorPosY(-1000, windowBase.duration).SetEase(GlobalInit.Instance.UIAnimationCurve).OnComplete(() => {
                        DestroyWindow(windowBase);
                    });
                    break;
                case 2:
                    windowBase.gameObject.GetComponent<RectTransform>().DOAnchorPosX(-1000, windowBase.duration).SetEase(GlobalInit.Instance.UIAnimationCurve).OnComplete(() => {
                        DestroyWindow(windowBase);
                    });
                    break;
                case 3:
                    windowBase.gameObject.GetComponent<RectTransform>().DOAnchorPosX(1000, windowBase.duration).SetEase(GlobalInit.Instance.UIAnimationCurve).OnComplete(() => {
                        DestroyWindow(windowBase);
                    });
                    break;
            }

            
        }
    }
    #endregion

   


    #region DestroyWindow 销毁窗口
    /// <summary>
    /// 销毁窗口
    /// </summary>
    /// <param name="obj"></param>
    private void DestroyWindow(UIWindowBase windowBase)
    {
        m_DicWindow.Remove(windowBase.CurrectUIType);
        Object.Destroy(windowBase.gameObject);
       
    }
    #endregion

}
