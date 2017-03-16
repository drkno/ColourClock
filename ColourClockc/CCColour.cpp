#include "CCColour.h"

CCColour::CCColour()
{
    CCColour(0,true);
}

CCColour::CCColour(unsigned int value, bool isHour)
{
    setIsHour(isHour);
    setValue(value);
}

void CCColour::setValue(unsigned int value)
{
    _currentValue = value%((getIsHour())?hourcc:mincc);
}

unsigned int CCColour::getValue()
{
    return _currentValue;
}

bool CCColour::incrementValue()
{
    setValue((getValue()+1)%((getIsHour())?hourcc:mincc));
    return getValue() == 0;
}

void CCColour::setIsHour(bool isHour)
{
    _isHour = isHour;
}

bool CCColour::getIsHour()
{
    return _isHour;
}
