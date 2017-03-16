/*
ColourClock Windows CPP Version
Build Codename:	ColourClockE
Build:			XP34
Version:		1.0.1
Authour:		Matthew Knox
Copyright:		Program:
				Matthew Knox 2013-Present
				Concept and Origional Idea:
				Ralph Knox 2013-Present
*/

#include "Startup.h"

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE h, PSTR p, INT iCmdShow)
{
	Initialise();
	WindowCreate(hInstance,h,p,iCmdShow);
	return 0;
}