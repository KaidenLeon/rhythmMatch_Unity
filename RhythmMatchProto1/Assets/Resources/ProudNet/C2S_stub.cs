
using System;
using System.Net;	     
 


namespace C2S{
public class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

public delegate bool ChatDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String a, int b, float c, SimpleUnity.MyClass d, System.Collections.Generic.List<int> f, System.Collections.Generic.Dictionary<int,float> g, Nettention.Proud.ByteArray block);
public ChatDelegate Chat = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, System.String a, int b, float c, SimpleUnity.MyClass d, System.Collections.Generic.List<int> f, System.Collections.Generic.Dictionary<int,float> g, Nettention.Proud.ByteArray block)
		{ 
			return false;
		};

	public override bool ProcessReceivedMessage(Nettention.Proud.ReceivedMessage pa, Object hostTag) 
	{
		Nettention.Proud.HostID remote=pa.RemoteHostID;
		if(remote==Nettention.Proud.HostID.None)
		{
			ShowUnknownHostIDWarning(remote);
		}

		Nettention.Proud.Message __msg=pa.ReadOnlyMessage;
		int orgReadOffset = __msg.ReadOffset;
        Nettention.Proud.RmiID __rmiID = Nettention.Proud.RmiID.None;
        if (!__msg.Read( out __rmiID))
            goto __fail;
					
		switch(__rmiID)
		{
case Common.Chat:
		{
			Nettention.Proud.RmiContext ctx=new Nettention.Proud.RmiContext();
			ctx.sentFrom=pa.RemoteHostID;
			ctx.relayed=pa.IsRelayed;
			ctx.hostTag=hostTag;
			ctx.encryptMode = pa.EncryptMode;
			ctx.compressMode = pa.CompressMode;
			
System.String a; SimpleUnity.MyMarshaler.Read(__msg,out a);
int b; SimpleUnity.MyMarshaler.Read(__msg,out b);
float c; SimpleUnity.MyMarshaler.Read(__msg,out c);
SimpleUnity.MyClass d; SimpleUnity.MyMarshaler.Read(__msg,out d);
System.Collections.Generic.List<int> f; SimpleUnity.MyMarshaler.Read(__msg,out f);
System.Collections.Generic.Dictionary<int,float> g; SimpleUnity.MyMarshaler.Read(__msg,out g);
Nettention.Proud.ByteArray block; SimpleUnity.MyMarshaler.Read(__msg,out block);
core.PostCheckReadMessage(__msg, RmiName_Chat);
if(enableNotifyCallFromStub==true)
			{
				string parameterString="";
parameterString+=a.ToString()+",";
parameterString+=b.ToString()+",";
parameterString+=c.ToString()+",";
parameterString+=d.ToString()+",";
parameterString+=f.ToString()+",";
parameterString+=g.ToString()+",";
parameterString+=block.ToString()+",";
NotifyCallFromStub(Common.Chat, RmiName_Chat,parameterString);
			}
			
			if(enableStubProfiling)
			{
				Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
				summary.rmiID = Common.Chat;
				summary.rmiName = RmiName_Chat;
				summary.hostID = remote;
				summary.hostTag = hostTag;
				BeforeRmiInvocation(summary);
			}
			
			long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();
			
			// Call this method.
			bool __ret=Chat (remote,ctx ,a,b,c,d,f,g,block );
			
			if(__ret==false)
			{
				// Error: RMI function that a user did not create has been called. 
				core.ShowNotImplementedRmiWarning(RmiName_Chat);
			}
			
			if(enableStubProfiling)
			{
				Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
				summary.rmiID = Common.Chat;
				summary.rmiName = RmiName_Chat;
				summary.hostID = remote;
				summary.hostTag = hostTag;
				summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
				AfterRmiInvocation(summary);
			}
		}
		break;
default:
			 goto __fail;
		}
		return true;
__fail:
	  {
			__msg.ReadOffset = orgReadOffset;
			return false;
	  }
	}
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
const string RmiName_Chat="Chat";
const string RmiName_First=RmiName_Chat;
    public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}

