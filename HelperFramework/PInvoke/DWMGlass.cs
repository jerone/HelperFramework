using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace HelperFramework.PInvoke
{
	public class DwmGlass
	{

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, Win32.MARGINS pMargins);
		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, Rectangle pMargins);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern bool DwmIsCompositionEnabled();

	}
}
