using UnityEngine;
using System.Collections;

using Nettention.Proud;
using System.Collections.Generic;
using System;

public class ClientEventSink
{
	NetClient m_client;
	C2C.Proxy m_c2cProxy;
	public bool IsConnectWaiting;
	public bool IsConnected;
	public HostID GroupID;
	
	public ClientEventSink(NetClient client, C2C.Proxy proxy)
	{
		m_client = client;
		m_c2cProxy = proxy;
		
		IsConnectWaiting = true;
		IsConnected = false;
		GroupID = HostID.None;
		
		m_client.JoinServerCompleteHandler = OnJoinServerComplete;
		m_client.LeaveServerHandler = OnLeaveServer;
		m_client.P2PMemberJoinHandler = OnP2PMemberJoin;
		m_client.P2PMemberLeaveHandler = OnP2PMemberLeave;
		
		m_client.ErrorHandler = OnError;
		m_client.WarningHandler = OnWarning;
		m_client.ExceptionHandler = OnException;
		m_client.InformationHandler = OnInformation;
		
		m_client.NoRmiProcessedHandler = OnNoRmiProcessed;
		m_client.ChangeServerUdpStateHandler = OnChangeServerUdp;
		m_client.ReceivedUserMessageHandler = OnReceiveUserMessage;
	}
	
	public void OnJoinServerComplete(ErrorInfo info, ByteArray replyFromServer)//
	{
		IsConnectWaiting = false;
		if (info.errorType == ErrorType.Ok)
		{
			NetworkManager.print("Server connection ok. Client HostID = " + m_client.LocalHostID);
			IsConnected = true;
		}
		else
		{
			NetworkManager.print("Server connection failed.");
		}
	}
	
	public void OnLeaveServer(ErrorInfo errorInfo)//
	{
		IsConnected = false;
	}
	
	public void OnP2PMemberJoin(HostID memberHostID, HostID groupHostID, int memberCount, ByteArray customField)
	{
		NetworkManager.print("[Client] P2P member " + memberHostID + " joined  group " + groupHostID + ".");
		
		this.GroupID = groupHostID;
		
		if (memberHostID != m_client.LocalHostID)
		{
			m_c2cProxy.P2PChat(memberHostID, RmiContext.ReliableSend, "Hello~~", 1, 1);
		}
	}
	
	public void OnP2PMemberLeave(HostID memberHostID, HostID groupHostID, int memberCount)
	{
		NetworkManager.print("[Client] P2P member " + memberHostID + " left group " + groupHostID + ".");
	}
	
	public void OnError(ErrorInfo errorInfo)
	{
		NetworkManager.print("Error : " + errorInfo.ToString());
	}
	
	public void OnException(HostID remoteID, System.Exception e)
	{
		NetworkManager.print("exception : " + e.ToString());
	}
	
	public void OnInformation(ErrorInfo errorInfo)
	{
		NetworkManager.print("Information " + errorInfo.ToString());
	}
	
	public void OnNoRmiProcessed(RmiID rmiID)
	{
		NetworkManager.print("NoRmiProcessed : " + rmiID);
	}
	
	public void OnWarning(ErrorInfo errorInfo)
	{
		NetworkManager.print("Warinig :  " + errorInfo.ToString());
	}
	
	public void OnChangeServerUdp(ErrorType reason)
	{
		NetworkManager.print("ChangeServerUdp " + reason);
	}
	
	public void OnReceiveUserMessage(HostID sender, RmiContext rmiContext, ByteArray payload)
	{
		NetworkManager.print("ReceiveUserMessage HostID : " + sender);
	}
}



public class NetworkManager : MonoBehaviour
{
	
	NetClient m_client;
	NetConnectionParam m_connectionParam;
	
	ClientEventSink m_eventSink;
	
	C2S.Proxy m_c2sProxy;
	C2C.Proxy m_c2cProxy;
	C2C.Stub m_c2cStub;
	S2C.Stub m_s2cStub;
	
	SimpleUnity.MyClass m_myClass;

	// 코루틴에 사용할 대기시간. WaitForSeconds() 는 초단위.
	float m_WaitTime = 0.01f;



	List<int> m_listData;
	Dictionary<int, float> m_dictionaryData;
	ByteArray m_blockData;
	




