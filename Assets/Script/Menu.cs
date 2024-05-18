using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;


public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField username;
    public TMP_InputField password;
    public DBManager dbManager; 

    public string currentUser;
    public string currentPassword;



    public void CambiarEscena(string name)
    {
        if (string.IsNullOrEmpty(username.text) || string.IsNullOrEmpty(password.text))
        {
            Debug.Log("Por favor, completa todos los campos.");
            return; // No carga la escena si alguno de los campos está vacío
        }
            currentUser = username.text;    
            currentPassword = password.text;

        //dbManager.dbConnection.Open();
        dbManager.Opendatabase();
        Debug.Log("Base de datos abierta");
        dbManager.InitializeDB();

       
        dbManager.Login(currentUser, currentPassword);

       
        //SceneManager.LoadScene(name);
        //dbManager.dbConnection.Close();
        //currentUser = null;
        //currentPassword=null;   

    }
}
