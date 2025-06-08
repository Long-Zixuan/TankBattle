using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

public class ClientState
{
	public Socket socket; 
	public byte[] readBuff = new byte[1024]; 
	public bool isDie = false;
	public float x = 0;
	public float y = 0;
	public float z = 0;
	public float eulY = 0;
	public int score;
}

class ServesMain
{
	//监听Socket
	public static Socket listenfd;
	//客户端Socket及状态信息
	public static Dictionary<Socket, ClientState> clients = new Dictionary<Socket, ClientState>();
	
	//public static string IP = "127.0.0.1";
	public static int PORT = 8888;

	public static void MainLogic ()
	{
		//Socket
		listenfd = new Socket(AddressFamily.InterNetwork,
						SocketType.Stream, ProtocolType.Tcp);
		//Bind
		//IPAddress ipAdr = IPAddress.Parse(IP);
		IPAddress ipAdr = IPAddress.Any;
		IPEndPoint ipEp = new IPEndPoint(ipAdr, PORT);
		listenfd.Bind(ipEp);
		//Listen
		listenfd.Listen(0);
		UnityEngine.MonoBehaviour.print("[服务器]启动成功");
		Console.WriteLine("[服务器]启动成功");
		//checkRead
		List<Socket> checkRead = new List<Socket>();
		//主循环
		while(true){
            //填充checkRead列表
            checkRead.Clear();
			checkRead.Add(listenfd); 
			foreach (ClientState s in clients.Values){
				 checkRead.Add(s.socket);
			}
			//select
			Socket.Select(checkRead, null, null, 1000);
			//检查可读对象
			foreach (Socket s in checkRead){
				if(s == listenfd){
					 ReadListenfd(s);
				}
				else{
					ReadClientfd(s);
				}
			}
		}
	}
	//读取Listenfd
	public static void ReadListenfd(Socket listenfd){
		Console.WriteLine("Accept");
		MonoBehaviour.print("Accept");
		Socket clientfd = listenfd.Accept();
		ClientState state = new ClientState();
		state.socket = clientfd;
		clients.Add(clientfd, state);
	}
	//读取Clientfd
	public static bool ReadClientfd(Socket clientfd){
		ClientState state = clients[clientfd];
		//接收
		int count = 0;
		try{
			count = clientfd.Receive(state.readBuff);
		}catch(SocketException ex){
			MethodInfo mei =  typeof(EventHandler).GetMethod("OnDisconnect");
			object[] ob = {state};
			mei.Invoke(null, ob);

			clientfd.Close();
			clients.Remove(clientfd);
			Console.WriteLine("Receive SocketException " + ex.ToString());
			return false;
		}
		//客户端关闭
		if(count <= 0){
			MethodInfo mei =  typeof(EventHandler).GetMethod("OnDisconnect");
			object[] ob = {state};
			mei.Invoke(null, ob);

			clientfd.Close();
			clients.Remove(clientfd);
			Console.WriteLine("Socket Close");
			return false;
		}
		//消息处理
		string recvStr = 
				System.Text.Encoding.Default.GetString(state.readBuff, 0, count);
		string[] split = recvStr.Split('|');
		Console.WriteLine("Recv " + recvStr);
		string msgName = split[0];
		string msgArgs = split[1];
		string funName = "Msg" + msgName;
		MethodInfo mi =  typeof(MsgHandler).GetMethod(funName);
		object[] o = {state, msgArgs};
		mi.Invoke(null, o);
		return true;
	}
	//发送
	public static void Send(ClientState cs, string sendStr){
		byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendStr);
		cs.socket.Send(sendBytes);
	}


}