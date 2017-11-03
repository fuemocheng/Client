using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResources : MonoBehaviour {

    private int m_nCount = 0;
    void Start () {
        Invoke("Func0", 3.0f);
    }
	
	void Update () {
        Debug.Log("Update: " + m_nCount);
        m_nCount++;
	}


    private GameObject m_goTempObj = null;
    private float m_fProgress = 0.0f;
    private void Loaded(Object _obj)
    {
        m_goTempObj = Instantiate(((GameObject)_obj));
        Debug.Log("Success!!");
    }
    private void Progress(float _progress)
    {
        m_fProgress = _progress;
    }

    private void Func0()
    {
        Debug.Log("Func0: " + m_nCount);
        //StartCoroutine(CacheManage.Instance.LoadObjectAsync("Prefabs/Capsule", Loaded, Progress));
        //StartCoroutine(CacheManage.Instance.LoadObjectCoroutinue("Prefabs/Capsule", Loaded));

        //OBJPool.Instance.OnGetObj("Capsule", "Prefabs/");
    }
}
