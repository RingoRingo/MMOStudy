using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySceneCtrl : MonoBehaviour
{

    void Awake() {
        SceneUIMgr.Instance.LoadSceneUI(SceneUIMgr.SceneUIType.MainCity);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
