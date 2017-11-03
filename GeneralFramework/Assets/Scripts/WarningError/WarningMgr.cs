using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMgr : MonoBehaviour {

    public static List<WarningModel> m_listWarnings = new List<WarningModel>();

    [SerializeField]
    private WarningWindow m_warningWindow;
	
	void Update () {
        if (m_listWarnings.Count > 0)
        {
            WarningModel warning = m_listWarnings[0];
            m_listWarnings.RemoveAt(0);
            m_warningWindow.Active(warning);
        }
	}
}
