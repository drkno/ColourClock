/*
 * COLOUR CLOCK C++
 * GUI Version by Matthew 2.0.2 Alpha
 * Copyright Matthew Knox 2012. All rights reserved.
 * Last Edit: 11/10/12
*/

#include <stdafx.h>
#include <string>
#include <fstream>
#include <sstream>
#include <windows.h>
#include <time.h>
#include <objidl.h>
#include <gdiplus.h>

#define BUTTON_QUIT 101
#define BUTTON_MINI 101

using namespace std;
using namespace Gdiplus;

#pragma comment (lib,"Gdiplus.lib")

int _lights[4];
Color Background;
Color Colours[4];
bool FirstRun;
bool TaskbarTime;
int Shape;
int Rad[4];
Point WindSize;
Point Xy[4];
HWND thisHandle;
HDC thisHDC;
HINSTANCE thisHinst;

void ColourClock();
void LoadConfig(string);
void ColourClockLoad();
void ColourClockPaint();
void ColourClockMouseDown();
void ButtonExitClick();
void ButtonMinimiseClick();
void SetVals();
void DisplayTime();
void SetWindowTitle();
void SaveFile(string filename);
void Increment();
DWORD WINAPI TimerFunction(LPVOID arg);
bool fexists(const char *);
int Split(string contents, int index, char splitChar);

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE, PSTR, INT iCmdShow)
{
	   HWND                hWnd;
	   MSG                 msg;
	   WNDCLASS            wndClass;
	   GdiplusStartupInput gdiplusStartupInput;
	   ULONG_PTR           gdiplusToken;

	   GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	   wndClass.style          = CS_HREDRAW | CS_VREDRAW;
	   wndClass.lpfnWndProc    = WndProc;
	   wndClass.cbClsExtra     = 0;
	   wndClass.cbWndExtra     = 0;
	   wndClass.hInstance      = thisHinst = hInstance;
	   wndClass.hIcon          = LoadIcon(NULL, IDI_APPLICATION);
	   wndClass.hCursor        = LoadCursor(NULL, IDC_ARROW);
	   wndClass.hbrBackground  = (HBRUSH)GetStockObject(BLACK_BRUSH);
	   wndClass.lpszMenuName   = NULL;
	   wndClass.lpszClassName  = TEXT("ColourClock");
   
	   RegisterClass(&wndClass);
	   hWnd = CreateWindow(TEXT("ColourClock"),TEXT("Colour Clock"),WS_POPUP|WS_MINIMIZEBOX,0,0,50,50,NULL,NULL,hInstance,NULL);
	   ShowWindow(hWnd, iCmdShow);
	   UpdateWindow(hWnd);
	   
	   thisHandle = hWnd;
	   ColourClock();

	   while(GetMessage(&msg, NULL, 0, 0))
	   {
			  TranslateMessage(&msg);
			  DispatchMessage(&msg);
	   }
   
	   GdiplusShutdown(gdiplusToken);
	   return msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	   PAINTSTRUCT  ps;
   
	   switch(message)
	   {
			case WM_PAINT:
				  thisHDC = BeginPaint(hWnd, &ps);
				  ColourClockPaint();
				  EndPaint(hWnd, &ps);
				  return 0;
			case WM_DESTROY:
				  PostQuitMessage(0);
				  return 0;	
			case WM_LBUTTONDOWN:
				  ReleaseCapture();
				  SendMessage(hWnd, 0xA1, 0x2, 0);
				  return 0;
			case WM_CREATE:
				InvalidateRect(thisHandle,NULL,false);
				return 0;
			case WM_COMMAND:
				if(LOWORD(wParam) == BUTTON_QUIT){
					PostQuitMessage(0);
				} else if(LOWORD(wParam) == BUTTON_MINI){
					ShowWindow(hWnd,SW_SHOWMINIMIZED);
				}
				return 0;
			default:
				  return DefWindowProc(hWnd, message, wParam, lParam);
	   }
}

void ColourClock()
{
	LoadConfig("");
	CreateWindowEx(NULL,"Button","X",BS_FLAT | WS_CHILD | WS_VISIBLE,WindSize.X-10,0,10,10,thisHandle,(HMENU)BUTTON_QUIT,thisHinst,0);
	CreateWindowEx(NULL,"Button","-",BS_FLAT | WS_CHILD | WS_VISIBLE,WindSize.X-20,0,10,10,thisHandle,(HMENU)BUTTON_MINI,thisHinst,0);
	ColourClockLoad();
}

