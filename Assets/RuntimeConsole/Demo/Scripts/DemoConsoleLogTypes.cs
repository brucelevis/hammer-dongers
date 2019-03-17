using RTC.Base;
using RTC.Core;

namespace RTC.Demo
{
	public static class ConsoleLogTypes
	{
		// New type
		public const string None = "None";

		// Extension ( this can be ignored )
		public static void LogType(this RTConsole console, string type, string title)
		{
			console.Logger.Add(new Log(type, title, string.Empty));
		}
	}
}
