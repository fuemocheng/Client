using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningWindow : MonoBehaviour {

    [SerializeField]
    private Text m_text;

    WarningResult m_delResult;

    public void Active(WarningModel warningModel)
    {
        m_text.text = warningModel.m_sValue;
        this.m_delResult = warningModel.m_delResult;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        if (null != m_delResult)
        {
            m_delResult();
        }
    }
}
