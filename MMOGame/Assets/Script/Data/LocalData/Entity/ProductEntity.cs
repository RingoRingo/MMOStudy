using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductEntity : AbstractEntity
{
    /// <summary>
    /// ��Ʒ���
    /// </summary>
    public int Id {
        get;set;
    }

    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public string Name {
        get;set;
    }

    /// <summary>
    /// �۸�
    /// </summary>
    public float Price {
        get;set;
    }
    /// <summary>
    /// ��ƷͼƬ
    /// </summary>
    public string PicName {

        get;set;
    }
    /// <summary>
    /// ����
    /// </summary>
    public string Desc {
        get;set;
    }
}
