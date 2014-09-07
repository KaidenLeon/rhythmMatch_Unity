using System;
using System.Net;
 


namespace S2C{
public class Proxy:Nettention.Proud.RmiProxy
	{
public bool ShowChat(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String a, int b, float c)
{
Nettention.Proud.Message __msg=new Nettention.Proud.Message();
	
	Nettention.Proud.RmiID __msgid= Common.ShowChat;
	__msg.Write(__msgid);
	
Nettention.Proud.Marshaler.Write(__msg,a);
Nettention.Proud.Marshaler.Write(__msg,b);
Nettention.Proud.Marshaler.Write(__msg,c);

		Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
		__list[0] = remote;
		
		return RmiSend(__list,rmiContext,__msg,
			RmiName_ShowChat, Common.ShowChat);
	}

	public bool ShowChat(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, System.String a, int b, float c)
{
Nettention.Proud.Message __msg=new Nettention.Proud.Message();
	
	Nettention.Proud.RmiID __msgid= Common.ShowChat;
	__msg.Write(__msgid);
	
Nettention.Proud.Marshaler.Write(__msg,a);
Nettention.Proud.Marshaler.Write(__msg,b);
Nettention.Proud.Marshaler.Write(__msg,c);

		return RmiSend(remotes,rmiContext,__msg,
			RmiName_ShowChat, Common.ShowChat);
	}

public bool SystemChat(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String txt)
{
Nettention.Proud.Message __msg=new Nettention.Proud.Message();
	
	Nettention.Proud.RmiID __msgid= Common.SystemChat;
	__msg.Write(__msgid);
	
Nettention.Proud.Marshaler.Write(__msg,txt);

		Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
		__list[0] = remote;
		
		return RmiSend(__list,rmiContext,__msg,
			RmiName_SystemChat, Common.SystemChat);
	}

	public bool SystemChat(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, System.String txt)
{
Nettention.Proud.Message __msg=new Nettention.Proud.Message();
	
	Nettention.Proud.RmiID __msgid= Common.SystemChat;
	__msg.Write(__msgid);
	
Nettention.Proud.Marshaler.Write(__msg,txt);

		return RmiSend(remotes,rmiContext,__msg,
			RmiName_SystemChat, Common.SystemChat);
	}


// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
const string RmiName_ShowChat="ShowChat";
const string RmiName_First=RmiName_ShowChat;
    const string RmiName_SystemChat="SystemChat";

		public override Nettention.Proud.RmiID[] GetRmiIDList() { return Common.RmiIDList; } 

	}
}

