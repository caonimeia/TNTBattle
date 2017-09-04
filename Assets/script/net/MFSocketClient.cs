using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;


public interface SocketClient {
    void Connect();
    void Send();
    void Receive();
}

public class MFUDPClient {
    private UdpClient _udpClient;
    private Socket _socket;
    private readonly string _ip;
    private readonly int _port;

    MFUDPClient(string ip, int port) {
        _ip = ip;
        _port = port;
    }

    public void Connect() {
        try {
            IPAddress ipAddr;
            if (!IPAddress.TryParse(_ip, out ipAddr)) {
                MFLog.LogError("Parse IPAdderss Error");
                return;
            }

            _udpClient = new UdpClient();
            //_udpClient.DontFragment = true;
            _udpClient.Connect(ipAddr, _port);
        }catch(Exception e) {
            MFLog.LogError(e.ToString());
            MFLog.LogError("Socket Connect Failed");
        }
    }
}


class MFSocketClient {
    private Socket _socket;
    private MFISerializable _serializable;
    private readonly string _ip;
    private readonly int _port;
    public MFSocketClient(string ip, int port, MFISerializable serializable) {
        _ip = ip;
        _port = port;
        _serializable = serializable;
    }

    public void Connect() {
        try {
            IPAddress ipAddr;
            if (!IPAddress.TryParse(_ip, out ipAddr)) {
                MFLog.LogError("Parse IPAdderss Error");
                return;
            }

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.BeginConnect(ipAddr, _port, ConnectCallBack, null);
        }catch(Exception e) {
            MFLog.LogError(e.ToString());
            MFLog.LogError("Socket Connect Failed");
        }
    }

    public void DisConnect() {
        if (_socket != null) {
            if (_socket.Connected) {
                _socket.Disconnect(false);
            }
            _socket.Close();
            _socket = null;
        }
    }

    private void ConnectCallBack(IAsyncResult ar) {
        try {
            _socket.EndConnect(ar);
            MFLog.LogInfo("Connect Success");
            Receive();
        }
        catch (Exception e) {
            MFLog.LogError("Connect Faild");
            MFLog.LogError(e.ToString());
            //todo 加上异常处理
            //m_stateCallback((int)ESocketState.eST_Error);
        }
    }

    public void Receive() {
        if (_socket == null || !_socket.Connected) return;
        var data = new byte[_socket.ReceiveBufferSize];
        _socket.BeginReceive(data, 0, data.Length, SocketFlags.None, ReceiveCallBack, data);
    }

    private void ReceiveCallBack(IAsyncResult ar) {
        try {
            var data = (byte[])ar.AsyncState;
            var length = _socket.EndReceive(ar);
            //m_totalRecv += length;

            if (length > 0) {
                var str = Encoding.UTF8.GetString(data);
                SubRecvData(ref str, length);
                TestA ta = _serializable.DeSerialize<TestA>(data);
                MFLog.LogInfo(ta.a);
                MFNetManager.GetInstance().PushRecvData(str);
                // go on
                Receive();
            } else {
                //todo  没有接收到数据
            }
        }
        catch (Exception e) {
            //todo 加上异常处理 程序关闭时会出异常
            MFLog.LogError(e.ToString());
            //uninit();
            //SFUtils.logWarning("网络连接中断：" + e.Message);
            //dispatcher.dispatchEvent(SFEvent.EVENT_NETWORK_INTERRUPTED);
        }
    }



    // 分包
    private void SubRecvData(ref string data, int dataLen) {
        data = data.Substring(0, dataLen);
        //todo 在这里可以做分包的操作
    }

    public void Send(string msg) {
        if (_socket == null || !_socket.Connected) return;
        try {
            byte[] data = Encoding.UTF8.GetBytes(msg);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, result => {
                _socket.EndSend(result);
            }, null);
        }
        catch (Exception e) {
            //SFUtils.logWarning("Socket消息发送失败: " + e.Message);
        }
    }
}
