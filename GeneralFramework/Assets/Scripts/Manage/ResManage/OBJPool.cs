using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJPool : UnitySingleton<OBJPool>
{

    private Dictionary<string, List<GameObject>> m_dicPool = new Dictionary<string, List<GameObject>>();

    public GameObject OnGetObj(string objName, string path = null)
    {
        if (null != path)
        {
            if (false == path.Contains("/"))
            {
                path += "/";
            }
        }

        if (false == m_dicPool.ContainsKey(objName) || 0 == m_dicPool[objName].Count)
        {
            if (null == path)
            {
                Debug.LogError("ObjPool does not have : " + objName);
                return null;
            }

            GameObject go = null;
            go = ResManage.Instance.LoadInstance(path + objName) as GameObject;

            if (null == go)
            {
                Debug.LogError("Can Not Find Resources : " + path + objName);
                return null;
            }

            go.name = objName;
            return go;
        }
        else
        {
            GameObject go = m_dicPool[objName][0];
            m_dicPool[objName].RemoveAt(0);

            if (go.transform.parent != this.transform)
            {
                Debug.LogError("Obj is not in Pool : " + go);
            }

            go.transform.SetParent(null);
            return go;
        }
    }

    public void OnReturnObj(GameObject go)
    {
        if (go.transform.parent == this.transform)
        {
            return;
        }

        if (false == m_dicPool.ContainsKey(go.name))
        {
            m_dicPool.Add(go.name, new List<GameObject>());
        }

        go.transform.SetParent(this.transform);
        m_dicPool[go.name].Add(go);
    }

    private void OnDestroy()
    {
        m_dicPool.Clear();
    }
}
