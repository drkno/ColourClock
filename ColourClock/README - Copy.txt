Colour Clock
--------------------------------------------
Version:	3.0a win32cpp
Authour:	Matthew Knox

Note
--------------------------------------------
This is an alpha quality program. It has issues that you may have to suffer
through even though an attempt has been made to remove them.

Feature Changes
--------------------------------------------
Complete rewrite in c++.
Uses xml configuration file located in local rather than roaming appdata.
Extra configuration options.

Known Issues
--------------------------------------------
Level	Description
Medium	GDI+ memory leak. Some HDC objects arn't being deallocated
	for some reason. This isn't a huge issue except if using the clock
	long term.
Minor	Disabling update icon does not display the correct icon.
Medium	Configuration needs more checking. Buffer overflows and crashing
	issues can be caused with incorrect configuration.
Minor	Window size changes by itself when chaging the window style.
Minor	Problematic configuration can lock up the main thread.