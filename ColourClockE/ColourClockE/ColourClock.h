#pragma once
#ifndef COLOURCLOCK_base
#define COLOURCLOCK_base 1

#include <Windows.h>
#include <time.h>
#include <string>
#include <sstream>

#define TimeInterval 300000

extern int _lights[4];

void SetCurrent();
void IncrementTime();
void BeginCounter(HWND, void (*)());
void CALLBACK TimerCall(HWND hwnd,UINT uMsg,UINT_PTR idEvent,DWORD dwTime);
std::string ClockTitle(std::string format);

#endif