using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMMO3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //1.���ӵ�������
        NetWorkSocket.Instance.Connect("192.168.0.100", 2222);

        //2.����Ϣ
        using (MMO_MemoryStream ms=new MMO_MemoryStream())
        {
            ms.WriteUTF8String("nihaoa");

            NetWorkSocket.Instance.SendMsg(ms.ToArray());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
