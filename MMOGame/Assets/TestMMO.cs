using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMMO : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Test item = new Test { Id = 1, Name = "aaa" };

        byte[] arr = null;
        using (MMO_MemoryStream ms=new MMO_MemoryStream())
        {
            ms.WriteInt(item.Id);
            ms.WriteUTF8String(item.Name);

            arr = ms.ToArray();
        }
        //if (arr!=null)
        //{
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        Debug.Log(string.Format("{0}={1}", i, arr[i]));
        //    }
        //}

        Test test2 = new Test();
        using (MMO_MemoryStream ms=new MMO_MemoryStream(arr)) {
            test2.Id = ms.ReadInt();
            test2.Name = ms.ReadUTF8String();
        }

        Debug.Log(string.Format("test2.Id={0}", test2.Id));
        Debug.Log(string.Format("test2.Name={0}", test2.Name));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

public class Test{
    public int Id;
    public string Name;
}
