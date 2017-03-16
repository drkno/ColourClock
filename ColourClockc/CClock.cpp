#include "CClock.h"

CClock::CClock()
{
    for(unsigned int i = 0; i < 4; i++)
    {
        CCColour ccol(0,!((bool)(i%2)));
    }
}

unsigned int CClock::getColour(unsigned int pos)
{
    return _clockColours[pos%_clockColours.size()].getValue();
}

void CClock::increment()
{
    for(unsigned int i = 0; i < _clockColours.size(); i++)
    {
        if(!_clockColours[i].incrementValue()) return;
        if(i == _clockColours.size()-1) i = -1;
    }
}

void CClock::setNow()
{

}

void CClock::setColour(unsigned int pos, unsigned int val)
{
    _clockColours[pos%_clockColours.size()].setValue(val);
}

std::vector<CCColour> CClock::getCCColours()
{
    return _clockColours;
}
