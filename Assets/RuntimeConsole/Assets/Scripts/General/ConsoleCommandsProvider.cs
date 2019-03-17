using RTC.Core.Extension;
using RTC.Interface;
using System;

namespace RTC
{
	public class ConsoleCommandsProvider : ITypesProvider
	{
		public Type[] GetTypes()
		{
			return typeof(ConsoleCommands).ToOneItemArray();
		}
	}
}
