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

#ifndef Include_KColour
#define Include_KColour 1.0

#include <string>

using namespace std;

class KColour {
    public:
    class InvalidColourException
    {
        protected:
        string description;
        int code;

        public:
        InvalidColourException();
        InvalidColourException(string desc);
        InvalidColourException(int ecode);
        InvalidColourException(string desc, int ecode);
        string getDescription();
        int getErrorCode();
    };

    KColour();
    KColour(string hex);
    KColour(int red, int green, int blue);
    KColour(int alpha, int red, int green, int blue);
    void setHex(string hex);
    void setRGB(int red, int green, int blue);
    void setARGB(int alpha, int red, int green, int blue);
    KColour darken(double percentage);
    KColour lighten(double percentage);
    string toHex();
    string toHexA();
    string toWebHex();
    string toWebHexA();

    int getAlpha();
    string getAlphaHex();
    int getRed();
    string getRedHex();
    int getGreen();
    string getGreenHex();
    int getBlue();
    string getBlueHex();

    void setAlpha(int alpha);
    void setAlpha(string alpha);
    void setRed(int red);
    void setRed(string red);
    void setGreen(int green);
    void setGreen(string green);
    void setBlue(int blue);
    void setBlue(string blue);

    protected:
    int a, r, g, b;

    int toNumberValue(string hexCode);
    string toHexValue(int numberCode);
};

#define KColourWhite KColour(255,255,255)
#define KColourBlack KColour(0,0,0)
#define KColourRed KColour(255,0,0)
#define KColourGreen KColour(0,255,0)
#define KColourBlue KColour(0,0,255)

#endif
