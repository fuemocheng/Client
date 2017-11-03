using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManage : UnitySingleton<ResManage> {

    private Dictionary<string, AssetInfo> m_dicAssetInfo = null;

    public override void Init()
    {
        m_dicAssetInfo = new Dictionary<string, AssetInfo>();
    }


    #region Load & Instantiate Resources
    public UnityEngine.Object LoadInstance(string _path)
    {
        UnityEngine.Object _obj = Load(_path);
        return Instantiate(_obj);
    }

    public void LoadCoroutineInstance(string _path, Action<UnityEngine.Object> _loaded = null)
    {
        LoadCoroutine(_path, (_obj) => { Instantiate(_obj, _loaded); });
    }

    public void LoadAsyncInstance(string _path, Action<UnityEngine.Object> _loaded = null, Action<float> _progress = null)
    {
        LoadAsync(_path, (_obj) => { Instantiate(_obj, _loaded); }, _progress);
    }
    #endregion


    #region Load Resources 
    private UnityEngine.Object Load(string _path)
    {
        AssetInfo _assetInfo = GetAssetInfo(_path);
        if (null != _assetInfo)
        {
            return _assetInfo.AssetObject;
        }
        return null;
    }

    private void LoadCoroutine(string _path, Action<UnityEngine.Object> _loaded = null)
    {
        AssetInfo _assetInfo = GetAssetInfo(_path, _loaded);
        if (null != _assetInfo)
        {
            CoroutineInstance.Instance.StartCoroutine(_assetInfo.LoadObjectCoroutinue(_loaded));
        }
    }

    private void LoadAsync(string _path, Action<UnityEngine.Object> _loaded = null, Action<float> _progress = null)
    {
        AssetInfo _assetInfo = GetAssetInfo(_path, _loaded);
        if (null != _assetInfo)
        {
            CoroutineInstance.Instance.StartCoroutine(_assetInfo.LoadObjectAsync(_loaded, _progress));
        }
    }
    #endregion

    #region GetAssetInfo & Instantiate Object
    private AssetInfo GetAssetInfo(string _path, Action<UnityEngine.Object> _loaded = null)
    {
        if (false == string.IsNullOrEmpty(_path))
        {
            Debug.Log("Error: _path Is NULL!");
            if (null != _loaded)
            {
                _loaded(null);
            }
        }

        AssetInfo _assetInfo = null;
        if (false == m_dicAssetInfo.TryGetValue(_path, out _assetInfo))
        {
            _assetInfo = new AssetInfo();
            _assetInfo.Path = _path;
            m_dicAssetInfo.Add(_path, _assetInfo);
        }
        _assetInfo.RefCount++;

        return _assetInfo;
    }

    private UnityEngine.Object Instantiate(UnityEngine.Object _obj, Action<UnityEngine.Object> _loaded = null)
    {
        UnityEngine.Object _go = null;
        if (null != _obj)
        {
            _go = MonoBehaviour.Instantiate(_obj);
            if (null != _go)
            {
                if (null != _loaded)
                {
                    _loaded(_obj);
                    return null;
                }
                return _go;
            }
            else
            {
                Debug.LogError("Error : Instantiate Failed !");
            }
        }
        else
        {
            Debug.LogError("Error : Parameter _object is null !");
        }
        return null;
    }
    #endregion

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
}

