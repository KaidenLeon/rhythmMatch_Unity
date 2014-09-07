// Server.cpp : 콘솔 응용 프로그램에 대한 진입점을 정의합니다.
// Server.cpp : Define entrance point about console application program.
//

#include "stdafx.h"
#include "../../../include/ProudNetServer.h"
using namespace std;
using namespace Proud;
#include "../common/vars.h"

#include "../common/C2S_common.cpp"
#include "../common/S2C_common.cpp"
#include "../common/C2S_stub.h"
#include "../common/S2C_proxy.h"
#include "../common/C2S_stub.cpp"
#include "../common/S2C_proxy.cpp"

using namespace std;

class C2SStub: public C2S::Stub
{
public:
	DECRMI_C2S_Chat;
};

C2SStub g_C2SStub;
S2C::Proxy g_S2CProxy;
HostID g_groupHostID = HostID_None;

DEFRMI_C2S_Chat(C2SStub)
{
	printf( "[Server] chat message received : a=%s, b=%d, c=%f, d.a=%d, d.b=%.f, d.c=%lf", (const char*)CW2A(a), b, c, d.a, d.b, d.c);	
	int i;
	for(i = 0; i < f.Count; ++i)
		printf(",f[%d]=%d", i, f[i]);

	for(CFastMap<int,float>::iterator it = g.begin(); it!=g.end(); ++it)
		printf(",pair(%d,%f)", it->first, it->second);

	printf("\n");

	if(block.Count != 100)
		printf(("Error : ByteArray length is not equal.\n"));

	for(i = 0; i < block.Count; ++i)
	{
		if((BYTE)i != block[i])
			printf("ByteArray data is not equal. index:%d, data:%d\n",i,(int)block[i]);
	}

	// 서버에서 받은 채팅 메시지를 다시 클라이언트에게 에코한다.
	// Echo chatting message which received from server to client.
	g_S2CProxy.ShowChat(remote, RmiContext::ReliableSend, a, b + 1, c + 1);
	return true;
}
bool makeRoom = false;
class CServerEventSink: public INetServerEvent
{
	virtual void OnClientJoin(CNetClientInfo *clientInfo) OVERRIDE
	{
		printf("Client %d connected.\n", clientInfo->m_HostID);
		makeRoom = true;
	}

	virtual void OnClientLeave( CNetClientInfo *clientInfo, ErrorInfo *errorInfo, const ByteArray& comment ) OVERRIDE
	{
		printf("Client %d disconnected.\n", clientInfo->m_HostID);
	}

	virtual bool OnConnectionRequest(AddrPort clientAddr, ByteArray &userDataFromClient, ByteArray &reply) OVERRIDE
	{
		return true;
	}

	virtual void OnP2PGroupJoinMemberAckComplete(HostID groupHostID,HostID memberHostID,ErrorType result) OVERRIDE {}
	virtual void OnUserWorkerThreadBegin() OVERRIDE {}
	virtual void OnUserWorkerThreadEnd() OVERRIDE {}

	virtual void OnError(ErrorInfo *errorInfo) OVERRIDE{ printf("OnError : %s",(const char*)CW2A(errorInfo->ToString())); }
	virtual void OnWarning(ErrorInfo *errorInfo) OVERRIDE{ printf("OnWarning : %s",(const char*)CW2A(errorInfo->ToString())); }
	virtual void OnInformation(ErrorInfo *errorInfo) OVERRIDE{ printf("OnInformation : %s",(const char*)CW2A(errorInfo->ToString())); }
	virtual void OnException(Exception &e) OVERRIDE{ printf("OnInformation : %s",e.what()); }

	/** RMI가 호출이 들어왔으나 Attach된 Stub 중에 대응하는 RMI가 전혀 없으면 이것이 콜백된다. */
	/** RMI has called but there are no opposited RMI from attached Stub then this will callback. */
	virtual void OnNoRmiProcessed(Proud::RmiID rmiID) OVERRIDE {}
};

CServerEventSink g_eventSink;


