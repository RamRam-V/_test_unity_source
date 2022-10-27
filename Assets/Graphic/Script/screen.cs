using UnityEngine;
using System.Collections;

public class screen : MonoBehaviour {

	void Awake()
	{
		Screen.SetResolution( (int)(2048), (int)(768), false );
	}
}
