using System.Collections.Generic;
using System.Linq;
using Editor;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
	public class MenuItems
	{
		[MenuItem("Stuff/CreateSettingsObjects")]
		private static void DoSomething()
		{
			ParameterSetup.CreateIfNotExisting();
			DialogTreeSetup.CreateIfNotExisting();
		}
	}
}