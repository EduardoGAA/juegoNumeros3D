using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void iniciarJuego(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
    public void replay(){
		Debug.Log("Reiniciar");
	}
    public void salir(){
		Application.Quit ();
		Debug.Log ("Salir");
	}
}
