using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public static bool WritetoFile(string filename, string data)
    {
        // Limpiar el nombre del archivo para eliminar caracteres no válidos


        string fullPath = Path.Combine(Application.persistentDataPath, filename);

        try
        {
            File.WriteAllText(fullPath, data);
            Debug.Log("Fichero guardado correctamente en: " + fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Error al guardar fichero" + filename + e);
            return false;
        }
    }
}
