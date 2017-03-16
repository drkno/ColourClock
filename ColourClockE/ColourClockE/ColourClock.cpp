#include "ColourClock.h"

int _lights[4] = {0,0,0,0};

void SetCurrent()
{
	time_t rawtime;
    struct tm * timeinfo;
    time ( &rawtime );
    timeinfo = localtime (&rawtime);

	double colValue = timeinfo->tm_min;
    switch ((int)(colValue / 15))
    {
        case 0: _lights[2] = 0; _lights[3] = (int)(colValue / 5.0); break;
        case 1: _lights[2] = 1; _lights[3] = (int)(colValue / 5.0) - 3; break;
        case 2: _lights[2] = 2; _lights[3] = (int)(colValue / 5.0) - 6; break;
        default: _lights[2] = 3; _lights[3] = (int)(colValue / 5.0) - 9; break;
    }

	colValue = timeinfo->tm_hour - ((timeinfo->tm_hour >= 12) ? 12 : 0);
    switch ((int)(colValue / 3))
    {
        case 0: _lights[0] = 0; _lights[1] = colValue; break;
        case 1: _lights[0] = 1; _lights[1] = colValue - 3; break;
        case 2: _lights[0] = 2; _lights[1] = colValue - 6; break;
        default: _lights[0] = 3; _lights[1] = colValue - 9; break;
    }
}

void IncrementTime()
{
	for (int i = 3; i >= 0; i--)
    {
        _lights[i] = (_lights[i] < ((i % 2 == 0) ? 3 : 2)) ? _lights[i]+1 : 0;
        if (_lights[i] != 0) break;
    }
}

void (*timerFunction)();
void BeginCounter(HWND hWnd, void (*timerCall)())
{
	timerFunction = timerCall;
	time_t rawtime;
    struct tm * timeinfo;
    time ( &rawtime );
    timeinfo = localtime (&rawtime);
	unsigned int difference = TimeInterval - ((timeinfo->tm_min%(TimeInterval/(60*1000)))*(60*1000) + (timeinfo->tm_sec*1000));
	SetTimer(hWnd,0,difference,TimerCall);
}

void CALLBACK TimerCall(HWND hwnd,UINT uMsg,UINT_PTR idEvent,DWORD dwTime)
{
	IncrementTime();
	timerFunction();
	SetTimer(hwnd,0,TimeInterval,TimerCall);
}


std::string ClockTitle(std::string format)
{
	time_t rawtime;
    struct tm * timeinfo;
    time ( &rawtime );
    timeinfo = localtime (&rawtime);

	//%time,%24time,%ampm
	while(format.find("%ampm") != std::string::npos)
	{
		format = format.replace(format.find("%ampm"),5,(timeinfo->tm_hour >= 12)?"pm":"am");
	}

    int min = (_lights[2]*3 + _lights[3])*5;
    int hr = ((_lights[0]*3) + _lights[1]);

	std::string time = ":";
	time += (min < 10)?"0":"";
	std::stringstream ss;
	ss << min;
	time += ss.str();
	ss.str(std::string());
	ss << ((timeinfo->tm_hour>=12)?hr+12:hr);
	std::string time24 = ss.str() + time;
	ss.str(std::string());
	ss << hr;
	time = ss.str() + time;
	
	while(format.find("%time") != std::string::npos)
	{
		format = format.replace(format.find("%time"),5,time);
	}

	while(format.find("%24time") != std::string::npos)
	{
		format = format.replace(format.find("%24time"),7,time24);
	}
	return format;
}

