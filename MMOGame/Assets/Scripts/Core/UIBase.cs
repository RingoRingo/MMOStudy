using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 所有UI的基类
/// </summary>
public class UIBase : MonoBehaviour
{

    
    void Awake()
    { 
        OnAwake();
    }


    // Start is called before the first frame update
    void Start()
    {
        Button[] BtnArr = GetComponentsInChildren<Button>(true);

        for (int i = 0; i < BtnArr.Length; i++)
        {
            int index = i;
            BtnArr[index].onClick.AddListener(() => {
                BtnClick(BtnArr[index].gameObject);
            });
        }

        OnStart();
    }

    private void OnDestroy() {
        BeforeOnDestroy();
    }

    private void BtnClick(GameObject go) {
        OnBtnClick(go);
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void BeforeOnDestroy() { }
    protected virtual void OnBtnClick(GameObject go) { }
}
