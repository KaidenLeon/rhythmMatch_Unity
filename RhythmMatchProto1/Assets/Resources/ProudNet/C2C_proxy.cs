using System;
using System.Net;
 


namespace C2C{
public class Proxy:Nettention.Proud.RmiProxy
	{
public bool P2PChat(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String a, int b, float c)
{
Nettention.Proud.Message __msg=new Nettention.Proud.Message();
	
	Nettention.Proud.RmiID __msgid= Common.P2PChat;
	__msg.Write(__msgid);
	
Nettention.Proud.Marshaler.Write(__msg,a);
Nettention.Proud.Marshaler.Write(__msg,b);
Nettention.Proud.Marshaler.Write(__msg,c);

		Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
		__list[0] = remote;
		
		return RmiSend(__list,rmiContext,__msg,
			RmiName_P2PChat, Common.P2PChat);
	}

	public bool P2PChat(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, System.String a, int b, float c)
{
Nettention.Proud.Message __msg=new Nettention.Proud.Message();
	
	Nettention.Proud.RmiID __msgid= Common.P2PChat;
	__msg.Write(__msgid);
	
Nettention.Proud.Marshaler.Write(__msg,a);
Nettention.Proud.Marshaler.Write(__msg,b);
Nettention.Proud.Marshaler.Write(__msg,c);

		return RmiSend(remotes,rmiContext,__msg,
			RmiName_P2PChat, Common.P2PChat);
	}


// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
const string RmiName_P2PChat="P2PChat";
const string RmiName_First=RmiName_P2PChat;
    
		public override Nettention.Proud.RmiID[] GetRmiIDList() { return Common.RmiIDList; } 

	}
}