void ColourClockLoad()
{
	time_t rawtime;
    struct tm * timeinfo;
    time ( &rawtime );
    timeinfo = localtime ( &rawtime );

    int colValue = timeinfo->tm_min;
    switch ((colValue / 15))
	{
        case 0: _lights[2] = 1; _lights[3] = (int)(colValue / 5.0) + 1; break;
        case 1: _lights[2] = 2; _lights[3] = (int)(colValue / 5.0) - 2; break;
        case 2: _lights[2] = 3; _lights[3] = (int)(colValue / 5.0) - 5; break;
        default: _lights[2] = 4; _lights[3] = (int)(colValue / 5.0) - 8; break;
    }

    colValue = timeinfo->tm_hour - ((timeinfo->tm_hour >= 12) ? 12 : 0);
    switch ((colValue / 3))
    {
        case 0: _lights[0] = 1; _lights[1] = colValue + 1; break;
        case 1: _lights[0] = 2; _lights[1] = colValue - 2; break;
        case 2: _lights[0] = 3; _lights[1] = colValue - 5; break;
        default: _lights[0] = 4; _lights[1] = colValue - 8; break;
    }

    SetWindowTitle();
	CreateThread(NULL,0,TimerFunction,NULL,0,NULL);
}

void ColourClockPaint()
{
    DisplayTime();
}

void ButtonExitClick()
{
	PostMessage(thisHandle, WM_QUIT, 0, 0);
}

void ButtonMinimiseClick()
{
	ShowWindow(thisHandle,SW_SHOWMINIMIZED);
}

void SetVals()
{
	HBRUSH brush = CreateSolidBrush(RGB(Background.GetRed(), Background.GetGreen(), Background.GetBlue()));
    SetClassLongPtr(thisHandle, GCLP_HBRBACKGROUND, (LONG)brush);

	SetWindowPos(thisHandle,HWND_TOP,0,0,WindSize.X,WindSize.Y,SWP_NOMOVE);

    if (FirstRun)
    {
        FirstRun = false;
        if (
            MessageBox(NULL,"Colour Clock comes with no warantee and it is entirely\nyour own fault if it does something bad to your computer.\nDo you agree to these terms?",
                "Colour Clock", MB_YESNO | MB_ICONINFORMATION) == IDNO)
        {
            PostMessage(thisHandle, WM_QUIT, 0, 0);
        }
        SaveFile("");
    }
}

