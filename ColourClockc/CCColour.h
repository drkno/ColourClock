#pragma once
#ifndef include_cccolour
#define include_cccolour 1.0

#define hourcc 4
#define mincc 3

class CCColour
{
public:
    CCColour();
    CCColour(unsigned int,bool);
    void setValue(unsigned int);
    unsigned int getValue();
    void setIsHour(bool);
    bool getIsHour();
    bool incrementValue();
protected:
    unsigned int _currentValue;
    bool _isHour;
};

#endif
