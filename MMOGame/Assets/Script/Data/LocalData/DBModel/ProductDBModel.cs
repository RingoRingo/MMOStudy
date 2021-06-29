using ReadExcel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商品数据管理
/// </summary>
public class ProductDBModel:IDisposable
{
    private List<ProductEntity> lst;
    private Dictionary<int, ProductEntity> dic;

    private ProductDBModel() {
        lst = new List<ProductEntity>();
        dic = new Dictionary<int, ProductEntity>();


    }

    private static ProductDBModel instance;
    public static ProductDBModel Instance {
        get {
            if (instance==null)
            {
                instance = new ProductDBModel();
                instance.Load();
            }
            return instance;
        }
    }

    private void Load() {
        //读取文件
        //路径问题
        using (GameDataTableParser parse=new GameDataTableParser(@"D:\UnityProject\MMOGame\www\Data\Product.data"))
        {
            while (!parse.Eof)
            {
                ProductEntity entity = new ProductEntity();
                entity.Id = parse.GetFieldValue("Id").ToInt();
                entity.Name = parse.GetFieldValue("Name");
                entity.Price = parse.GetFieldValue("Price").ToFloat();
                entity.PicName = parse.GetFieldValue("PicName");
                entity.Desc = parse.GetFieldValue("Desc");
                lst.Add(entity);
                dic[entity.Id] = entity;

                parse.Next();
            }
        }


        //解压缩

    }

    public List<ProductEntity> GetList() {
        return lst;
    }

    /// <summary>
    /// 根据商品编号查询商品
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ProductEntity Get(int id) {
        if (dic.ContainsKey(id))
        {
            return dic[id];
        }
        return null;
    }

    public void Dispose()
    {
        lst.Clear();
        lst = null;
    }
}
