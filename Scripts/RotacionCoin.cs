using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionCoin : MonoBehaviour {


	void Update () 
	{
		gameObject.transform.Rotate (Vector3.forward);
		
	}
}
