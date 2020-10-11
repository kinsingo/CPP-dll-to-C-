#include <windows.h>
#include <stdio.h>

typedef struct typeExp
{
    char strTest[128];
    int intTest;
    BYTE byteTest[64];
    UINT uintTest[4];
} typeExp;


extern "C" __declspec(dllexport) void calltest(void);
extern "C" __declspec(dllexport) int intReturn(int n_in);
extern "C" __declspec(dllexport) char* strReturn(char* strTemp);
extern "C" __declspec(dllexport) int stReturn(typeExp* strTemp);
extern "C" __declspec(dllexport) int aryReturn(int* n_array);
extern "C" __declspec(dllexport) int* intpReturn();
extern "C" __declspec(dllexport) char* stringReturn();
