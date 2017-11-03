using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Ex
{
    /// <summary>
    /// 扩展 Monobehaviour 发送消息方法
    /// </summary>
    /// <param name="mono"></param>
    /// <param name="type"></param>
    /// <param name="area"></param>
    /// <param name="command"></param>
    /// <param name="message"></param>
    public static void WriteMessage(this MonoBehaviour mono, byte type, int area, int command, object message)
    {
        NetIO.Instance.Write(type, area, command, message);
    }

}
