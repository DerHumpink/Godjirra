using UnityEngine;
using System.Collections;
using System.Linq;

public class IdManager : MonoBehaviour {

	public static int GetSceneID()
	{
		int id = 1;
		while (DialogStageBehaviour.AllBehaviours.Any(s => s.Stage.Id==id))
		{
			id++;
		}
		return id;
	}
}
