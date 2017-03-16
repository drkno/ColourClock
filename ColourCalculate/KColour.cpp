/*
KColour 1.0
Matthew Knox
matthew@makereti.co.nz

An extreamly quickly written, OS independant colour class. All references
to the word colour are spelt with a "u", as is done in proper
english.

Copyright Information/Licence:
This work is Copyright Matthew Knox 2013-Present.
You may use it/modify it in any way, under any licence as long as
attribution is present and visible to the end users of the
code/program.
*/

#include "KColour.h"

// InvalidColourException
KColour::InvalidColourException::InvalidColourException(){}
KColour::InvalidColourException::InvalidColourException(string desc)
{
    description = desc;
}

KColour::InvalidColourException::InvalidColourException(int ecode)
{
    code = ecode;
}

KColour::InvalidColourException::InvalidColourException(string desc, int ecode)
{
    description = desc;
    code = ecode;
}

string KColour::InvalidColourException::getDescription()
{
    return description;
}

int KColour::InvalidColourException::getErrorCode()
{
    return code;
}

// KCOLOUR

KColour::KColour()
{
    setARGB(0,0,0,0);
}

KColour::KColour(string hex)
{
    setHex(hex);
}

KColour::KColour(int red, int green, int blue)
{
    setRGB(red,green,blue);
}

KColour::KColour(int alpha, int red, int green, int blue)
{
    setARGB(alpha,red,green,blue);
}

void KColour::setHex(string hex)
{
    if(hex.length() > 0 && hex[0] == '#')
    {
        hex = hex.substr(1);
    }

    if(hex.length() == 6)
    {
        setAlpha(0);
        setRed(hex.substr(0,2));
        setGreen(hex.substr(2,2));
        setBlue(hex.substr(4,2));
    }
    else if(hex.length() == 8)
    {
        setAlpha(hex.substr(0,2));
        setRed(hex.substr(2,2));
        setGreen(hex.substr(4,2));
        setBlue(hex.substr(6,2));
    }
    else
    {
        throw InvalidColourException("Invalid Colour Hex");
    }
}

void KColour::setRGB(int red, int green, int blue)
{
    setARGB(0,red,green,blue);
}

void KColour::setARGB(int alpha, int red, int green, int blue)
{
    setAlpha(alpha);
    setRed(red);
    setGreen(green);
    setBlue(blue);
}

KColour KColour::darken(double percentage)
{
    if(percentage > 100.0 || percentage < 0.0)
    {
        throw InvalidColourException("Invalid Percentage");
    }
    percentage /= 100.0;
    percentage = 1 - percentage;
    int rn = (int)((double)getRed() * percentage);
    int gn = (int)((double)getGreen() * percentage);
    int bn = (int)((double)getBlue() * percentage);

    rn = (rn < 0)?0:rn;
    gn = (gn < 0)?0:gn;
    bn = (bn < 0)?0:bn;
    return KColour(rn,gn,bn);
}

KColour KColour::lighten(double percentage)
{
    if(percentage > 100.0 || percentage < 0.0)
    {
        throw InvalidColourException("Invalid Percentage");
    }
    percentage /= 100.0;
    percentage += 1;
    int rn = (int)((double)getRed() * percentage);
    int gn = (int)((double)getGreen() * percentage);
    int bn = (int)((double)getBlue() * percentage);

    rn = (rn > 255)?255:rn;
    gn = (gn > 255)?255:gn;
    bn = (bn > 255)?255:bn;
    return KColour(rn,gn,bn);
}

string KColour::toHex()
{
    return getRedHex() + getGreenHex() + getBlueHex();
}

string KColour::toHexA()
{
    return getAlphaHex() + toHex();
}

string KColour::toWebHex()
{
    return "#" + toHex();
}

string KColour::toWebHexA()
{
    return "#" + toHexA();
}

int KColour::toNumberValue(string hexCode)
{
    if(hexCode.length() != 2)
    {
        throw InvalidColourException("Invalid Hex Input");
    }
    int result = 0;
    switch(tolower(hexCode[1]))
    {
        case 'f': result += 15; break;
        case 'e': result += 14; break;
        case 'd': result += 13; break;
        case 'c': result += 12; break;
        case 'b': result += 11; break;
        case 'a': result += 10; break;
        case '9': result += 9; break;
        case '8': result += 8; break;
        case '7': result += 7; break;
        case '6': result += 6; break;
        case '5': result += 5; break;
        case '4': result += 4; break;
        case '3': result += 3; break;
        case '2': result += 2; break;
        case '1': result += 1; break;
        case '0': result += 0; break;
        default: throw InvalidColourException("Invalid Hex Input"); break;
    }
    switch(tolower(hexCode[0]))
    {
        case 'f': result += 240; break;
        case 'e': result += 224; break;
        case 'd': result += 208; break;
        case 'c': result += 192; break;
        case 'b': result += 176; break;
        case 'a': result += 160; break;
        case '9': result += 144; break;
        case '8': result += 128; break;
        case '7': result += 112; break;
        case '6': result += 96; break;
        case '5': result += 80; break;
        case '4': result += 64; break;
        case '3': result += 48; break;
        case '2': result += 32; break;
        case '1': result += 16; break;
        case '0': result += 0; break;
        default: throw InvalidColourException("Invalid Hex Input"); break;
    }
    return result;
}

