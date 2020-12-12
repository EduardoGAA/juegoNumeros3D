using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shooter : MonoBehaviour 
{
	private Ray rayo;
	private RaycastHit hit;
	public float distanciaDisparo;
	private Camera camara;
	private Vector2 centroCamara;
	public GameObject[] decalsPrefabs; //Array de los prefabs 
	public GameObject[] createdDecals; //Array para crear los Decals
	public int decalIndex;
	public float tempoDisparo;
	private float tempoUltimoDisparo;
	private Quaternion rotDecal;
	private Vector3 posDecal;
	public LayerMask decalLayerMask;

	public float fuerzahit = 150f; 
	public int escopetaDamage =1;
	ControlPlayer salud;
	private GameObject player;

	//---Conteo de balas----//
	public int balas;
	public TextMeshProUGUI contBalas;
	private int balasActual;
	public float tiempoMunicion = 10f;
	private GameObject miMunicion;
	private float tiempoActual;
    public Image primerQ;
	public Image segundaQ;
	public Image terceraQ;
    private GameObject cero;
	private GameObject uno;
	private GameObject dos;
	private GameObject tres;
	private GameObject cuatro;
	private GameObject cinco;
	private GameObject seis;
	private GameObject siete;
	private GameObject ocho;
	private GameObject nueve;

	void Awake()
	{
		this.camara = gameObject.transform.GetChild (0).GetComponent<Camera>();
		this.centroCamara.x= Screen.width/2;
		this.centroCamara.y= Screen.height/2;
		this.tempoUltimoDisparo = Time.time;

		this.player = GameObject.FindGameObjectWithTag("Player");
        this.salud = this.player.GetComponent<ControlPlayer>();

		cero = GameObject.Find("cero");
		uno = GameObject.Find("uno");
		dos = GameObject.Find("dos");
		tres = GameObject.Find("tres");
		cuatro = GameObject.Find("cuatro");
		cinco = GameObject.Find("cinco");
		seis = GameObject.Find("seis");
		siete = GameObject.Find("siete");
		ocho = GameObject.Find("ocho");
		nueve = GameObject.Find("nueve");
		this.miMunicion = GameObject.FindWithTag("Municion");
		this.tiempoActual = Time.time;
		this.balas = 3;
		this.contadorBalas();

		for(int decalNum=0; decalNum<this.createdDecals.Length; decalNum++)
		{
			this.createdDecals[decalNum] = GameObject.Instantiate(this.decalsPrefabs[0], Vector3.zero,Quaternion.identity)as GameObject;
			this.createdDecals[decalNum].GetComponent<Renderer>().enabled=false;     
		} 
		this.decalIndex = 0;	
	}

	void OnTriggerEnter(Collider otro) {
		if (otro.gameObject.CompareTag("Municion"))
		{
			otro.gameObject.SetActive(false);
			this.recargar();
			this.contadorBalas();
		}
	}

	public void recargar(){
		this.balas += 3;
		if (this.balas > 3)
		{
			this.balas = 3;
		}
	}

	public void contadorBalas(){
		this.contBalas.SetText("BALAS: "+this.balas.ToString());
	}

	void Update()
	{
		this.contadorBalas();
		if ((Time.time - this.tiempoMunicion) > this.tiempoMunicion)
		{
			this.miMunicion.SetActive(true);
			this.tiempoActual = Time.time;
		}
		if (!tres.activeSelf)
		{
			primerQ.gameObject.SetActive(false);
			segundaQ.gameObject.SetActive(true);
		}

		if (!seis.activeSelf)
		{
			segundaQ.gameObject.SetActive(false);
			terceraQ.gameObject.SetActive(true);
		}

		if(Input.GetButtonDown("Fire1"))
		{
			if (this.balas < 1)
			{
				return;
			}
			if((Time.time-this.tempoUltimoDisparo)>this.tempoDisparo)
			{	
				this.rayo= this.camara.ScreenPointToRay(this.centroCamara);
				this.tempoUltimoDisparo = Time.time;

				if(Physics.Raycast (this.rayo,out this.hit,this.distanciaDisparo, this.decalLayerMask))
				{
					this.rotDecal= Quaternion.FromToRotation(Vector3.forward,this.hit.normal);
					this.posDecal= this.hit.point+this.hit.normal* 0.01f; 
					this.createdDecals[this.decalIndex].transform.position=this.posDecal;
					this.createdDecals[this.decalIndex].transform.rotation=this.rotDecal;
					this.createdDecals[this.decalIndex].transform.parent=null;
					this.createdDecals[this.decalIndex].GetComponent<Renderer>().enabled=true;  

					if(this.hit.collider.tag=="Puerta" || this.hit.collider.tag=="Caja")
					{
						this.createdDecals[this.decalIndex].transform.parent= this.hit.collider.gameObject.transform;
					}
					this.decalIndex++;

					if(this.decalIndex>9)
					{
						this.decalIndex=0;
					}
					DestroyCajas salud = hit.collider.GetComponent<DestroyCajas>();

					if(salud != null)
					{
						salud.Damage(escopetaDamage);
					}
					if(hit.rigidbody != null)
					{
						hit.rigidbody.AddForce (-hit.normal*fuerzahit);
					} 
					this.balas -= 1;
					this.salud.sentirDolor(5);
				}
			}
		}
	}
}
