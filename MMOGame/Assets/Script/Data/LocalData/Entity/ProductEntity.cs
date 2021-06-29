using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductEntity : AbstractEntity
{
    /// <summary>
    /// 商品编号
    /// </summary>
    public int Id {
        get;set;
    }

    /// <summary>
    /// 商品名称
    /// </summary>
    public string Name {
        get;set;
    }

    /// <summary>
    /// 价格
    /// </summary>
    public float Price {
        get;set;
    }
    /// <summary>
    /// 商品图片
    /// </summary>
    public string PicName {

        get;set;
    }
    /// <summary>
    /// 描述
    /// </summary>
    public string Desc {
        get;set;
    }
}
