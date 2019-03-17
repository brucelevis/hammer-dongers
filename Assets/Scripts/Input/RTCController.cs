using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTCController : MonoBehaviour
{
	private GameObject _console;
    void Start()
    {
		_console = GameObject.Find("RuntimeConsole");
		_console.SetActive (false);
    }
		
    void Update()
    {
		if (Input.GetKeyUp (KeyCode.Escape)) 
			_console.SetActive (!_console.activeSelf);
    }
}
