#include <iostream>
#include "CClock.h"

using namespace std;

int main(int argc, char** argv)
{
    CClock clock;
    clock.setNow();
    cout << "[" << clock.getColour(0) << "," << clock.getColour(1) << "," << clock.getColour(2) << "," << clock.getColour(3) << "]\n";
    return 0;
}
