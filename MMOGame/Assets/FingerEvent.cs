using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerEvent : MonoBehaviour
{
    public static object Instance { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum ZoomType { 
        In,
        Out
    }

    public enum FingerDir { 
        Left,
        Right,
        Up,
        Down
    }
}


