using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject controlsParent;

	[HideInInspector]
	public bool controlsActive = false;

	public void Controls()
	{
		if (controlsActive == false) 
		{
			controlsParent.SetActive (true);
			controlsActive = true;
		}
		else
		{
			controlsActive = false;
			controlsParent.SetActive (false);
		}
	}

	public void Play()
	{
		SceneManager.LoadScene (sceneBuildIndex: 1);
	}
}
