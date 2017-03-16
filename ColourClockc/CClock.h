#pragma once
#ifndef include_cclock
#define include_cclock 1.0

#include <ctime>
#include <vector>
#include "CCColour.h"

class CClock
{
public:
    CClock();
    unsigned int getColour(unsigned int);
    void increment();
    void setNow();
    void setColour(unsigned int, unsigned int);
    std::vector<CCColour> getCCColours();

protected:
    std::vector<CCColour> _clockColours;
};

#endif