string KColour::toHexValue(int numberCode)
{
    if(numberCode < 0 || numberCode > 255)
    {
        throw InvalidColourException("Invalid Number Input");
    }
    string result = "00";
    int temp = numberCode / 16.0;
    switch(temp)
    {
        case 0: result[0] = '0'; break;
        case 1: result[0] = '1'; break;
        case 2: result[0] = '2'; break;
        case 3: result[0] = '3'; break;
        case 4: result[0] = '4'; break;
        case 5: result[0] = '5'; break;
        case 6: result[0] = '6'; break;
        case 7: result[0] = '7'; break;
        case 8: result[0] = '8'; break;
        case 9: result[0] = '9'; break;
        case 10: result[0] = 'A'; break;
        case 11: result[0] = 'B'; break;
        case 12: result[0] = 'C'; break;
        case 13: result[0] = 'D'; break;
        case 14: result[0] = 'E'; break;
        case 15: result[0] = 'F'; break;
    }
    temp = numberCode % 16;
    switch(temp)
    {
        case 0: result[1] = '0'; break;
        case 1: result[1] = '1'; break;
        case 2: result[1] = '2'; break;
        case 3: result[1] = '3'; break;
        case 4: result[1] = '4'; break;
        case 5: result[1] = '5'; break;
        case 6: result[1] = '6'; break;
        case 7: result[1] = '7'; break;
        case 8: result[1] = '8'; break;
        case 9: result[1] = '9'; break;
        case 10: result[1] = 'A'; break;
        case 11: result[1] = 'B'; break;
        case 12: result[1] = 'C'; break;
        case 13: result[1] = 'D'; break;
        case 14: result[1] = 'E'; break;
        case 15: result[1] = 'F'; break;
    }
    return result;
}

int KColour::getAlpha()
{
    return a;
}

string KColour::getAlphaHex()
{
    return toHexValue(a);
}

int KColour::getRed()
{
    return r;
}

string KColour::getRedHex()
{
    return toHexValue(r);
}

int KColour::getGreen()
{
    return g;
}

string KColour::getGreenHex()
{
    return toHexValue(g);
}

int KColour::getBlue()
{
    return b;
}

string KColour::getBlueHex()
{
    return toHexValue(b);
}

void KColour::setAlpha(int alpha)
{
    if(alpha > 255 || alpha < 0)
    {
        throw InvalidColourException("Invalid Alpha Colour");
    }
    a = alpha;
}

void KColour::setAlpha(string alpha)
{
    try
    {
        if(alpha.length() != 2){ throw InvalidColourException(); }
        setAlpha(toNumberValue(alpha));
    }
    catch (InvalidColourException)
    {
        throw InvalidColourException("Invalid Alpha Colour");
    }
}

void KColour::setRed(int red)
{
    if(red > 255 || red < 0)
    {
        throw InvalidColourException("Invalid Red Colour");
    }
    r = red;
}

void KColour::setRed(string red)
{
    try
    {
        if(red.length() != 2){ throw InvalidColourException(); }
        setRed(toNumberValue(red));
    }
    catch (InvalidColourException)
    {
        throw InvalidColourException("Invalid Red Colour");
    }
}

void KColour::setGreen(int green)
{
    if(green > 255 || green < 0)
    {
        throw InvalidColourException("Invalid Green Colour");
    }
    g = green;
}

void KColour::setGreen(string green)
{
    try
    {
        if(green.length() != 2){ throw InvalidColourException(); }
        setGreen(toNumberValue(green));
    }
    catch (InvalidColourException)
    {
        throw InvalidColourException("Invalid Green Colour");
    }
}

void KColour::setBlue(int blue)
{
    if(blue > 255 || blue < 0)
    {
        throw InvalidColourException("Invalid Blue Colour");
    }
    b = blue;
}

void KColour::setBlue(string blue)
{
    try
    {
        if(blue.length() != 2){ throw InvalidColourException(); }
        setBlue(toNumberValue(blue));
    }
    catch (InvalidColourException)
    {
        throw InvalidColourException("Invalid Blue Colour");
    }
}
