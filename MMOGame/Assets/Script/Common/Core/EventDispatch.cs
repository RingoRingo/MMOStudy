using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �۲���ģʽ
/// </summary>
public class EventDispatch : Singleton<EventDispatch>
{
    /// <summary>
    /// ί��ԭ��
    /// </summary>
    public delegate void OnActionHandler(byte[] buffer);
    /// <summary>
    /// ί���ֵ�
    /// </summary>
    public Dictionary<ushort, List<OnActionHandler>> onHandlerDic = new Dictionary<ushort, List<OnActionHandler>>();

    /// <summary>
    /// ��Ӽ���
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
    /// �Ƴ�����
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
    /// �ɷ�Э��,֮�ɷ���������id����
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
