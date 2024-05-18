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
    public int puntosTotales = 0;
    public HUD hud;
    public int vidas = 3;
    public DBManager dbManager;
    public int currentPuntos;
    public int currentVidas;
    public Menu menu;

    public static GameManager Instance;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            //Primera vez. Instancia unica.
            GameManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameManager(HUD _hud, DBManager _dBManager)
    {
        dbManager = _dBManager;
        hud = _hud;
    }
    public void SumarPuntos(int sumarPuntos)
    {
        puntosTotales += sumarPuntos;   
        Debug.Log(puntosTotales);
        if (hud != null)
        {
            hud.ActualizarPuntos(PuntosTotales);
        }
        else
        {
            Debug.LogError("HUD is not assigned in the GameManager.");
        }                
        currentPuntos = puntosTotales;       
    }

    public void PerderVida()
    {
        vidas -= 1;  

        if(vidas == 0)
        {
            if (dbManager != null)
            {
               dbManager.Opendatabase();
                
                dbManager.InsertDataUsers(puntosTotales, vidas); // Pass the variables directly
                
                
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.LogError("DBManager is not assigned in the GameManager.");
            }
            
            //dbManager.InsertDataUsers();
        }

        if (hud != null)
        {
            hud.DesactivarVida(vidas);
        }
        else
        {
            Debug.LogError("HUD is not assigned in the GameManager.");
        }
        //Si tenemos 3 vidas y perdemos 1, nos quedamos con 2 vidas. Desactivamos la vida en la posicion 2. 
        //La posicion de la vida que queremos desactivar coincide con el numero de vidas despues de haber perdido 1. 
        currentVidas = vidas;        
        Debug.Log("Perdiendo vidas");
    }

    public void GanarJuego()
    {
        if(puntosTotales == 1)
        {
            if (dbManager != null)
            {
                dbManager.Opendatabase();
               
                dbManager.InsertDataUsers(puntosTotales, vidas);
                
                //dbManager.AddRandomData();
                SceneManager.LoadScene(3);
            }
            else
            {
                Debug.LogError("DBManager is not assigned in the GameManager.");
            }
            
            //dbManager.InsertDataUsers(); //inserto datos cuando termino el juego
        }
    }

    


}
