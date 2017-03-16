#include "SettingsLoader.h"

// Settings vars
ClockColour colour[4];
string DisplayString = "%24time %ampm - Colour Clock";
bool UpdateIcon = true, AlwaysAbove = false, InitialImportComplete = false;
byte WindowAppreal = 0;
ClockColour WindowStyle;
unsigned int OutlineThickness = 5;
Color OutlineColour = Color::Black;

// Retvars
const char * DefaultLocation;

void SetDefaultLocation()
{
	DefaultLocation = getenv("APPDATA");
	strcat((char*)DefaultLocation,"\\ColourClock\\config.xml");
}

void ImportSettings()
{
	if(DefaultLocation == "" || DefaultLocation == NULL)
	{
		SetDefaultLocation();
	}
	ImportSettings(DefaultLocation);
}

void ImportSettings(std::string location)
{
	// Set Default Settings in case not in config
	/*for(byte i = 0; i < 4; i++)
	{
		colour[i].height = colour[i].width = 50;
		colour[i].type = 2;
		colour[i].x = ((i-2)>=0)?52:0;
		colour[i].y = ((i+1)%2==0)?52:0;
	}
	colour[0].colour = Color::Red;
	colour[1].colour = Color::LawnGreen;
	colour[2].colour = Color::Yellow;
	colour[3].colour = Color::Blue;
	WindowStyle.colour = Color::Black;
	WindowStyle.width = 102;
	WindowStyle.height = 102;
	WindowStyle.x = WindowStyle.y = 50;*/

	ifstream in(location.c_str());
	if(!in) return;
	string temp;
	while(!in.eof())
	{
		getline(in,temp);
		temp = tolower(temp);
		if(!(temp.length() > 2 && XmlCheck)) { continue; }

		if(temp.find("<style>") != string::npos) 
		{
			temp = temp.substr(temp.find('>')+1);
			temp = temp.substr(0,temp.find('<'));
			if(temp == "borderless") { WindowAppreal = 0; }
			if(temp == "windowed") { WindowAppreal = 1; }
			if(temp == "simple") { WindowAppreal = 2; }
			if(temp == "tool") { WindowAppreal = 3; }
			continue; 
		}

		if(temp.find("<alwaysontop>") != string::npos) { SetValue(AlwaysAbove,temp); continue; }
		if(temp.find("<outlinecolour>") != string::npos) { SetValue(OutlineColour,temp); continue; }
		if(temp.find("<outline>") != string::npos) { SetValue(OutlineThickness,temp); continue; }
		if(temp.find("<background>") != string::npos) { SetValue(WindowStyle.colour,temp); continue; }
		if(temp.find("<size0>") != string::npos) { SetValue(WindowStyle,temp); continue; }
		if(temp.find("<updateicon>") != string::npos) { SetValue(UpdateIcon,temp); continue; }
		if(temp.find("<displaystring>") != string::npos) { SetValue(DisplayString,temp); continue; }
		if(temp.find("<shape4>") != string::npos) { SetValue(colour[3].type,temp); continue; }
		if(temp.find("<size4>") != string::npos) { SetValue(colour[3],temp); continue; }
		if(temp.find("<colour4>") != string::npos) { SetValue(colour[3].colour,temp); continue; }
		if(temp.find("<shape3>") != string::npos) { SetValue(colour[2].type,temp); continue; }
		if(temp.find("<size3>") != string::npos) { SetValue(colour[2],temp); continue; }
		if(temp.find("<colour3>") != string::npos) { SetValue(colour[2].colour,temp); continue; }
		if(temp.find("<shape2>") != string::npos) { SetValue(colour[1].type,temp); continue; }
		if(temp.find("<size2>") != string::npos) { SetValue(colour[1],temp); continue; }
		if(temp.find("<colour2>") != string::npos) { SetValue(colour[1].colour,temp); continue; }
		if(temp.find("<shape1>") != string::npos) { SetValue(colour[0].type,temp); continue; }
		if(temp.find("<size1>") != string::npos) { SetValue(colour[0],temp); continue; }
		if(temp.find("<colour1>") != string::npos) { SetValue(colour[0].colour,temp); continue; }
	}
	in.close();
	InitialImportComplete = true;
	ExportSettings();
}

