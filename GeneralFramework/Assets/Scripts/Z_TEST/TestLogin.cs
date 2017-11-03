using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLogin : MonoBehaviour {

	void Start () {
        WarningModel warningModel = new WarningModel("发送失败",PrintMessage);
        WarningModel warningModel2 = new WarningModel("发送成功", PrintMessage2);

        WarningMgr.m_listWarnings.Add(warningModel2);
        WarningMgr.m_listWarnings.Add(warningModel);
    }
	
	void Update () {
		
	}

    public void PrintMessage()
    {
        Debug.Log("Confirm,Close!");
    }
    public void PrintMessage2()
    {
        Debug.Log("Confirm2,Close2!");
    }
}
