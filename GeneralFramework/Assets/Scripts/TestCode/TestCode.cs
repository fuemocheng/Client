using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestCode : MonoBehaviour {

    void Awake()
    {
        //CacheManage.Instance.Func0();
        //CDManage.Instance.Func0();
    }

    void Start()
    {

    }

    public void NextLevel(string str)
    {
        SceneManager.LoadScene(str);
    }
}
