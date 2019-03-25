using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthBehaviour : MonoBehaviour
{
	public bool Continous = true;

	void SetDepth ()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
	}

    void Start()
    {
		SetDepth ();
    }
		
    void Update()
    {
		if(Continous)
			SetDepth ();
    }
}
