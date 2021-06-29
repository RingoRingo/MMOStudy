using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtil
{
    /// <summary>
    /// ��չ����
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ToInt(this string str) {
        int temp = 0;
        int.TryParse(str, out temp);
        return temp;
    }
    /// <summary>
    /// ��stringת��ΪFloat
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ToFloat(this string str)
    {
        float temp = 0;
        float.TryParse(str, out temp);
        return temp;
    }
}