int _tmain(int argc, _TCHAR* argv[])
{
	CNetServer* srv = CNetServer::Create();

	srv->SetEventSink(&g_eventSink);
	srv->AttachStub(&g_C2SStub);
	srv->AttachProxy(&g_S2CProxy);

	CStartServerParameter p1;
	p1.m_protocolVersion = g_Version;
	p1.m_tcpPorts.Add(g_ServerPort);

	srv->Start(p1);

	puts("Server started.\n");
	puts("1키를 누르면 서버에 연결된 모든 클라이언트가 P2P 그룹이 맺어지게 합니다.\n");

	while (1)
	{
		if (_kbhit())
		{
			int ch = _getch();
			switch (ch)
			{
			case 27: // ESC key
				goto out;
			case '1':
				/* 1키를 누르면 서버에 연결된 모든
				클라이언트가 P2P 그룹이 맺어지게 한다.
				주의: P2P 그룹 맺기의 권한은 오직 서버에게만 있다.
				클라가 임의로 할 수는 없고, 서버에 그룹 맺기 요청을 별도 RMI
				로 보내는 방식이 좋다. */

				/* Make P2P group with all clients which connected with server.
				Note: only server has P2P grouping permission.
				Client does not do themselves, it is better to send grouping request to server with extra RMI. */
				{
					// 안전한 코딩은 아니지만 샘플이니까 ^^
 					// This is not safe coding but it is a sample.
					HostID list[100]; 

					/* 서버에 접속된 모든 클라 목록을 얻는다.
					(참고: 이렇게 하는 방식보다는 OnClientJoin, OnClientLeave
					이벤트를 활용해서 서버에 접속한 클라이언트 객체 리스트를
					별도 가지게 만드는 것이 좋다. */

					/* Get list of all client which connected at server.
					(Note: We recommend to have an extra connected client object list using OnClientJoin, OnClientLeave event rather than this method. */
					int listCount = srv->GetClientHostIDs(list, 100);

					/* 위의 목록을 모두 묶은 P2P 그룹을 만든다.
					입력 파라메터: P2P 그룹을 묶을 클라 목록, 커스텀 데이터
					(커스텀 데이터는, P2P 그룹을 묶었다는 이벤트가 클라에 전송
					되면서 같이 가는 데이터이다. 예를 들어 팀 구별이라든지,
					개발자가 지정한 그룹 이름 등을 넣을 수 있다. */

					/* Make P2P group that bind all list from above.
					Input parameter: Client list that bind P2P group, custom data
					(Custom data is sening together when send event about binding P2P gorup to client. For example, team identification, group name that assigned by developer. */
					g_groupHostID = srv->CreateP2PGroup(list, listCount, ByteArray());
				}
				break;
			case '2':
				{
					g_S2CProxy.SystemChat(g_groupHostID, RmiContext::ReliableSend,
						L"Hello~~~!");
				}
				break;
			case '3':
				{
					srv->DestroyP2PGroup(g_groupHostID);
				}
				break;
			}
		}


		if ( makeRoom )
		{
			makeRoom = false;
			{
				// 안전한 코딩은 아니지만 샘플이니까 ^^
				// This is not safe coding but it is a sample.
				HostID list[100];

				/* 서버에 접속된 모든 클라 목록을 얻는다.
				(참고: 이렇게 하는 방식보다는 OnClientJoin, OnClientLeave
				이벤트를 활용해서 서버에 접속한 클라이언트 객체 리스트를
				별도 가지게 만드는 것이 좋다. */

				/* Get list of all client which connected at server.
				(Note: We recommend to have an extra connected client object list using OnClientJoin, OnClientLeave event rather than this method. */
				int listCount = srv->GetClientHostIDs( list, 100 );

				/* 위의 목록을 모두 묶은 P2P 그룹을 만든다.
				입력 파라메터: P2P 그룹을 묶을 클라 목록, 커스텀 데이터
				(커스텀 데이터는, P2P 그룹을 묶었다는 이벤트가 클라에 전송
				되면서 같이 가는 데이터이다. 예를 들어 팀 구별이라든지,
				개발자가 지정한 그룹 이름 등을 넣을 수 있다. */

				/* Make P2P group that bind all list from above.
				Input parameter: Client list that bind P2P group, custom data
				(Custom data is sening together when send event about binding P2P gorup to client. For example, team identification, group name that assigned by developer. */
				g_groupHostID = srv->CreateP2PGroup( list, listCount, ByteArray() );
			}
		}
		// CPU 점유를 100% 차지하지 않게 하기 위함
		// To prevent 100% occupation rate of CPU
		Sleep(10); 
	}
out:
	delete srv;

	return 0;
}
