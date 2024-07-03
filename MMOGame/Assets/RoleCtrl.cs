using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnZoom() { 
        
    }

    private void OnPlayerClickGround() {
        if (Camera.current!=null)
        {
            Ray rayUI = Camera.current.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(rayUI,Mathf.Infinity,1<<LayerMask.NameToLayer("UI")))
            {
                Debug.Log("触碰到了UI层");
                return;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray,out hitInfo))
        {
            if (hitInfo.collider.gameObject.name.Equals("Ground",System.StringComparison.CurrentCultureIgnoreCase))
            {

            }
        }
    }
}
