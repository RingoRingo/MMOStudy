using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 观察者模式
/// </summary>
public class EventDispatch : Singleton<EventDispatch>
{
    /// <summary>
    /// 委托原型
    /// </summary>
    public delegate void OnActionHandler(byte[] buffer);
    /// <summary>
    /// 委托字典
    /// </summary>
    public Dictionary<ushort, List<OnActionHandler>> onHandlerDic = new Dictionary<ushort, List<OnActionHandler>>();

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void AddEventListener(ushort protoCode, OnActionHandler handler) {
        if (onHandlerDic.ContainsKey(protoCode))
        {
            onHandlerDic[protoCode].Add(handler);
        }
        else {
            List<OnActionHandler> ListHandler = new List<OnActionHandler>();
            ListHandler.Add(handler);
            onHandlerDic.Add(protoCode, ListHandler);
        }
    }

    /// <summary>
    /// 移除监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void RemoveEventListener(ushort protoCode, OnActionHandler handler) {
        if (onHandlerDic.ContainsKey(protoCode))
        {
            List<OnActionHandler> ListHandler = onHandlerDic[protoCode];
            ListHandler.Remove(handler);
            if (onHandlerDic[protoCode].Count==0)
            {
                onHandlerDic.Remove(protoCode);
            }
        }
    }

    /// <summary>
    /// 派发协议,之派发给监听我id的人
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="handler"></param>
    public void Dispatch(ushort protoCode,byte[] buffer) {
        if (onHandlerDic.ContainsKey(protoCode))
        {
            List<OnActionHandler> ListHandler = onHandlerDic[protoCode];
            if (ListHandler!=null && ListHandler.Count>0)
            {
                for (int i = 0; i < ListHandler.Count; i++)
                {
                    if (ListHandler[i]!=null)
                    {
                        ListHandler[i](buffer);
                    }
                }
            }
        }
    }
}
