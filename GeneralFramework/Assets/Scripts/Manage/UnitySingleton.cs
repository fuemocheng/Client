using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingleton<T> : MonoBehaviour
    where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (null == _instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    //隐藏实例化的 new GameObject
                    //obj.hideFlags = HideFlags.HideAndDontSave;      
                    _instance = (T)obj.AddComponent(typeof(T));
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    //对象创建之初，在Awake之前执行
    protected UnitySingleton()
    {
        Init();
    }

    public virtual void Init() {}
}
