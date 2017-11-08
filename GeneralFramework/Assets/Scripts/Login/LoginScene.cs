using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameProtocol;
using GameProtocol.dto;

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

    void Awake()
    {
        
    }

    void Start()
    {
        ReadLastinfo();
    }

    void Update()
    {
        
    }

    private void ReadLastinfo()
    {
        m_accoutInput.text =  PlayerPrefs.GetString("Accout", "youname");
        m_passwordInput.text = PlayerPrefs.GetString("Password", "0123456");
    }

    private void SaveLastInfo()
    {
        PlayerPrefs.SetString("Accout", m_accoutInput.text);
        PlayerPrefs.SetString("Password", m_passwordInput.text);
    }

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
        AccoutInfoDTO accoutInfoDTO = new AccoutInfoDTO();
        accoutInfoDTO.accout = m_accoutInput.text;
        accoutInfoDTO.password = m_passwordInput.text;

        NetIO.Instance.Write(Protocol.TYPE_LOGIN, 0, LoginProtocol.LOGIN_CRES, accoutInfoDTO);
        m_btnLogin.interactable = false;

        SaveLastInfo();
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
