using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkHttp : MonoBehaviour
{
    #region 单例
    private static NetWorkHttp instance;
    public static NetWorkHttp Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("NetWorkHttp");
                DontDestroyOnLoad(obj);
                instance = obj.AddComponent<NetWorkHttp>();
            }

            return instance;
        }
    }
    #endregion

    private void Start()
    {
        m_CallBackArgs = new CallBackArgs();
    }

    /// <summary>
    /// Web请求回调
    /// </summary>
    private Action<CallBackArgs> m_CallBack;
    /// <summary>
    /// Web请求数据
    /// </summary>
    private CallBackArgs m_CallBackArgs;

    public void SendData(string url, Action<CallBackArgs> callBack, bool isPost = false, string json = "")
    {
        m_CallBack = callBack;

        if (!isPost)
        {
            GetUrl(url);
        }
        else
        {
            PostUrl(url, json);
        }
    }

    #region GetUrl Get请求
    private void GetUrl(string url)
    {
        WWW data = new WWW(url);
        StartCoroutine(Get(data));
    }
    private IEnumerator Get(WWW data)
    {
        yield return data;
        if (string.IsNullOrEmpty(data.error))
        {
            if (data.text==null)
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.isError = true;
                    m_CallBackArgs.Error = "为请求到数据";
                    m_CallBack(m_CallBackArgs);
                }
            }
            else
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.isError = false;
                    m_CallBackArgs.Json = data.text;
                    m_CallBack(m_CallBackArgs);
                }
            }
        }
        else
        {
            
        }
    }
    #endregion
    
    

    private void PostUrl(string url,string Json) { 
    
    }
    /// <summary>
    /// Web请求回调
    /// </summary>
    public class CallBackArgs {
        /// <summary>
        /// 是否报错
        /// </summary>
        public bool isError;
        /// <summary>
        /// 错误原因
        /// </summary>
        public string Error;
        /// <summary>
        /// Json数据
        /// </summary>
        public string Json;
    }

}
