using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSaver : MonoBehaviour 
{

	public void SaveName()
	{
		PlayerPrefs.SetString("Name", GameObject.Find("Player Name").GetComponent<InputField>().text);
	}
}
