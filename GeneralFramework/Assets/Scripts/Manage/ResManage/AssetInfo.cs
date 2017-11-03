using System.Collections;
using UnityEngine;
using System;

public class AssetInfo {

    //资源对象
    private UnityEngine.Object _object;
    //资源类型
    public Type AssetType { get; set; }
    //资源路径
    public string Path { get; set; }
    //引用次数
    public int RefCount { get; set; }

    public bool IsLoaded
    {
        get
        {
            return null != _object;
        }
    }

    public UnityEngine.Object AssetObject
    {
        get
        {
            if (null == _object)
            {
                ResourcesLoad();
            }
            return _object;
        }
    }

    /// <summary>
    /// 加载
    /// </summary>
    private void ResourcesLoad()
    {
        try
        {
            _object = Resources.Load(Path);
            if (null == _object)
            {
                Debug.Log("Resources Load Failed : " + Path);
            }
        }
        catch(Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    /// <summary>
    /// 协程加载
    /// </summary>
    /// <param name="_loaded"> 委托 </param>
    /// <returns></returns>
    public IEnumerator LoadObjectCoroutinue(Action<UnityEngine.Object> _loaded)
    {
        while (true)
        {
            yield return null;
            if (null == _object)
            {
                ResourcesLoad();
                yield return null;
            }
            else
            {
                if (null != _loaded)
                {
                    _loaded(_object);
                }
            }
            yield break;
        }
    }

    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="_loaded"> 委托 </param>
    /// <param name="_progress"> 委托 </param>
    /// <returns></returns>
    public IEnumerator LoadObjectAsync(Action<UnityEngine.Object> _loaded = null, Action<float> _progress = null)
    {
        if (null != _object)
        {
            if (null != _loaded)
            {
                _loaded(_object);
            }
            yield break;
        }

        ResourceRequest _resRequest = Resources.LoadAsync(Path);

        while (false == _resRequest.isDone)
        {
            if (null != _progress)
            {
                _progress(_resRequest.progress);
            }
            yield return null;
        }

        _object = _resRequest.asset;

        if (null != _loaded)
        {
            _loaded(_object);
        }

        yield return _resRequest;
    }
	
}
