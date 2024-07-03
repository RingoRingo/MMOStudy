using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主城UI控制器
/// </summary>
public class UISceneCityCtrl : UISceneBase
{
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);

        switch (go.name)
        {
            case "btnHead":
                OpenRoleInfo();
                break;
            default:
                break;
        }
    }

    private void OpenRoleInfo() {
        WindowUIMgr.Instance.OpenWindow(WindowUIType.RoleInfo);
    }
}
