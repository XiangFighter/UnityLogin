// LoginServer.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include<stdio.h>
#include<winsock.h>
#include <iostream>
#pragma comment (lib, "ws2_32.lib") 
using namespace std;

int main()
{
	WSADATA wsaData;
	WSAStartup(MAKEWORD(2, 2), &wsaData);
	SOCKET servSock = socket(PF_INET, SOCK_STREAM, 0);
	SOCKADDR_IN sockAddr;
	memset(&sockAddr, 0, sizeof(sockAddr));
	sockAddr.sin_family = PF_INET;
	sockAddr.sin_port = htons(8080);
	sockAddr.sin_addr.s_addr = inet_addr("127.0.0.1");
	bind(servSock, (SOCKADDR*)&sockAddr, sizeof(SOCKADDR));
	printf("绑定套接字成功\n");
	listen(servSock, 20);
	printf("服务器已经进入监听状态\n");
	SOCKADDR clntAddr;
	int nSize = sizeof(SOCKADDR);
	SOCKET clntSock = accept(servSock, (SOCKADDR*)&clntAddr, &nSize);
	printf("accept函数执行完毕开始接收用户输入\n");

	printf("\n您好，我是服务器\n");
	char szBuffer[MAXBYTE] = { 0 };
	recv(clntSock, szBuffer, MAXBYTE, NULL);
	
	printf("\n服务器接收到的消息: %s\n", szBuffer);


	char str[] = "the hello from Server";
	send(clntSock, str, strlen(str), NULL);
	printf("\n执行到了这里\n");

	//关闭套接字
	closesocket(clntSock);
	closesocket(servSock);
	//终止 DLL 的使用
	WSACleanup();
	system("PAUSE");
	return 0;
}