using System;
using System.Runtime.InteropServices;

namespace HelperFramework.PInvoke
{
	public class Hook
	{

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int RegisterShellHookWindow(IntPtr hwnd);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool DeregisterShellHookWindow(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int RegisterWindowMessage(string name);
	}
}
