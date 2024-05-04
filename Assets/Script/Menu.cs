using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    public TMP_InputField username;
    public TMP_InputField password;

    public void CambiarEscena(string name)
    {
        if (string.IsNullOrEmpty(username.text) || string.IsNullOrEmpty(password.text))
        {
            Debug.Log("Por favor, completa todos los campos.");
            return; // No carga la escena si alguno de los campos está vacío
        }
        SceneManager.LoadScene(name);
    }
}
