using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCajas : MonoBehaviour {

	public int saludActual=3;

	public void Damage(int cantidadDamage)
	{
		this.saludActual -= cantidadDamage;
		if(this.saludActual <=0)
		{
			gameObject.SetActive(false);
		}
	}
}
