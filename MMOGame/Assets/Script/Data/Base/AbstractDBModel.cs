using ReadExcel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDBModel<T,P> 
    where T:class, new()
    where P:AbstractEntity
{
    protected List<P> m_List;
    protected Dictionary<int, P> m_dic;

    public AbstractDBModel() {
        m_List = new List<P>();
        m_dic = new Dictionary<int, P>();

        LoadData();
    }

    #region 单例
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
                //instance.Load();
            }
            return instance;
        }
    }
    #endregion
    #region 需要子类实现的属性或方法
    /// <summary>
    /// 数据文件名称
    /// </summary>
    protected abstract string FileName { get; }
    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected abstract P MakeEntity(GameDataTableParser parse);
    #endregion
    #region LoadData 加载数据
    /// <summary>
    /// 加载数据
    /// </summary>
    private void LoadData()
    {
        //读取文件
        //路径问题
        using (GameDataTableParser parse = new GameDataTableParser(string.Format(@"D:\UnityProject\MMOGame\www\Data\{0}", FileName)))
        {
            while (!parse.Eof)
            {
                //创建实体
                P p = MakeEntity(parse);
                m_List.Add(p);
                m_dic[p.Id] = p;
                parse.Next();
            }
        }
        //解压缩
    }
    #endregion
    #region 获取集合
    public List<P> GetList()
    {
        return m_List;
    }

    /// <summary>
    /// 根据编号获取实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public P Get(int id)
    {
        if (m_dic.ContainsKey(id))
        {
            return m_dic[id];
        }
        return null;
    }
    #endregion

}