	private static NetworkManager _instance = null;
	public static NetworkManager GetInstance()
	{
		return _instance;
	}


	// Use this for initialization
	IEnumerator Start()
	{

		if (_instance == null)
		{
			_instance = this;

		}		
		else
		{
			Destroy (gameObject);
		}

		m_client = new NetClient();
		m_connectionParam = new NetConnectionParam();
		
		m_c2sProxy = new C2S.Proxy();
		m_c2cProxy = new C2C.Proxy();
		m_c2cStub = new C2C.Stub();
		m_s2cStub = new S2C.Stub();
		
		m_eventSink = new ClientEventSink(m_client, m_c2cProxy);
		
		m_myClass = new SimpleUnity.MyClass();
		
		m_s2cStub.ShowChat = ShowChat;
		m_s2cStub.SystemChat = SystemChat;
		m_c2cStub.P2PChat = P2PChat;
		
		m_client.AttachProxy(m_c2sProxy);
		m_client.AttachProxy(m_c2cProxy);
		m_client.AttachStub(m_c2cStub);
		m_client.AttachStub(m_s2cStub);
		
		m_connectionParam.protocolVersion = SimpleUnity.Vars.Version;
		
		// 웹플레이어로 접속시에는 localhost 가 아닌 IP를 써주어야 함.
		m_connectionParam.serverIP = GameParameters.serverIP;
		m_connectionParam.serverPort = (ushort)SimpleUnity.Vars.ServerPort;





		#if UNITY_WEBPLAYER
		Security.PrefetchSocketPolicy(m_connectionParam.serverIP, (int)m_connectionParam.serverPort);
		#endif
		
		if (m_client.Connect(m_connectionParam) == false)
		{
			print("m_client failed to connect to server!!");
		}
		else
		{
			
			long time = System.DateTime.Now.Ticks;
			long currentTime;
			
			while (m_eventSink.IsConnectWaiting)
			{
				// 코루틴을 이용해 m_WaitTime 동안 대기.
				yield return new WaitForSeconds(m_WaitTime);
				m_client.FrameMove();
				
				currentTime = System.DateTime.Now.Ticks;
				
				// ticks 의 1초는 10000000
				if (currentTime - time >  100000000)
					break;
			}
			
			m_myClass.a = 0;
			m_myClass.b = 0.0f;
			m_myClass.c = 0.0;
			
			// List혹은 Dictionary를 Rmi인자로 사용이 가능합니다.
			m_listData = new List<int>();
			m_listData.Add(0);
			m_listData.Add(0);
			
			m_dictionaryData = new Dictionary<int, float>();
			m_dictionaryData.Add(1, 0.0f);
			m_dictionaryData.Add(2, 0.0f);
			
			m_blockData = new ByteArray();
			for (int i = 0; i < 100; ++i)
				m_blockData.Add((byte)i);
			
			m_c2sProxy.Chat(HostID.Server, RmiContext.ReliableSend, "Login", 0, 0.0f, m_myClass, m_listData, m_dictionaryData, m_blockData);
		}
		
	}
	
	// Update is called once per frame
	void Update()
	{
		if (m_eventSink.IsConnected)
		{
			m_client.FrameMove();
		}

	}


	// note start = 1
	// judge result = 2
	// item request = 3
	// item result = 4

	//custom wrapper
	public void SendNoteStart( NoteType noteType )
	{
	    string dataA_packetData = (int)noteType + "";
		int dataB_packetType = 1; // note start
		float dataC_playTime = SoundManager.GetInstance().GetPlayTime();

		m_c2sProxy.Chat(HostID.Server, RmiContext.ReliableSend, dataA_packetData, dataB_packetType, dataC_playTime, m_myClass, m_listData, m_dictionaryData, m_blockData);

		if (m_eventSink.GroupID != HostID.None)
		{
			m_c2cProxy.P2PChat(m_eventSink.GroupID, RmiContext.ReliableSend, dataA_packetData, dataB_packetType, (float)GameParameters.myID);
		}
	}

