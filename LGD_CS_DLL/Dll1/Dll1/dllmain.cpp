#include "dllmain.h"

void calltest(void) {
    Sleep(10000);
}

int intReturn(int n_in) {
    n_in++;
    return n_in;
}

char* strReturn(char* strTemp) {
    static char strTemp2[128] = { 0, };
    sprintf_s(strTemp2, sizeof(strTemp2), "%s DLL에서 추가", strTemp);
    return strTemp2;
}


int stReturn(typeExp* strTemp) {

    strTemp->byteTest[0] = 1;
    strTemp->intTest = strTemp->intTest + 111;
    sprintf_s(strTemp->strTest, 128, "%s DLL에서 추가, 구조체", strTemp->strTest);
    strTemp->uintTest[0] = 1;

    return 1;
}

int aryReturn(int* n_array) {

    for (int i = 0; i < 10; ++i) {
        n_array[i] = i * 100 + 10;
    }
    return 1;
}

int* intpReturn() {
    static char strTemp2[128] = { 0, };
    sprintf_s(strTemp2, sizeof(strTemp2), "DLL에서 작성 intp");
    return (int*)strTemp2;
}

char* stringReturn() {
    static char strTemp2[128] = { 0, };
    sprintf_s(strTemp2, sizeof(strTemp2), "DLL에서 작성 intp");
    return strTemp2;
}