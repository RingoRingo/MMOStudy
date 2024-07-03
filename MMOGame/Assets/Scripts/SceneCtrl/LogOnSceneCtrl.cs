using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOnSceneCtrl : MonoBehaviour
{
    GameObject obj;
    void Awake() {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIMgr.SceneUIType.LogOn);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Destroy(obj);
        //}
        //else if (Input.GetKeyDown(KeyCode.L))
        //{
        //    obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene, "UIRoot_LogOnScene", cache: true);
        //}
    }
}
