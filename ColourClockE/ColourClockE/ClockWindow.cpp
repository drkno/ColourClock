#include "ClockWindow.h"

HWND hWnd;

void Initialise()
{
	ImportSettings();
	SetCurrent();
}

void WindowCreate(HINSTANCE hInstance, HINSTANCE, PSTR, INT iCmdShow)
{
	MSG msg;
	WNDCLASS wndClass;
	GdiplusStartupInput gdiplusStartupInput;
	ULONG_PTR gdiplusToken;

	GdiplusStartup(&gdiplusToken, &gdiplusStartupInput, NULL);

	wndClass.style          = CS_HREDRAW | CS_VREDRAW;
	wndClass.lpfnWndProc    = WindowEvents;
	wndClass.cbClsExtra     = 0;
	wndClass.cbWndExtra     = 0;
	wndClass.hInstance      = hInstance;
	wndClass.hIcon          = NULL;
	wndClass.hCursor        = LoadCursor(NULL, IDC_ARROW);
	wndClass.hbrBackground  = (HBRUSH)GetStockObject(TRANSPARENT);
	wndClass.lpszMenuName   = NULL;
	wndClass.lpszClassName  = TEXT("ColourClock");

	RegisterClass(&wndClass);

	DWORD dwStyle, exdwStyle=NULL;
	switch(WindowAppreal)
	{
	default: 
	case 0: dwStyle = WS_POPUP|WS_MINIMIZEBOX|WS_SYSMENU; break;
	case 1: dwStyle = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX; break;
	case 2: dwStyle = WS_OVERLAPPED; break;
	case 3: dwStyle = WS_SYSMENU; exdwStyle = WS_EX_TOOLWINDOW; break;
	}

	exdwStyle = exdwStyle | WS_EX_TRANSPARENT;
	if(AlwaysAbove){
		exdwStyle = exdwStyle | WS_EX_TOPMOST;
	}

	hWnd = CreateWindowEx(exdwStyle,TEXT("ColourClock"),TEXT("Colour Clock"),dwStyle,WindowStyle.x,WindowStyle.y,WindowStyle.width,WindowStyle.height,NULL,NULL,hInstance,NULL);
	ShowWindow(hWnd, iCmdShow);
	UpdateWindow(hWnd);

	if(WindowAppreal == 0)
	{
		CreateWindow("BUTTON","X", BS_OWNERDRAW | WS_CHILD | WS_VISIBLE, WindowStyle.width-13,0,13,13,hWnd,(HMENU)BTN_QUIT,hInstance,0);
		CreateWindow("BUTTON","-", BS_OWNERDRAW | WS_CHILD | WS_VISIBLE, WindowStyle.width-26,0,13,13,hWnd,(HMENU)BTN_MIN,hInstance,0);
	}

	BeginCounter(hWnd,WindowInvalidate);

	std::stringstream ss; ss << "W:" << WindowStyle.width << " H:" << WindowStyle.height << " X:" << WindowStyle.x << " Y:" << WindowStyle.y;
	MessageBox(NULL,ss.str().c_str(),"",MB_OK);

	while(GetMessage(&msg, NULL, 0, 0))
	{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
	}
   
	GdiplusShutdown(gdiplusToken);
}

LRESULT CALLBACK WindowEvents(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PAINTSTRUCT  ps;
	switch(message)
	{
		case WM_PAINT:
			{
				HDC hdc = BeginPaint(hWnd, &ps);
				WindowPaint(hdc);
				EndPaint(hWnd, &ps);
				return 0;
			}
		case WM_DESTROY:
			{
				if(!InitialImportComplete) return 0;
				RECT rect;
				GetWindowRect(hWnd,&rect);
				WindowStyle.x = rect.left;
				WindowStyle.y = rect.top;
				WindowStyle.width = rect.right-rect.left;
				WindowStyle.height = rect.bottom-rect.top;
				ExportSettings();
				PostQuitMessage(0);
				return 0;
			}
		case WM_LBUTTONDOWN:
			{
				ReleaseCapture();
				SendMessage(hWnd, 0xA1, 0x2, 0);
				return 0;
			}
		case WM_CREATE:
			{
				WindowInvalidate();
				return 0;
			}
		case WM_DRAWITEM:
			{
				DRAWITEMSTRUCT* data= (DRAWITEMSTRUCT*)lParam;
				SetBkMode(data->hDC, TRANSPARENT);
				Graphics gfx(data->hDC);
				int btncmd = GetDlgCtrlID(data->hwndItem);
				if(data->itemState & ODS_SELECTED)
				{				
					gfx.Clear(Color::Yellow);
					gfx.DrawRectangle(new Pen(Color::Black), 2, 2, data->rcItem.right - data->rcItem.left - 5, data->rcItem.bottom - data->rcItem.top - 5);
					if(btncmd == BTN_QUIT)
					{
						gfx.DrawLine(new Pen(Color::Black),2,2,data->rcItem.right - data->rcItem.left - 3,data->rcItem.bottom-data->rcItem.top-3);
						gfx.DrawLine(new Pen(Color::Black),data->rcItem.right - data->rcItem.left - 3,2,2,data->rcItem.bottom-data->rcItem.top-3);
					} else if(btncmd == BTN_MIN)
					{
						gfx.DrawLine(new Pen(Color::Black),4,(data->rcItem.bottom-data->rcItem.top-1)/2,data->rcItem.right-data->rcItem.left - 5,(data->rcItem.bottom-data->rcItem.top-1)/2);
					}
				}
				else
				{
					gfx.Clear(Color::Transparent);
					gfx.DrawRectangle(new Pen(Color::White), 2, 2, data->rcItem.right - data->rcItem.left - 5, data->rcItem.bottom - data->rcItem.top - 5);
					if(btncmd == BTN_QUIT)
					{
						gfx.DrawLine(new Pen(Color::White),2,2,data->rcItem.right - data->rcItem.left - 3,data->rcItem.bottom-data->rcItem.top-3);
						gfx.DrawLine(new Pen(Color::White),data->rcItem.right - data->rcItem.left - 3,2,2,data->rcItem.bottom-data->rcItem.top-3);
					} else if(btncmd == BTN_MIN)
					{
						gfx.DrawLine(new Pen(Color::White),4,(data->rcItem.bottom-data->rcItem.top-1)/2,data->rcItem.right-data->rcItem.left - 5,(data->rcItem.bottom-data->rcItem.top-1)/2);
					}
				}
				
				return TRUE;
			}
		case WM_COMMAND:
			{
				switch(LOWORD(wParam))
				{
				case BTN_QUIT:
					{
						DestroyWindow(hWnd); break;
					}
				case BTN_MIN:
					{
						ShowWindow(hWnd,SW_SHOWMINIMIZED); break;
					}
				default:
					{
						return 0;
					}
				}
			}
		default:
			{
				return DefWindowProc(hWnd, message, wParam, lParam);
			}
	}
}

