using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class ControlPlayer : MonoBehaviour
{
    private int contador;
    public TextMeshProUGUI contMonedas;
    public int vidaInicial;
    public int vidaActual;
    public Slider barraVida;
    
    public float tiempoBotiquin;
    private float tiempoActual;
    private GameObject miBotiquin;
    public Image imagen;

    // Start is called before the first frame update
    void Start()
    {
      
       
    }

    void Awake(){
        this.contador = 0;
        this.vidaInicial = 100;
        this.vidaActual = this.vidaInicial;
        this.miBotiquin = GameObject.FindWithTag("Botiquin");
        this.tiempoBotiquin = 5f;
        this.tiempoActual = Time.time;
    }

    public void sentirDolor(int cantidad){
        this.vidaActual -= cantidad;
        barraVida.value = this.vidaActual;
        if (vidaActual <= 0)
        {
            this.imagen.gameObject.SetActive(true);
            replay();

        }
    }


    public void curarse(){
        this.vidaActual = 100;
        barraVida.value = this.vidaActual;
        this.imagen.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if ((Time.time - this.tiempoActual) > this.tiempoBotiquin)
        {
            this.miBotiquin.SetActive(true);
            this.tiempoActual = Time.time;
        }
    }

    void OnTriggerEnter(Collider otro){
        if (otro.gameObject.CompareTag("Item")){
            otro.gameObject.SetActive(false);
            this.contador += 1;
            this.contadorTexto();
        }
        if (otro.gameObject.CompareTag("Botiquin"))
        {
            if (this.vidaActual < 100)
            {
                otro.gameObject.SetActive(false);
                this.curarse();
            }
        }
    }

    public void contadorTexto(){
        contMonedas.SetText("COINS: " + this.contador.ToString());
    }
    public void replay(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}
}
