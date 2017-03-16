#pragma once
#ifndef COLOURCLOCK_loader
#define COLOURCLOCK_loader 1

#define XmlCheck  temp[1] != '!' && temp[1] != '?'

#include <Windows.h>
#include <GdiPlus.h>
#include <fstream>
#include <string>
#include <sstream>

using namespace Gdiplus;
using namespace std;

struct ClockColour {
	unsigned int x,y,width,height;
	byte type;
	Color colour;
};

void ImportSettings();
void ImportSettings(std::string location);
void ExportSettings();
void ExportSettings(std::string location);

Color ToColour(byte i);

extern ClockColour colour[4];
extern string DisplayString;
extern bool UpdateIcon;
extern byte WindowAppreal;
extern ClockColour WindowStyle;
extern unsigned int OutlineThickness;
extern Color OutlineColour;
extern bool AlwaysAbove;
extern bool InitialImportComplete;

void SetValue(string& outputObject, string value);
void SetValue(unsigned int& outputObject, string value);
void SetValue(bool& outputObject, string value);
void SetValue(Color& outputObject, string value);
void SetValue(byte& outputObject, string value);
void SetValue(ClockColour& outputObject, string value);
byte FromShape(string type);
std::string tolower(string value);

#endif