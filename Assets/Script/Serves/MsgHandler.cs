﻿using System;
using UnityEngine;
using Random = System.Random;


public class MsgHandler
{
	public static void MsgEnter(ClientState c, string msgArgs){
		//解析参数
		string[] split = msgArgs.Split(',');
		string desc = split[0];
		/*float x = float.Parse(split[1]);
		float y = float.Parse(split[2]);
		float z = float.Parse(split[3]);
		float eulY = float.Parse(split[4]);
		//赋值
		//c.hp = 100;
		c.x = x;
		c.y = y;
		c.z = z;
		c.eulY = eulY;*/
		
		//广播
		string sendStr = "Enter|" + msgArgs;
		int count = 0;
		foreach (ClientState cs in ServesMain.clients.Values){
			ServesMain.Send(cs, sendStr);
			count++;
		}

		
		
		if (count > 1)
		{
			LoadScene();
		}
	}

	private static void LoadScene()
	{
		Random rand = new Random();
		int scIndex = rand.Next(0, 4);
		string sendStr = "Start|" + scIndex.ToString();
		foreach (ClientState cs in ServesMain.clients.Values){
			ServesMain.Send(cs, sendStr);
		}
	}

	public static void MsgList(ClientState c, string msgArgs){
		string sendStr = "List|";
		foreach (ClientState cs in ServesMain.clients.Values){
			cs.isDie = false;
			sendStr+=cs.socket.RemoteEndPoint.ToString() + ",";
			sendStr+=cs.x.ToString() + ",";
			sendStr+=cs.y.ToString() + ",";
			sendStr+=cs.z.ToString() + ",";
			sendStr+=cs.eulY.ToString() + ",";
			sendStr+=cs.score.ToString() + ",";
		}
		ServesMain.Send(c, sendStr);
	}

    public static void MsgMove(ClientState c, string msgArgs)
    {
		try
		{
			//解析参数
			string[] split = msgArgs.Split(',');
			string desc = split[0];
			float x = float.Parse(split[2]);
			float y = float.Parse(split[3]);
			float z = float.Parse(split[4]);
			//赋值
			c.x = x;
			c.y = y;
			c.z = z;

			//广播
			string sendStr = "Move|" + msgArgs;
			foreach (ClientState cs in ServesMain.clients.Values)
			{
				ServesMain.Send(cs, sendStr);
			}
		}
        catch (Exception e)
		{
			Console.WriteLine(e.ToString());
		}
    }

    public static void MsgRotate(ClientState c, string msgArgs)
    {
		try
		{
			//解析参数
			string[] split = msgArgs.Split(',');
			string desc = split[0];
			float x = float.Parse(split[1]);

			float eulY = float.Parse(split[3]);
			//赋值
			c.eulY = eulY;

			//广播
			string sendStr = "Rotate|" + msgArgs;
			foreach (ClientState cs in ServesMain.clients.Values)
			{
				ServesMain.Send(cs, sendStr);
			}
		}
        catch (Exception e)
		{
			Console.WriteLine(e.ToString());
		}
    }

    public static void MsgFire(ClientState c, string msgArgs){
		//广播
		string sendStr = "Fire|" + msgArgs;
		foreach (ClientState cs in ServesMain.clients.Values){
			ServesMain.Send(cs, sendStr);
		}
	}

	public static void MsgPlusScore(ClientState c, string msgArgs)
	{
		//解析参数
		string[] split = msgArgs.Split(',');
		string winerDesc = split[0];
		int plusScoreVal = int.Parse(split[1]);
		ClientState winnerCS = null;
		foreach (ClientState cs in ServesMain.clients.Values){
			if(cs.socket.RemoteEndPoint.ToString() == winerDesc)
				winnerCS = cs;
		}
		if(winnerCS == null) 
			return;
		winnerCS.score += plusScoreVal;
	}

	public static void MsgAttacked(ClientState c, string msgArgs){
		//解析参数
		string[] split = msgArgs.Split(',');
		string attackedDesc = split[0];
		//被攻击
		ClientState hitCS = null;
		foreach (ClientState cs in ServesMain.clients.Values){
			if(cs.socket.RemoteEndPoint.ToString() == attackedDesc)
				hitCS = cs;
		}
		if(hitCS == null) 
			return;

		hitCS.isDie = true;
		int liveTankCount = 0;
		string sendStr = "Die|" + attackedDesc;
		foreach (ClientState cs in ServesMain.clients.Values){
			ServesMain.Send(cs, sendStr);
			if (!cs.isDie)
			{
				liveTankCount++;
			}
		}

		if (liveTankCount == 1)
		{
			LoadScene();
		}
	}
	public static void MsgSendChatMSG(ClientState c,string msgArgs)
	{
		string sendStr = "GetChatMSG|" + msgArgs;
        foreach (ClientState cs in ServesMain.clients.Values)
        {
            ServesMain.Send(cs, sendStr);
        }
    }
}


