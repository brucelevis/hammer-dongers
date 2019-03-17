using System;
using RTC.Base;
using RTC.Core;
using RTC.Core.Extension;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace RTC
{
	public static class ConsoleCommands
	{
		[Command(Alias = "quit", Description = "Quits the application.", Usage = "quit")]
		public static void Quit()
		{
			Application.Quit();
		}

		[Command(Alias = "grid", Description = "Inspects grid.", Usage = "grid")]
		public static void Grid()
		{
			var grid = ConsoleManager.Instance.GridBehaviour;
			Debug.Log ("Rows: " + grid.matrix.rows);
			Debug.Log ("Columns: " + grid.matrix.columns);
		}
	}
}