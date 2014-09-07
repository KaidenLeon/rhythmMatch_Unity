     
using System;
 
     	     

namespace S2C{
public class Common
	{
		// Message ID that replies to each RMI method. 
	
public const Nettention.Proud.RmiID ShowChat = (Nettention.Proud.RmiID)4000+1;

public const Nettention.Proud.RmiID SystemChat = (Nettention.Proud.RmiID)4000+2;

 

		// List that has RMI ID.
		public static Nettention.Proud.RmiID[] RmiIDList = new Nettention.Proud.RmiID[] {
ShowChat,
SystemChat,
};
	}
}
