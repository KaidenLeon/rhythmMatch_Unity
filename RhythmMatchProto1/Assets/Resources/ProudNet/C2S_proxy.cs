using System;
using System.Net;
 


namespace C2S{
public class Proxy:Nettention.Proud.RmiProxy
	{
public bool Chat(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String a, int b, float c, SimpleUnity.MyClass d, System.Collections.Generic.List<int> f, System.Collections.Generic.Dictionary<int,float> g, Nettention.Proud.ByteArray block)
{
Nettention.Proud.Message __msg=new Nettention.Proud.Message();
	
	Nettention.Proud.RmiID __msgid= Common.Chat;
	__msg.Write(__msgid);
	
SimpleUnity.MyMarshaler.Write(__msg,a);
SimpleUnity.MyMarshaler.Write(__msg,b);
SimpleUnity.MyMarshaler.Write(__msg,c);
SimpleUnity.MyMarshaler.Write(__msg,d);
SimpleUnity.MyMarshaler.Write(__msg,f);
SimpleUnity.MyMarshaler.Write(__msg,g);
SimpleUnity.MyMarshaler.Write(__msg,block);

		Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
		__list[0] = remote;
		
		return RmiSend(__list,rmiContext,__msg,
			RmiName_Chat, Common.Chat);
	}

	public bool Chat(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, System.String a, int b, float c, SimpleUnity.MyClass d, System.Collections.Generic.List<int> f, System.Collections.Generic.Dictionary<int,float> g, Nettention.Proud.ByteArray block)
{
Nettention.Proud.Message __msg=new Nettention.Proud.Message();
	
	Nettention.Proud.RmiID __msgid= Common.Chat;
	__msg.Write(__msgid);
	
SimpleUnity.MyMarshaler.Write(__msg,a);
SimpleUnity.MyMarshaler.Write(__msg,b);
SimpleUnity.MyMarshaler.Write(__msg,c);
SimpleUnity.MyMarshaler.Write(__msg,d);
SimpleUnity.MyMarshaler.Write(__msg,f);
SimpleUnity.MyMarshaler.Write(__msg,g);
SimpleUnity.MyMarshaler.Write(__msg,block);

		return RmiSend(remotes,rmiContext,__msg,
			RmiName_Chat, Common.Chat);
	}


// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
const string RmiName_Chat="Chat";
const string RmiName_First=RmiName_Chat;
    
		public override Nettention.Proud.RmiID[] GetRmiIDList() { return Common.RmiIDList; } 

	}
}

