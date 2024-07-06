using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtrl : MonoBehaviour
{
    /// <summary>
    /// 动画播放
    /// </summary>
    [SerializeField]
    private Animator m_Animator;

    /// <summary>
    /// 移动的目标点
    /// </summary>
    private Vector3 m_TargetPos = Vector3.zero;

    /// <summary>
    /// 控制器
    /// </summary>
    private CharacterController m_ChararcterController;
    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    private float m_Speed = 10f;
    /// <summary>
    /// 转身速度
    /// </summary>
    private float m_RotationSpeed = 0.2f;

    /// <summary>
    /// 转身的目标方向
    /// </summary>
    private Quaternion m_TargetQuaternion;
    // Start is called before the first frame update
    void Start()
    {
        m_ChararcterController=GetComponent<CharacterController>();

        if (CameraCtrl.Instance!=null)
        {
            CameraCtrl.Instance.Init();
        }

        m_Animator.SetBool("ToIdleNormal", true);
    }

    private void Reset() {
        m_Animator.SetBool("ToIdleNormal", false);
        m_Animator.SetBool("ToRun", false);
        m_Animator.SetBool("ToHurt", false);
        m_Animator.SetBool("ToDie", false);
        m_Animator.SetBool("ToIdleFight", false);
        m_Animator.SetInteger("ToPhyAttack", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_ChararcterController==null)
        {
            return;
        }

        //if (!m_ChararcterController.isGrounded)
        //{
        //    m_ChararcterController.Move((transform.position - new Vector3(0, -1000, 0)) - transform.position);
        //}

        AnimatorStateInfo info = m_Animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("Idle_Normal"))
        {
            m_Animator.SetInteger("CurrState", 1);
        }
        else if (info.IsName("Idle_Fight"))
        {
            m_Animator.SetInteger("CurrState", 2);
        }
        else if (info.IsName("Run"))
        {
            m_Animator.SetInteger("CurrState", 3);
        }
        else if (info.IsName("Hurt"))
        {
            m_Animator.SetInteger("CurrState", 4);
        }
        else if (info.IsName("Die"))
        {
            m_Animator.SetInteger("CurrState", 5);
        }
        else if (info.IsName("Select"))
        {
            m_Animator.SetInteger("CurrState", 6);
        }
        else if (info.IsName("XiuXian"))
        {
            m_Animator.SetInteger("CurrState", 7);
        }
        else if (info.IsName("Died"))
        {
            m_Animator.SetInteger("CurrState", 8);
        }
        else if (info.IsName("PhyAttack1"))
        {
            m_Animator.SetInteger("CurrState", 11);

            if (info.normalizedTime >=1)
            {
                Reset();
                m_Animator.SetBool("ToIdleNormal", true);
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Reset();
            m_Animator.SetBool("ToRun", true);
        }
        else if (Input.GetKeyUp(KeyCode.B)) {
            Reset();
            m_Animator.SetBool("ToIdleNormal", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            Reset();
            m_Animator.SetInteger("ToPhyAttack", 1);
        }
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
