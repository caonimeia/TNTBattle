using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;


class MFSocketClient
{
    private Socket _socket;
    public MFSocketClient()
    {

    }

    public void Connect(string ip, int port)
    {
        IPAddress ipAddr;
        if (!IPAddress.TryParse(ip, out ipAddr))
        {
            //todo 加上日志
            return;
        }

        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _socket.BeginConnect(ipAddr, port, ConnectCallBack, null);
    }

    public void DisConnect()
    {
        if (_socket != null)
        {
            if (_socket.Connected)
            {
                _socket.Disconnect(false);
            }
            _socket.Close();
            _socket = null;
        }
    }

    private void ConnectCallBack(IAsyncResult ar)
    {
        try
        {
            _socket.EndConnect(ar);
            MFLog.LogInfo("Connect Success");
            Receive();
        }
        catch (Exception e)
        {
            //todo 加上异常处理
            //m_stateCallback((int)ESocketState.eST_Error);
        }
    }

    public void Receive()
    {
        if (_socket == null || !_socket.Connected) return;
        var data = new byte[_socket.ReceiveBufferSize];
        _socket.BeginReceive(data, 0, data.Length, SocketFlags.None, ReceiveCallBack, data);
    }

    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            var data = (byte[])ar.AsyncState;
            var length = _socket.EndReceive(ar);
            //m_totalRecv += length;
            
            if (length > 0)
            {
                var str = Encoding.UTF8.GetString(data);
                SubRecvData(ref str, length);
                MFNetManager.GetInstance().PushRecvData(str);
                // go on
                Receive();
            }
            else
            {
                //todo  没有接收到数据
            }
        }
        catch (Exception e)
        {
            //todo 加上异常处理
            //uninit();
            //SFUtils.logWarning("网络连接中断：" + e.Message);
            //dispatcher.dispatchEvent(SFEvent.EVENT_NETWORK_INTERRUPTED);
        }
    }

    

    // 分包
    private void SubRecvData(ref string data, int dataLen)
    {
        data = data.Substring(0, dataLen);
        //todo 在这里可以做分包的操作
    }

    public void Send(string msg)
    {
        if (_socket == null || !_socket.Connected) return;
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, result =>
            {
                _socket.EndSend(result);
            }, null);
        }
        catch (Exception e)
        {
            //SFUtils.logWarning("Socket消息发送失败: " + e.Message);
        }
    }
}
