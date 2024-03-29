using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
   //Cuando leamos la propiedad PuntosTotales desde otro Script nos devuelve  la variable private puntosTotales. 
   //Propiedad solo de lectura
    public int PuntosTotales { get { return puntosTotales; } } 
    private int puntosTotales;
    public HUD hud;
    private int vidas = 3;

    public void SumarPuntos(int sumarPuntos)
    {
        puntosTotales += sumarPuntos;   
        Debug.Log(puntosTotales);
        hud.ActualizarPuntos(PuntosTotales);
    }

    public void PerderVida()
    {
        vidas -= 1;  

        if(vidas == 0)
        {
            SceneManager.LoadScene(1);
        }

        hud.DesactivarVida(vidas); 
        //Si tenemos 3 vidas y perdemos 1, nos quedamos con 2 vidas. Desactivamos la vida en la posicion 2. 
        //La posicion de la vida que queremos desactivar coincide con el numero de vidas despues de haber perdido 1. 
    }

    public void GanarJuego()
    {
        if(puntosTotales == 12)
        {
            SceneManager.LoadScene(2);
        }
    }
}
