using System.Runtime.InteropServices;
namespace ControlaMiPC.Helpers
{

	public static class MouseHelper
	{
		[DllImport("user32.dll")]
		private static extern bool SetCursorPos(int X, int Y);

		[DllImport("user32.dll")]
		private static extern bool GetCursorPos(out POINT lpPoint);

		[DllImport("user32.dll")]
		private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

		private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
		private const uint MOUSEEVENTF_LEFTUP = 0x0004;
		private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
		private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
		private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
		private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
		private const uint MOUSEEVENTF_WHEEL = 0x0800;
		private const uint MOUSEEVENTF_HWHEEL = 0x01000;

		public static void MoveMouse(int x, int y, bool relative)
		{
			if (relative)
			{
				GetCursorPos(out var current);
				x += current.X;
				y += current.Y;
			}

			SetCursorPos(x, y);
		}

		public static void ClickMouse(string button = "left", bool doubleClick = false)
		{
			var (down, up) = button.ToLower() switch
			{
				"right" => (MOUSEEVENTF_RIGHTDOWN, MOUSEEVENTF_RIGHTUP),
				"middle" => (MOUSEEVENTF_MIDDLEDOWN, MOUSEEVENTF_MIDDLEUP),
				_ => (MOUSEEVENTF_LEFTDOWN, MOUSEEVENTF_LEFTUP),
			};

			void ClickOnce()
			{
				mouse_event(down, 0, 0, 0, 0);
				mouse_event(up, 0, 0, 0, 0);
			}

			ClickOnce();
			if (doubleClick) ClickOnce();
		}

		public static void Scroll(int amount, bool horizontal)
		{
			var flag = horizontal ? MOUSEEVENTF_HWHEEL : MOUSEEVENTF_WHEEL;
			mouse_event(flag, 0, 0, (uint)amount, 0);
		}

		public static (int X, int Y) GetPosition()
		{
			GetCursorPos(out var p);
			return (p.X, p.Y);
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct POINT
		{
			public int X;
			public int Y;
		}
	}

}