void SetValue(string& outputObject, string value)
{
	value = value.substr(value.find('>')+1);
	value = value.substr(0,value.find('<'));
	outputObject = value;
}
void SetValue(unsigned int& outputObject, string value)
{
	value = value.substr(value.find('>')+1);
	value = value.substr(0,value.find('<'));
	unsigned long num;
	stringstream ss;ss << value;
	ss >> num;
	if(num >= 0){
		outputObject = num;
	}
}
void SetValue(bool& outputObject, string value)
{
	value = value.substr(value.find('>')+1);
	value = value.substr(0,value.find('<'));
	for(byte i = 0; i < value.length(); i++){
		value[i] = tolower(value[i]);
	}
	outputObject = ((value == "1" || value == "true")?true:false);
}
void SetValue(Color& outputObject, string value)
{
	value = value.substr(value.find('>')+1);
	value = value.substr(0,value.find('<'));
	unsigned int acolour;
	SetValue(acolour,value);
	outputObject.SetValue(acolour);
}
void SetValue(byte& outputObject, string value)
{
	value = value.substr(value.find('>')+1);
	value = value.substr(0,value.find('<'));
	outputObject = FromShape(value);
}
void SetValue(ClockColour& outputObject, string value)
{
	value = value.substr(value.find('>')+1);
	value = value.substr(0,value.find('<'));

	unsigned int temp;
	SetValue(temp,value.substr(0,value.find(',')));
	outputObject.x = temp;
	value = value.substr(1);
	value = value.substr(value.find(','));

	temp = 0;
	SetValue(temp,value.substr(1,value.find(',',1)-1));
	outputObject.y = temp;
	value = value.substr(1);
	value = value.substr(value.find(','));

	temp = 0;
	SetValue(temp,value.substr(1,value.find(',',1)-1));
	outputObject.width = temp;
	value = value.substr(1);
	value = value.substr(value.find(','));

	temp = 0;
	SetValue(temp,value.substr(1));
	outputObject.height = temp;
}

void ExportSettings()
{
	if(DefaultLocation == "" || DefaultLocation == NULL)
	{
		SetDefaultLocation();
	}
	ExportSettings(DefaultLocation);
}

const char * ToShape(byte type)
{
	switch(type)
	{
	default:
	case 0: return "Elipse";
	case 1: return "OutlinedElipse";
	case 2: return "Rectangle";
	case 3: return "OutlinedRectangle";
	}
}

byte FromShape(string type)
{
	if(type == "elipse" || type == "circle") return 0;
	if(type == "outlinedelipse" || type == "outlinedcircle") return 1;
	if(type == "rectangle" || type == "square") return 2;
	if(type == "outlinedrectangle" || type == "outlinedsquare") return 3;

	return 0;
}

const char * ToStyle(byte style)
{
	switch(style)
	{
	default:
	case 0: return "borderless";
	case 1: return "windowed";
	case 2: return "simple";
	case 3: return "tool";
	}
}

void ExportSettings(std::string location)
{
	if(!InitialImportComplete) return;
	ofstream out(location.c_str());
	if(!out) return;
	out << "<?ColourClock version='1.0'?>\n<!-- This file was generated automatically. It is advised you leave it alone. Colour Clock comes with no warranty at all and what ever happens to it is all your own fault. -->";
	out << "\n\n<settings>";
	for(int i = 1; i <= 4; i++)
	{
		out << "\n\n<!-- Colour " << i << " -->\n<Colour" << i << ">" << colour[(i-1)].colour.GetValue() << "</Colour" << i << ">\n";
		out << "<Size" << i << ">" << colour[(i-1)].x << "," << colour[(i-1)].y << "," << colour[(i-1)].width << "," << colour[(i-1)].height << "</Size" << i << ">\n";
		out << "<Shape" << i << ">" << ToShape(colour[(i-1)].type) << "</Shape" << i << ">";
	}

	out << "\n\n<!-- Taskbar -->\n<DisplayString>" << DisplayString << "</DisplayString>\n";
	out << "<UpdateIcon>" << UpdateIcon << "</UpdateIcon>\n";

	out << "\n\n<!-- Window -->\n<Style>" << ToStyle(WindowAppreal) << "</Style>\n";
	out << "<Size0>" << WindowStyle.x << "," << WindowStyle.y << "," << WindowStyle.width << "," << WindowStyle.height << "</Size0>\n";
	out << "<Background>" << WindowStyle.colour.GetValue() << "</Background>\n";
	out << "<Outline>" << OutlineThickness << "</Outline>\n";
	out << "<OutlineColour>" << OutlineColour.GetValue() << "</OutlineColour>\n";
	out << "<AlwaysOnTop>" << AlwaysAbove << "</AlwaysOnTop>\n";

	out << "\n\n</settings>\n";
	out.close();
}

Color ToColour(byte i)
{
	return colour[i].colour;
}

std::string tolower(string value)
{
	for(unsigned int i = 0; i < value.length(); i++)
	{
		value[i] = tolower(value[i]);
	}
	return value;
}