void DisplayTime()
{
	Graphics _paper(thisHDC);
	for (int i = 0; i < 4; i++)
	{
		Color col = Color::Black;
		switch (_lights[i])
		{
			case 1:
				col = Colours[0];
				break;
			case 2:
				col = Colours[1];
				break;
			case 3:
				col = Colours[2];
				break;
			case 4:
				col = Colours[3];
				break;
		}
		switch (Shape)
		{
			case 0:
				_paper.FillEllipse(new SolidBrush(col), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
				break;
			case 1:
				_paper.FillRectangle(new SolidBrush(col), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
				break;
			case 2:
				_paper.DrawEllipse(new Pen(col, 3), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
				break;
			case 3:
				_paper.DrawRectangle(new Pen(col, 3), Xy[i].X, Xy[i].Y, Rad[i], Rad[i]);
				break;
		}
	}
}

void SetWindowTitle()
{
	time_t rawtime;
    struct tm * timeinfo;
    time ( &rawtime );
    timeinfo = localtime (&rawtime);
	
	string timeDisp = "Colour Clock";
	if (TaskbarTime)
	{
		stringstream ss,s1; 
		ss << ((_lights[0] - 1)*3 + (_lights[1] - 1)) + ((timeinfo->tm_hour >= 12)?12:0);
		s1 << ((_lights[2] - 1)*3 + (_lights[3] - 1))*5;
		timeDisp = ss.str() + ":" + (((((_lights[2] - 1)*3 + (_lights[3] - 1))*5) < 10)?"0":"") + s1.str() + ((timeinfo->tm_hour >= 12)?" pm":" am") + " - " + timeDisp;
	}

	SetWindowText(thisHandle,timeDisp.c_str());
	InvalidateRect(thisHandle,NULL,false);
}

DWORD WINAPI TimerFunction(LPVOID arg)
{
	time_t rawtime;
	time(&rawtime);
    struct tm * timeinfo = localtime (&rawtime);

	int min = timeinfo->tm_min;
	min = (min/5)*5;
	if(min > 5) { min -= 5; }
	min = (((5 - min) * 60) - timeinfo->tm_sec) * 1000;



	Sleep(min);
	while(1){
		Increment();
		Sleep(300000);
	}
	return 0;
}

void Increment()
{
	for (int i = 3; i >= 0; i--)
	{
		_lights[i] = (_lights[i] < ((i % 2 == 0) ? 4 : 3)) ? _lights[i] + 1 : 1;
		if (_lights[i] != 1) break;
	}

	SetWindowTitle();
}

bool fexists(const char *filename)
{
	ifstream ifile(filename);
	return ifile;
}

void LoadConfig(string filename)
{
    if (filename == "")
    {
		char const* tmp = getenv("APPDATA");
		if ( tmp == NULL ) {
			return;
		} else {
			filename = tmp;
			filename += "\\ColourClock\\settings.ini";
		}

		if (!fexists(filename.c_str()))
        {
            Xy[0].X = Xy[1].X = 160;
            Xy[0].Y = Xy[2].Y = 43;
            Xy[1].Y = Xy[3].Y = 110;
            Xy[2].X = Xy[3].X = 229;
            Rad[0] = Rad[1] = Rad[2] = Rad[3] = 45;
                    
            Colours[0] = Color::Red;
            Colours[1] = Color::LawnGreen;
            Colours[2] = Color::Yellow;
            Colours[3] = Color::Blue;
            Background = Color::Black;

            Shape = 0;
            WindSize.X = 425;
            WindSize.Y = 210;
            TaskbarTime = FirstRun = true;
            SetVals();
            return;
        } else {
			try
			{
				ifstream inputStream(filename.c_str());
				string tempStr;
				getline(inputStream,tempStr); getline(inputStream,tempStr); getline(inputStream,tempStr); getline(inputStream,tempStr);
                
				int temp;
				for (int i = 0; i < 4; i++)
				{
					if(inputStream >> temp){ Xy[i].X = temp; }
					if(inputStream >> temp){ Xy[i].Y = temp; }
					if(inputStream >> temp){ Rad[i] = temp; }
					if(inputStream >> temp){ Colours[i].SetValue(temp); }
				}

				if(inputStream >> temp){ Background.SetValue(temp); }
				if(inputStream >> temp){ Shape = temp; }
				if(inputStream >> temp){ WindSize.X = temp; }
				if(inputStream >> temp){ WindSize.Y = temp; }

				inputStream >> tempStr;
				if(tempStr == "True" || tempStr == "true" || tempStr == "1") { TaskbarTime = true; } else { TaskbarTime = false; }
				inputStream >> tempStr;
				if(tempStr == "True" || tempStr == "true" || tempStr == "1") { FirstRun = true; } else { FirstRun = false; }

				inputStream >> tempStr;
				if (tempStr != "")
				{
					Point pos(0,0);
					pos.X = Split(tempStr,0,',');
					pos.Y = Split(tempStr,1,',');
					if (pos.X != pos.Y != -1)
					{
						SetWindowPos(thisHandle,HWND_TOP,pos.X,pos.Y,0,0,SWP_NOSIZE);
					}
				}

				inputStream.close();
				SetVals();
			}
			catch (string ex)
			{
				MessageBox(NULL,("Settings could not be loaded due to an error. Error is as follows:\n" + ex).c_str(),"Colour Clock", MB_OK | MB_ICONEXCLAMATION);
			}
		}
    }
}

int Split(string contents, int index, char splitChar){
	stringstream ss;
	ss << contents;
	while(!ss.eof()){
		if(index == 0){
			if(ss >> index){
				break;
			} else {
				index = -1;
				break;
			}
		} else {
			getline(ss,contents,splitChar);
			index--;
		}
	}
	return index;
}

void SaveFile(string filename)
{
    try
    {
        if (filename == "")
        {
			char const* tmp = getenv( "APPDATA" );
			if ( tmp == NULL ) {
				return;
			} else {
				filename = tmp;
				filename += "\\ColourClock\\";
			}

			if (!(CreateDirectory(filename.c_str(), NULL) || ERROR_ALREADY_EXISTS == GetLastError()))
			{
				return;
			}

			filename += "settings.ini";
        }
		ARGB argbval = Background.GetValue();

		ofstream saveSettings(filename);
        saveSettings << "Colour Clock Configuration File.\r\nIt is advisable not to change this file.\r\n##########################\r\n\r\n";
        int temp;
		for (int i = 0; i < 4; i++)
        {
			temp = Colours[i].GetValue();
			saveSettings << Xy[i].X << "\r\n" << Xy[i].Y << "\r\n" << Rad[i] << "\r\n" << temp << "\r\n";
        }

		RECT rectangle;
		GetWindowRect(thisHandle,&rectangle);
		temp = Background.GetValue();
		saveSettings << temp << "\r\n" << Shape << "\r\n" << WindSize.X << "\r\n" << WindSize.Y << "\r\n" <<
                            ((TaskbarTime)?"True":"False") << "\r\n" << ((FirstRun)?"True":"False") << "\r\n" << rectangle.left << "," << rectangle.top;
        saveSettings.close();
    }
    catch (string ex)
    {
		MessageBox(NULL,("Settings could not be saved to file due to an error. Error is as follows:\n" + ex).c_str(),"Colour Clock", MB_OK | MB_ICONEXCLAMATION);
    }
}