#pragma once
#ifndef COLOURCLOCK_window
#define COLOURCLOCK_window 1

#define BTN_QUIT 42
#define BTN_MIN 43

#include <Windows.h>
#include <GdiPlus.h>
#include "ColourClock.h"
#include "SettingsLoader.h"

#pragma comment (lib,"Gdiplus.lib")
using namespace Gdiplus;

void Initialise();
void WindowCreate(HINSTANCE, HINSTANCE, PSTR, INT);
LRESULT CALLBACK WindowEvents(HWND, UINT, WPARAM, LPARAM);
void WindowPaint(HDC&);
void WindowInvalidate();
void WindowSetSize(unsigned int& x, unsigned int& y, unsigned int& width, unsigned int& height);
void WindowDrawIcon();
void DrawShape(Graphics& gfx, Color& colour, unsigned int& x, unsigned int& y, unsigned int& width, unsigned int& height, unsigned int& thickness, byte& shape);

#endif