using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public float tiempoAtaque;
    public int dolorAtaque;
    ControlPlayer salud;

    private GameObject player;
    private bool playerDentro;
    private float tiempo;

   void Awake() {
        this.tiempoAtaque = 0.5f;
        this.dolorAtaque = 10;
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.salud = this.player.GetComponent<ControlPlayer>();
    }

    void OnTriggerEnter(Collider otro) {
        if (otro.gameObject == this.player)
        {
            this.playerDentro = true;
        }
    }

    void OnTriggerExit(Collider otro){
        if (otro.gameObject == this.player){
            this.playerDentro = false;
        }
    }

    void ataque(){
        this.tiempo = 0f;
        if (this.salud.vidaActual > 0)
        {
            this.salud.sentirDolor(this.dolorAtaque);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.tiempo += Time.deltaTime;
        if (this.tiempo >= this.tiempoAtaque && this.playerDentro && this.salud.vidaActual > 0)
        {
            this.ataque();
        }
    }
}
