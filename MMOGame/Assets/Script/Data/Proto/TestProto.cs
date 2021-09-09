using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ≤‚ ‘–≠“È
/// </summary>
public struct TestProto
{
    public ushort ProtoCode {
        get {
            return 1001;
        }
    }

    public int Id;
    public string Name;
    public int Type;
    public float Price;

    public byte[] ToArray() {

        using (MMO_MemoryStream ms=new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(Id);
            ms.WriteUTF8String(Name);
            ms.WriteInt(Type);
            ms.WriteDouble(Price);
        }
        return null;
    }


    public static TestProto GetProto(byte[] buffer) {
        TestProto proto = new TestProto();
        using (MMO_MemoryStream ms=new MMO_MemoryStream(buffer))
        {
            proto.Id = ms.ReadInt();
            proto.Name = ms.ReadUTF8String();
            proto.Type = ms.ReadInt();
            proto.Price = ms.ReadFloat();
        }
        return proto;
    }
}
