using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	public string[] animals;
   public bool isEdibleBy(string animal)
	{
		bool found = false;

		foreach (string s in animals)
		{
			if (animal.Contains(s))
			{
				found = true;
				break;
			}
		}

		return found;
	}
}
