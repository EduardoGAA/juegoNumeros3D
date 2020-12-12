using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPuerta : MonoBehaviour 
{
	public bool abriendo= false;
	public bool cerrando= false;
	public bool abierta= false;
	public Transform puertaPosicion;
	public Vector3 posicionAbierta;
	public Vector3 posicionCerrada;
	public Vector3 posicionFinal;

	public float recorrido;
	public float tiempoInicio;
	public float tiempoApertura;

	void Start () 
	{
		this.puertaPosicion = gameObject.transform.GetChild (0);
		this.posicionCerrada = this.puertaPosicion.transform.localPosition;
		this.posicionFinal = new Vector3 (0f, 0.1f, 4f);
		this.posicionAbierta = this.posicionCerrada + this.posicionFinal;

	}

	void OnTriggerEnter (Collider colisionador)
	{
		this.tiempoInicio = Time.time;
		this.abriendo = true;

	}

	void OnTriggerExit (Collider colisionador)
	{
		this.tiempoInicio = Time.time;
		this.cerrando = true;

	}

	void Update ()
	{
		if (this.abriendo) 
		{
			this.recorrido = (Time.time - this.tiempoInicio) / tiempoApertura;
			this.puertaPosicion.transform.localPosition = new Vector3(0f,0.1f,Mathf.Lerp(this.posicionCerrada.z,this.posicionAbierta.z,this.recorrido));
			if (this.puertaPosicion.localPosition.z == this.posicionAbierta.z) 
			{
				this.abriendo = false;
			}
		}
		if (this.cerrando) 
		{
			this.recorrido=(Time.time - this.tiempoInicio) / tiempoApertura;
			this.puertaPosicion.transform.localPosition = new Vector3(0f,0.1f,Mathf.Lerp(this.posicionAbierta.z,this.posicionCerrada.z,this.recorrido));
			if (this.puertaPosicion.localPosition.z == this.posicionCerrada.z) 
			{
				this.cerrando = false;
			}
		}
	}
}
