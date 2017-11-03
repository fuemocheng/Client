using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CacheManage : UnitySingleton<CacheManage> {


    public override void Init()
    {
    }

    void Awake()
    {
    }

    void Start()
    {
    }


    /// <summary>
    /// 分别在几帧中处理，实际Load是在一帧中完成，意义不大
    /// </summary>
    public IEnumerator LoadObjectCoroutinue(string path, Action<UnityEngine.Object> _loaded)
    {
        if (path == null)
        {
            Debug.LogError("Resources Path Is Not Exist : " + path);
            yield break;
        }

        UnityEngine.Object _object = null;

        while (true)
        {
            yield return null;

            try
            {
                _object = Resources.Load(path);
                if (null == _object)
                {
                    Debug.LogError("Resources Coroutinue Load Failed : " + path);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }

            yield return null;

            if (null != _object)
            {
                if (null != _loaded)
                {
                    _loaded(_object);
                }
            }

            yield break;
        }
    }


    public IEnumerator LoadObjectAsync(string path, Action<UnityEngine.Object> _loaded, Action<float> _progress = null)
    {
        if (path == "")
        {
            Debug.Log("Resources Path Is Exist!");
            yield break;
        }

        ResourceRequest _resRequest = Resources.LoadAsync(path);

        while (false == _resRequest.isDone)
        {
            if (null != _progress)
            {
                _progress(_resRequest.progress);
            }
            yield return null;
        }

        if (null != _resRequest.asset)
        {
            if (null != _loaded)
            {
                _loaded(_resRequest.asset);
            }
        }
        else
        {
            Debug.LogError("Resources Async Load Failed : " + path);
        }
    }


}
