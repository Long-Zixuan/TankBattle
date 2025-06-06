using System;


public class EventHandler
{
	public static void OnDisconnect(ClientState c){
		string desc = c.socket.RemoteEndPoint.ToString();
		string sendStr = "Leave|" + desc + ",";
		foreach (ClientState cs in ServesMain.clients.Values){
			ServesMain.Send(cs, sendStr);
		}
	}
}

