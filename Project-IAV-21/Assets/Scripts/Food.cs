using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class for every Food object
/// </summary>
public class Food : MonoBehaviour
{
	/// <summary>
	/// Animals that can eat this food
	/// </summary>
	public string[] animals;
	/// <summary>
	/// Check if animal is part of the animals array, in that case, the animal can eat this food
	/// </summary>
	/// <param name="animal">animal name</param>
	/// <returns></returns>
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
