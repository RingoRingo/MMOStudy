using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMMO2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<ProductEntity> lst = ProductDBModel.Instance.GetList();

        //for (int i = 0; i < lst.Count; i++)
        //{
        //    Debug.Log(lst[i].Name);
        //}

        ProductEntity entity = ProductDBModel.Instance.Get(5);

        if (entity!=null)
        {
            Debug.Log(entity.Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
