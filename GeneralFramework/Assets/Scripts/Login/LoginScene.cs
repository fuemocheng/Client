using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScene : MonoBehaviour {

    #region 登录面板部分
    [SerializeField]
    private InputField m_accoutInput;
    [SerializeField]
    private InputField m_passwordInput;
    [SerializeField]
    private Button m_btnLogin;
    [SerializeField]
    private Button m_btnOpenRegPanel;
    #endregion

    #region 注册面板部分
    [SerializeField]
    private GameObject m_panelRegister;
    [SerializeField]
    private InputField m_regAccountInput;
    [SerializeField]
    private InputField m_regPwInput;
    [SerializeField]
    private InputField m_regPwInput2;
    [SerializeField]
    private Button m_btnRegister;
    #endregion

    public void OnLogin()
    {
        if (m_accoutInput.text.Length == 0 || m_accoutInput.text.Length > 8)
        {
            WarningMgr.m_listWarnings.Add(new WarningModel("账户名不合法"));
            Debug.Log("账户名不合法");
            return;
        }

        if (m_passwordInput.text.Length == 0 || m_passwordInput.text.Length > 8)
        {
            WarningMgr.m_listWarnings.Add(new WarningModel("密码不合法"));
            Debug.Log("密码不合法");
            return;
        }

        //通过验证 申请登录


    }

    public void OnRegister()
    {
        if (m_regAccountInput.text.Length == 0 || m_regAccountInput.text.Length > 8)
        {
            WarningMgr.m_listWarnings.Add(new WarningModel("账户名不合法"));
            Debug.Log("账号不合法");
            return;
        }

        if (m_regPwInput.text.Length == 0 || m_regPwInput.text.Length > 8)
        {
            WarningMgr.m_listWarnings.Add(new WarningModel("密码不合法"));
            Debug.Log("密码不合法");
            return;
        }

        if (false == m_regPwInput2.text.Equals(m_regPwInput.text))
        {
            WarningMgr.m_listWarnings.Add(new WarningModel("两次输入密码不一致"));
            Debug.Log("两次输入密码不一致");
            return;
        }

        //通过验证，申请注册，并关闭注册面板
    }



    public void OpenPanelRegister()
    {
        m_panelRegister.SetActive(true);
    }

    public void HidePanelRegister()
    {
        m_panelRegister.SetActive(false);
    }
}