void WindowInvalidate()
{
	InvalidateRect(hWnd,NULL,false);
}

void WindowPaint(HDC& hdc)
{
	Graphics gfx(hdc);
	gfx.Clear(WindowStyle.colour);

	for(int i = 0; i < 4; i++)
	{
		DrawShape(gfx,ToColour(_lights[i]),colour[i].x,colour[i].y,colour[i].width,colour[i].height,OutlineThickness,colour[i].type);
	}
	
	if(OutlineThickness > 0)
	{
		unsigned int actualLoc = OutlineThickness / 2;
		gfx.DrawRectangle(new Pen(OutlineColour,OutlineThickness),(int)actualLoc,(int)actualLoc,WindowStyle.width-OutlineThickness,WindowStyle.height-OutlineThickness);
	}

	if(UpdateIcon){	WindowDrawIcon(); }
	SetWindowText(hWnd,ClockTitle(DisplayString).c_str());
}

void WindowSetSize(int& x, int& y, int& width, int& height)
{
	if(!InitialImportComplete)return;
	MoveWindow(hWnd,x,y,width,height,true);
}

void WindowDrawIcon()
{
    HDC hdc = CreateDC(TEXT("DISPLAY"),NULL,NULL,NULL);
    HBITMAP hBitmap = CreateCompatibleBitmap(hdc, 32, 32);
    HDC hdc1 = CreateCompatibleDC(hdc);
    HGDIOBJ hobj = SelectObject(hdc1, hBitmap);

	Graphics gfx(hdc1);
	SolidBrush sldBrs(ToColour(_lights[0]));
	gfx.FillRectangle(new SolidBrush(ToColour(_lights[0])),0,0,16,16);
	sldBrs.SetColor(ToColour(_lights[1]));
	gfx.FillRectangle(new SolidBrush(ToColour(_lights[1])),0,16,16,16);
	sldBrs.SetColor(ToColour(_lights[1]));
	gfx.FillRectangle(new SolidBrush(ToColour(_lights[2])),16,0,16,16);
	sldBrs.SetColor(ToColour(_lights[1]));
	gfx.FillRectangle(new SolidBrush(ToColour(_lights[3])),16,16,16,16);

    ICONINFO ii;
    ii.fIcon = true;
    ii.xHotspot = ii.yHotspot = 0;
    ii.hbmMask = ii.hbmColor = hBitmap;
	SetClassLong(hWnd, GCL_HICON, (LONG)CreateIconIndirect(&ii));

	DeleteObject(hobj);
	DeleteDC(hdc1);
	DeleteObject(hBitmap);
	DeleteDC(hdc);
}

void DrawShape(Graphics& gfx, Color& colour, unsigned int& x, unsigned int& y, unsigned int& width, unsigned int& height, unsigned int& thickness, byte& shape)
{
	switch(shape)
	{
	case 0: gfx.FillEllipse(new SolidBrush(colour),(int)x,(int)y,(int)width,(int)height); break;
	case 1: gfx.DrawEllipse(new Pen(colour,thickness),(int)x,(int)y,(int)width,(int)height); break;
	case 2: gfx.FillRectangle(new SolidBrush(colour),(int)x,(int)y,(int)width,(int)height); break;
	case 3: gfx.DrawRectangle(new Pen(colour,thickness),(int)x,(int)y,(int)width,(int)height); break;
	default:break;
	}
}