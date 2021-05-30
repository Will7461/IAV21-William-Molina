using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.UI;

public class UIStateManager : MonoBehaviour
{
    public GameObject gO;

	public Sprite[] sprites;

	private void OnEnable()
	{
		updateUIState();
	}

	public void updateUIState()
	{
		string state = (string)Variables.Object(gO).Get("State");

		switch (state)
		{
			case "Wandering":
				transform.GetChild(0).gameObject.SetActive(false);
				transform.GetChild(1).gameObject.SetActive(false);
				break;
			case "Fighting":
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[0];
				break;
			case "Starving":
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[1];
				break;
			case "Following":
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[2];
				break;
			case "Sleeping":
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[3];
				break;
			case "RunningAway":
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[4];
				break;
			case "Death":
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[5];
				break;
			case "Loving":
				transform.GetChild(0).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.SetActive(true);
				transform.GetChild(1).gameObject.GetComponent<Image>().sprite = sprites[6];
				break;
		}
	}
}
