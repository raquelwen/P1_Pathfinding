using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Transactions;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    public GameObject[] vidas;
    //public string currentPuntos;
    //public string currentVidas; 
    
   
    // Update is called once per frame
    void Update()
    {
        puntos.text = gameManager.PuntosTotales.ToString();
        
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();  
        //currentPuntos = puntos.text;
        
    }

    public void DesactivarVida(int indice) //indice: cual de las vidas queremos desactivar
    {
        vidas[indice].SetActive(false); 
        //currentVidas = indice.ToString();
        
    }
    //public void ActivarVida(int indice)
    //{
    //    vidas[indice].SetActive(true);
    //}
}