	public void SendJudgeResult( JudgeType judgeType )
	{
		string dataA_packetData = (int)judgeType + "-" + GameParameters.combo + "-" + GameParameters.HP + "-" + GameParameters.itemGauge;
		int dataB_packetType = 2; // judge result
		float dataC_playTime = SoundManager.GetInstance().GetPlayTime();
		
		m_c2sProxy.Chat(HostID.Server, RmiContext.ReliableSend, dataA_packetData, dataB_packetType, dataC_playTime, m_myClass, m_listData, m_dictionaryData, m_blockData);
		
		if (m_eventSink.GroupID != HostID.None)
		{
			m_c2cProxy.P2PChat(m_eventSink.GroupID, RmiContext.ReliableSend, dataA_packetData, dataB_packetType, (float)GameParameters.myID);
		}
	}

	public void SendItemRequest( ItemType itemType, bool isON )
	{
		string dataA_packetData = (int)itemType + "-" + isON;
		int dataB_packetType = 3; // item request
		float dataC_playTime = SoundManager.GetInstance().GetPlayTime();
		
		m_c2sProxy.Chat(HostID.Server, RmiContext.ReliableSend, dataA_packetData, dataB_packetType, dataC_playTime, m_myClass, m_listData, m_dictionaryData, m_blockData);
		
		if (m_eventSink.GroupID != HostID.None)
		{
			m_c2cProxy.P2PChat(m_eventSink.GroupID, RmiContext.ReliableSend, dataA_packetData, dataB_packetType, (float)GameParameters.myID);
		}
	}

	public void SendItemResult( ItemType itemType, bool isON )
	{
		string dataA_packetData = (int)itemType + "-" + isON;
		int dataB_packetType = 4; // item result
		float dataC_playTime = SoundManager.GetInstance().GetPlayTime();
		
		m_c2sProxy.Chat(HostID.Server, RmiContext.ReliableSend, dataA_packetData, dataB_packetType, dataC_playTime, m_myClass, m_listData, m_dictionaryData, m_blockData);
		
		if (m_eventSink.GroupID != HostID.None)
		{
			m_c2cProxy.P2PChat(m_eventSink.GroupID, RmiContext.ReliableSend, dataA_packetData, dataB_packetType, (float)GameParameters.myID);
		}
	}


	// RMI stub
    public bool ShowChat(HostID remote, RmiContext rmiContext, string a, int packetType, float playTime)
	{
		//Debug.Log("[m_client] ShowChat : packetType=" + packetType + ", packetType=" + a + ", playTime=" + playTime);
		
		return true;
	}
	
	public bool SystemChat(HostID remote, RmiContext rmiContext, string txt)
	{
		Debug.Log("[m_client] SystemChat : txt=" + txt);
		SoundManager.GetInstance().PlayBGM();
		GameParameters.HP = 10;
		return true;
	}
		
	public bool P2PChat(HostID remote, RmiContext rmiContext, string a, int packetType, float SenderID)
	{

		if( (int)SenderID == GameParameters.myID )
		{
			return true;
		}

		string relayed = "";
		if (rmiContext.relayed == true)
		{
			relayed = "true";
		}
		else
		{
			relayed = "false";
		}
		Debug.Log("[m_client] P2PChat relayed:" + relayed + ", packetType=" + packetType + ", a=" + a + ", SenderID=" + SenderID);



		// note start = 1
		// judge result = 2
		// item request = 3
		// item result = 4

		// test unpack
		switch(packetType)
		{

		case 1:
		{
			int noteTypeInt = Convert.ToInt32(a);
			NoteType noteType = (NoteType)noteTypeInt;
			E_NoteManager.GetInstance().StartNote(noteType);
			break;
		}
		case 2:
		{
			string[] split1 = a.Split('-');
			int judgeTypeInt = Convert.ToInt32( split1[0] );
			JudgeType judgeType = (JudgeType)judgeTypeInt;
			int combo = Convert.ToInt32( split1[1] );
			int HP = Convert.ToInt32( split1[2] );
			int itemGauge = Convert.ToInt32( split1[3] );
			E_JudgeManager.GetInstance().JudgeNote(judgeType, combo, HP, itemGauge);
			break;
		}
		case 3:
		{
			string[] split1 = a.Split('-');
			bool isON = Convert.ToBoolean( split1[1] );
			ItemManager.GetInstance().Rotate(isON);
			break;
		}
		case 4:
		{
			string[] split1 = a.Split('-');
			bool isON = Convert.ToBoolean( split1[1] );
			E_ItemManager.GetInstance().Rotate(isON);
			break;
		}

		}

		return true;
	}
}
