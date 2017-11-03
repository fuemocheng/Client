using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class NetIO
{
    private static NetIO m_instance;
    public static NetIO Instance
    {
        get
        {
            if (null == m_instance)
            {
                m_instance = new NetIO();
            }
            return m_instance;
        }
    }

    private Socket m_socket;

    private string ip = "127.0.0.1";
    private int port = 8001;

    //异步接收缓冲区
    private byte[] readBuff = new byte[1024];
    //长度解码缓冲区
    private List<byte> cache = new List<byte>();

    private bool m_bIsReading = false;
    private bool m_bIsWriting = false;

    List<SocketModel> m_listMessages = new List<SocketModel>();

    private NetIO()
    {
        try
        {
            //创建客户端连接对象
            m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //连接到服务器
            m_socket.Connect(ip, port);
            //开启异步消息接收 消息到达后会直接写入缓冲区 readBuff
            m_socket.BeginReceive(readBuff, 0, 1024, SocketFlags.None, ReceiveCallBack, readBuff);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// 收到消息异步回调
    /// </summary>
    /// <param name="ar"></param>
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            //获取当前收到的消息的长度
            int length = m_socket.EndReceive(ar);
            byte[] message = new Byte[length];
            Buffer.BlockCopy(readBuff, 0, message, 0, length);
            cache.AddRange(message);
            if (false == m_bIsReading)
            {
                m_bIsReading = true;
                OnData();
            }
            //尾递归 再次开启异步消息接收 消息到达后会直接写入 缓冲区 readbuff
            m_socket.BeginReceive(readBuff, 0, 1024, SocketFlags.None, ReceiveCallBack, readBuff);
        }
        catch (Exception e)
        {
            Debug.Log("远程服务器主动断开连接 " + e.Message);
            m_socket.Close();
        }
    }

    public void Write(byte type, int area, int command, object message)
    {
        ByteArray ba = new ByteArray();
        ba.Write(type);
        ba.Write(area);
        ba.Write(command);
        //判断消息体是否为空 不为空则序列化后写入
        if (null != message)
        {
            ba.Write(SerializeUtil.Encode(message));
        }

        ByteArray byteArray = new ByteArray();
        byteArray.Write(ba.Length);
        byteArray.Write(ba.GetBuffer());

        try
        {
            m_socket.Send(byteArray.GetBuffer());
        }
        catch (Exception e)
        {
            Debug.Log("网络错误：" + e.Message);
        }
    }

    private void OnData()
    {
        //长度解码
        byte[] result = LengthDecode(ref cache);

        //长度解码返回空 说明消息体不全，等待下条消息过来补全
        if (result == null)
        {
            m_bIsReading = false;
            return;
        }

        SocketModel message = MessageDecode(result);

        if (message == null)
        {
            m_bIsReading = false;
            return;
        }
        //进行消息的处理
        m_listMessages.Add(message);
        //尾递归 防止在消息处理过程中 有其他消息到达而没有经过处理
        OnData();
    }


    private byte[] LengthDecode(ref List<byte> cache)
    {
        if (cache.Count < 4) return null;

        //创建内存流对象，并将缓存数据写入进去
        MemoryStream ms = new MemoryStream(cache.ToArray());
        //二进制读取流
        BinaryReader br = new BinaryReader(ms);
        //从缓存中读取int型消息体长度
        int length = br.ReadInt32();
        //如果消息体长度 大于缓存中数据长度 说明消息没有读取完 等待下次消息到达后再次处理
        if (length > ms.Length - ms.Position)
        {
            return null;
        }
        //读取正确长度的数据
        byte[] result = br.ReadBytes(length);
        //清空缓存
        cache.Clear();
        //将读取后的剩余数据写入缓存
        cache.AddRange(br.ReadBytes((int)(ms.Length - ms.Position)));
        br.Close();
        ms.Close();
        return result;
    }


    private SocketModel MessageDecode(byte[] value)
    {
        ByteArray ba = new ByteArray(value);
        SocketModel model = new SocketModel();
        byte type;
        int area;
        int command;
        //从数据中读取 三层协议  读取数据顺序必须和写入顺序保持一致
        ba.Read(out type);
        ba.Read(out area);
        ba.Read(out command);
        model.type = type;
        model.area = area;
        model.command = command;
        //判断读取完协议后 是否还有数据需要读取 是则说明有消息体 进行消息体读取
        if (ba.Readable)
        {
            byte[] message;
            //将剩余数据全部读取出来
            ba.Read(out message, ba.Length - ba.Position);
            //反序列化剩余数据为消息体
            model.message = SerializeUtil.Decode(message);
        }
        ba.Close();
        return model;
    }
}
