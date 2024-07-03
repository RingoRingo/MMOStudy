using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneLogOnCtrl : UISceneBase
{

    protected override void OnStart()
    {
        base.OnStart();
        GameObject obj = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }

    void Update() {
        //if (Input.GetKeyUp(KeyCode.O))
        //{
        //    GameObject obj = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
        //}

        //if (Input.GetKeyUp(KeyCode.C))
        //{
        //    WindowUIMgr.Instance.CloseWindow(WindowUIType.LogOn);
        //}
    }
}
