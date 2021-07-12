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

    #region ����
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
    #region ��Ҫ����ʵ�ֵ����Ի򷽷�
    /// <summary>
    /// �����ļ�����
    /// </summary>
    protected abstract string FileName { get; }
    /// <summary>
    /// ����ʵ��
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected abstract P MakeEntity(GameDataTableParser parse);
    #endregion
    #region LoadData ��������
    /// <summary>
    /// ��������
    /// </summary>
    private void LoadData()
    {
        //��ȡ�ļ�
        //·������
        using (GameDataTableParser parse = new GameDataTableParser(string.Format(@"D:\UnityProject\MMOGame\www\Data\{0}", FileName)))
        {
            while (!parse.Eof)
            {
                //����ʵ��
                P p = MakeEntity(parse);
                m_List.Add(p);
                m_dic[p.Id] = p;
                parse.Next();
            }
        }
        //��ѹ��
    }
    #endregion
    #region ��ȡ����
    public List<P> GetList()
    {
        return m_List;
    }

    /// <summary>
    /// ���ݱ�Ż�ȡʵ��
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
