using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;
   
    // Update is called once per frame
    void Update()
    {
        puntos.text = gameManager.PuntosTotales.ToString();
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();        
    }

    public void DesactivarVida(int indice) //indice: cual de las vidas queremos desactivar
    {
        vidas[indice].SetActive(false); 
    }
    //public void ActivarVida(int indice)
    //{
    //    vidas[indice].SetActive(true);
    //}
}
