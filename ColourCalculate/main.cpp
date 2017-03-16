#include <iostream>
#include <string>
#include "KColour.h"

using namespace std;

int main()
{
    KColour col1 = KColourGreen;
    KColour col2("#AA0000FF");
    KColour col3("0000FF");
    KColour col4("AA0000FF");
    KColour col5(255,233,13);
    KColour col6(100,255,233,13);

    cout << col1.toWebHex() << "\n";
    cout << col2.toWebHex() << "\n";
    cout << col3.toWebHex() << "\n";
    cout << col4.toWebHex() << "\n";
    cout << col5.toWebHex() << "\n";
    cout << col6.toWebHex() << "\n";
    cout << "----\n";
    col1 = col1.darken(10.0);
    cout << col1.toWebHex() << "\n";
    col1 = col1.lighten(10.0);
    cout << col1.toWebHex() << "\n";
    return 0;
}
