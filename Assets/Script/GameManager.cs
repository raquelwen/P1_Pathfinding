using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
   //Cuando leamos la propiedad PuntosTotales desde otro Script nos devuelve  la variable private puntosTotales. 
   //Propiedad solo de lectura
    public int PuntosTotales { get { return puntosTotales; } } 
    private int puntosTotales;

    public void SumarPuntos(int sumarPuntos)
    {
        puntosTotales += sumarPuntos;
        Debug.Log(puntosTotales);
    }
}
