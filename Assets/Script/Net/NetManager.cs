﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using UnityEngine.UI;
using System;

public static class NetManager {
	//定义套接字
	static Socket socket;
	//接收缓冲区
	static byte[] readBuff = new byte[1024]; 
	//委托类型
	public delegate void MsgListener(String str);
	//监听列表
	private static Dictionary<string, MsgListener> listeners = new Dictionary<string, MsgListener>();
	//消息列表
	static List<String> msgList = new List<string>();

	//添加监听
	public static void AddListener(string msgName, MsgListener listener){
		listeners[msgName] = listener;
	}

	//获取描述
	public static string GetDesc(){
		if(socket == null) return "";
		if(!socket.Connected) return "";
		return socket.LocalEndPoint.ToString();
	}

	//连接
	public static void Connect(string ip, int port)
	{
		//Socket
		socket = new Socket(AddressFamily.InterNetwork,
			SocketType.Stream, ProtocolType.Tcp);
		//Connect
		socket.Connect(ip, port);
		//BeginReceive
		socket.BeginReceive( readBuff, 0, 1024, 0,
			ReceiveCallback, socket);
	}
		

	//Receive回调
	private static void ReceiveCallback(IAsyncResult ar){
		try {
			Socket socket = (Socket) ar.AsyncState;
			int count = socket.EndReceive(ar);
			string recvStr = System.Text.Encoding.Default.GetString(readBuff, 0, count);
			msgList.Add(recvStr);
			socket.BeginReceive( readBuff, 0, 1024, 0,
				ReceiveCallback, socket);
		}
		catch (SocketException ex){
			Debug.Log("Socket Receive fail" + ex.ToString());
		}
	}

	//点击发送按钮
	public static void Send(string sendStr)
	{
		if(socket == null) return;
		if(!socket.Connected)return;

		byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
		socket.BeginSend(sendBytes, 0, sendBytes.Length, 0, SendCallback, socket);
	}

	//Send回调
	private static void SendCallback(IAsyncResult ar){
		try {
			Socket socket = (Socket) ar.AsyncState;
			//int count = socket.EndSend(ar);
		}
		catch (SocketException ex){
			Debug.Log("Socket Send fail" + ex.ToString());
		}
	}

	private static string _lastMsg = "";
	//Update
	public static void Update(){
		if(msgList.Count <= 0)
			return;
		String msgStr = msgList[0];
		msgStr = _lastMsg + msgStr;
		_lastMsg = "";
		MonoBehaviour.print("Receive:" + msgStr);
		msgList.RemoveAt(0);

		string[] msgs = msgStr.Split('$');
		int flag = 0;
		if (msgStr.Substring(msgStr.Length - 1) != "$")
		{
			_lastMsg = msgs[msgs.Length-1];
			flag = 1;
		}
		int msgCount = msgs.Length;
		for(int i = 0 ; i < msgCount - flag; i++)
		{
			if (msgs[i] == "")
			{
				continue;
			}
			string[] split = msgs[i].Split('|');
			string msgName = split[0];
			/*string msgArgs;
			if (split.Length < 2)
			{
				msgArgs = "";
				MonoBehaviour.print("----------ThisFunc args is null:"+msgName);
			}
			else
			{
				msgArgs = split[1];
			}*/
			string msgArgs = split[1];
			//监听回调;
			if(listeners.ContainsKey(msgName)){
				listeners[msgName](msgArgs);
			}
		}
		/*string[] split = msgStr.Split('|');
		string msgName = split[0];
		string msgArgs = split[1];
		//监听回调;
		if(listeners.ContainsKey(msgName)){
			listeners[msgName](msgArgs);
		}*/
	}
}
