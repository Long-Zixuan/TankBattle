using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMainLogic : BaseSceneLogic
{
	private List<string> chatMSGList = new List<string>();
	//public Text chatUIText;
	
	//public InputField chatUIInput;
	//坦克模型预设
	public GameObject tankPrefab;
	//坦克列表
	public BaseTank myTank;
	public Dictionary<string, BaseTank> otherHumans = new Dictionary<string, BaseTank>();
	
	public Transform[] bornPoints;
	

	new void Start () {
		base.Start();
		NetManager.Send("List|");
		//网络模块
		/*NetManager.AddListener("Enter", OnEnter);
		NetManager.AddListener("List", OnList);
		NetManager.AddListener("Move", OnMove);
		NetManager.AddListener("Rotate", OnRotate);
		NetManager.AddListener("Leave", OnLeave);
		NetManager.AddListener("Fire", OnFire);
		NetManager.AddListener("Die", OnDie);
		
		NetManager.AddListener("GetChatMSG",getChatMSG);
		
		/*NetManager.Connect("127.0.0.1", 8888);
		//添加一个角色
		GameObject obj = (GameObject)Instantiate(tankPrefab);
		float x = Random.Range(-5, 5);
		float z = Random.Range(-5, 5);
		obj.transform.position = new Vector3(x, 0, z);
		myTank = obj.AddComponent<CtrlTank>();
	
		myTank.desc = NetManager.GetDesc();
		//发送协议
		Vector3 pos = myTank.transform.position;
		Vector3 eul = myTank.transform.eulerAngles;
		string sendStr = "Enter|";
		sendStr += NetManager.GetDesc()+ ",";
		sendStr += pos.x + ",";
		sendStr += pos.y + ",";
		sendStr += pos.z + ",";
		sendStr += eul.y + ",";
		NetManager.Send(sendStr);
		NetManager.Send("List|");*/
	}

	void getChatMSG(string msgArgs)
	{
		chatMSGList.Add(msgArgs);
		//chatUIText.text = msgArgs;
	}

	private float timeCount = 0;
	/*void chatTextLogic()
	{
		string chatMSG = "";
		foreach (var s in chatMSGList)
		{
			string tmp = s + "\n";
			chatMSG += tmp;
		}
		chatUIText.text = chatMSG;
		timeCount+= Time.deltaTime;
		if(chatMSGList.Count > 0 && timeCount > 10)
		{
			timeCount = 0;
			chatMSGList.RemoveAt(0);
		}
	}

	public void sendChatMSG()
	{
		string sendStr = "SendChatMSG|";
		sendStr += chatUIInput.text;
		NetManager.Send(sendStr);
	}*/

	new void Update(){
		base.Update();
		//chatTextLogic();
	}

	

	public override void OnList (string msgArgs) {
		Debug.Log("OnList " + msgArgs);
		//解析参数
		string[] split = msgArgs.Split(',');
		int count = (split.Length-1)/5;
		if (bornPoints.Length < count)
		{
			Debug.LogError("出生点数量不足");
			return;
		}
		for(int i = 0; i < count; i++)
		{
			string desc = split[i*5+0];
			/*float x = float.Parse(split[i*6+1]);
			float y = float.Parse(split[i*6+2]);
			float z = float.Parse(split[i*6+3]);
			float eulY = float.Parse(split[i*6+4]);*/
			//int hp = int.Parse(split[i*6+5]);
			float x = bornPoints[i].position.x;
			float y = bornPoints[i].position.y;
			float z = bornPoints[i].position.z;
			float eulY = bornPoints[i].eulerAngles.y;
			//添加一个角色
			GameObject obj = (GameObject)Instantiate(tankPrefab);
			obj.transform.position = new Vector3(x, y, z);
			obj.transform.eulerAngles = new Vector3(0, eulY, 0);
			//是自己
			if (desc == NetManager.GetDesc())
			{
				myTank = obj.AddComponent<CtrlTank>();
				myTank.desc = desc;
				continue;
			}
			BaseTank h = obj.AddComponent<SyncTank>();
			h.desc = desc;
			otherHumans.Add(desc, h);
		}
	}

	public override void OnRotate (string msgArgs) {
		Debug.Log("OnRotate " + msgArgs);
		//解析参数
		string[] split = msgArgs.Split(',');
		string desc = split[0];
		float x = float.Parse(split[1]);
		
		float euX = float.Parse(split[2]);
		float euY = float.Parse(split[3]);
		float euZ = float.Parse(split[4]);
		
		//移动
		if(!otherHumans.ContainsKey(desc))
			return;
		SyncTank h = (SyncTank)otherHumans[desc];
		h.MoveX = x;
		h.transform.eulerAngles = new Vector3(euX, euY, euZ);
		
	}
	
	public override void OnMove (string msgArgs) {
		Debug.Log("OnMove " + msgArgs);
		//解析参数
		string[] split = msgArgs.Split(',');
		string desc = split[0];
		float z = float.Parse(split[1]);
		float posX = float.Parse(split[2]);
		float posY = float.Parse(split[3]);
		float posZ = float.Parse(split[4]);
		
		//移动
		if(!otherHumans.ContainsKey(desc))
			return;
		SyncTank h = (SyncTank)otherHumans[desc];
		
		h.MoveZ = z;
		h.transform.position = new Vector3(posX, posY, posZ);
	}

	public override void OnLeave (string msgArgs) {
		Debug.Log("OnLeave " + msgArgs);
		//解析参数
		string[] split = msgArgs.Split(',');
		string desc = split[0];
		//删除
		if(!otherHumans.ContainsKey(desc))
			return;
		BaseTank h = otherHumans[desc];
		Destroy(h.gameObject);
		otherHumans.Remove(desc);
	}

	public override void OnFire (string msgArgs) {
		Debug.Log("OnFire " + msgArgs);
		//解析参数
		string[] split = msgArgs.Split(',');
		string desc = split[0];
		float eulX = float.Parse(split[1]);
		float eulY = float.Parse(split[2]);
		float eulZ = float.Parse(split[3]);
		
		float posX = float.Parse(split[4]);
		float posY = float.Parse(split[5]);
		float posZ = float.Parse(split[6]);

		Vector3 rotate = new Vector3(eulX, eulY, eulZ);
		Vector3 position = new Vector3(posX, posY, posZ);
		//攻击动作
		if(!otherHumans.ContainsKey(desc))
			return;
		SyncTank h = (SyncTank)otherHumans[desc];
		h.Fire(rotate,position);
	}

	public override void OnDie (string msgArgs) {
		Debug.Log("OnAttack: " + msgArgs);
		//解析参数
		string[] split = msgArgs.Split(',');
		string attackedDesc = split[0];
		//自己死了
		if(attackedDesc == myTank.desc){
			Debug.Log("Game Over");
			myTank.IsDie  = true;
			return;
		}
		//死了
		if(!otherHumans.ContainsKey(attackedDesc))
			return;
		SyncTank h = (SyncTank)otherHumans[attackedDesc];
		h.IsDie = true;

	}

	private string nextSceneName;
	public override void OnStart(string msgArgs)
	{
		print("加分等。。。。");
		nextSceneName = "Scenes/Map/"+msgArgs;
		//SceneManager.LoadScene(scIndex);
		//SceneManager.LoadScene("Scenes/SampleScene");
		Invoke("EndLogic", 5f);
	}

	void EndLogic()
	{
		SceneManager.LoadScene(nextSceneName);
	}
	

}
