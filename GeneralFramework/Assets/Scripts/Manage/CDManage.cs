using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDManage : UnitySingleton<CDManage> {

    void Awake()
    {
        Debug.Log("CDManage Awake!");
    }

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void Func0()
    {
        Debug.Log("CDManage Func0");
    }
}
