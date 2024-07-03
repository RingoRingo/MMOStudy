using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ResourcesMgr:Singleton<ResourcesMgr>
{
    #region 资源类型 ResourceType
    /// <summary>
    /// 资源类型
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// 场景UI
        /// </summary>
        UIScene,
        /// <summary>
        /// 窗口
        /// </summary>
        UIWindows,
        /// <summary>
        /// 角色
        /// </summary>
        Role,
        /// <summary>
        /// 特效
        /// </summary>
        Effect
    }
    #endregion

    /// <summary>
    /// 预设的列标配
    /// </summary>
    private Hashtable m_PrefabTable;

    public ResourcesMgr() { 
        m_PrefabTable= new Hashtable();
    }
    #region Load 加载资源
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="type">资源类型</param>
    /// <param name="path">短路径</param>
    /// <param name="cache">是否放入缓存</param>
    /// <returns>预设克隆体</returns>
    public GameObject Load(ResourceType type, string path, bool cache = false)
    {
        
        GameObject obj = null;
        if (m_PrefabTable.Contains(path))
        {
            Debug.Log("资源从缓存中加载");
            obj = m_PrefabTable[path] as GameObject;
        }
        else {
            StringBuilder sbr = new StringBuilder();
            switch (type)
            {
                case ResourceType.UIScene:
                    sbr.Append("UIPrefabs/UIScene/");
                    break;
                case ResourceType.UIWindows:
                    sbr.Append("UIPrefabs/UIWindows/");
                    break;
                case ResourceType.Role:
                    sbr.Append("RolePrefab/");
                    break;
                case ResourceType.Effect:
                    sbr.Append("EffectPrefab/");
                    break;

            }
            sbr.Append(path);
            Debug.Log(sbr.ToString());

            obj = Resources.Load<GameObject>(sbr.ToString());
            if (cache)
            {
                m_PrefabTable.Add(path,obj);
            }
        }
        return GameObject.Instantiate(obj);
    }
    #endregion

    #region Dispose 释放资源
    /// <summary>
    /// 释放资源
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();

        m_PrefabTable.Clear();

        //把未使用的资源进行释放
        Resources.UnloadUnusedAssets();
    }
    #endregion


}
