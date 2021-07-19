using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkHttp : MonoBehaviour
{
    #region ����
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
    /// Web����ص�
    /// </summary>
    private Action<CallBackArgs> m_CallBack;
    /// <summary>
    /// Web��������
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

    #region GetUrl Get����
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
                    m_CallBackArgs.Error = "Ϊ��������";
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
    /// Web����ص�
    /// </summary>
    public class CallBackArgs {
        /// <summary>
        /// �Ƿ񱨴�
        /// </summary>
        public bool isError;
        /// <summary>
        /// ����ԭ��
        /// </summary>
        public string Error;
        /// <summary>
        /// Json����
        /// </summary>
        public string Json;
    }

}
