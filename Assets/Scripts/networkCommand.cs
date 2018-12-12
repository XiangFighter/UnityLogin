using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class networkCommand : MonoBehaviour {
    public string serverIP = "127.0.0.1";
    public int serverPort = 8080;
    byte[] data = new byte[8096];
    Socket clientSocket;
    public static networkCommand instance;
    public static networkCommand GetInstance() { return instance; }

	// Use this for initialization
	void Start () {
		if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        if(instance.clientSocket == null)
        {
            instance.clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        clientSocket.BeginReceive(data, 0, 8096, SocketFlags.None, null, null);
    }
	
    public void ExcudeCommand(string commandName, object[] paraments)
    {
        if(!instance.clientSocket.Connected)
        {
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse(serverIP), serverPort));
        }
        string messageString = commandName + ":";
        foreach(var par in paraments)
        {
            messageString += par.ToString() + ",";
        }
        byte[] buffer = Encoding.UTF8.GetBytes(messageString);
        clientSocket.Send(buffer);

        //接收服务器传回的数据
        clientSocket.Receive(data);
        string serverCommand = Encoding.ASCII.GetString(data);
        print("收到服务器回复：" + serverCommand);
    }
}
