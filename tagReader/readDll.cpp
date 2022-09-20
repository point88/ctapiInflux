#include <stdio.h>
#include <Windows.h>
#include <ctapi.h>

HANDLE hCTAPI;

extern "C"
{
	__declspec(dllexport) void __cdecl tag_init()
	{
		hCTAPI = ctOpen(NULL, NULL, NULL, 0);
	}
	__declspec(dllexport) void __cdecl tag_read(char* sProcessValue, int len)
	{
		ctTagRead(hCTAPI, "BLD_W", sProcessValue, len);
	}

	__declspec(dllexport) void __cdecl tag_close()
	{
		ctClose(hCTAPI);
	}

}