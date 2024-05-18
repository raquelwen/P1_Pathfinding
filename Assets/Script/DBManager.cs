using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using System.Data.Common;
using System.Drawing;
using UnityEditor;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.GraphView;
using static UnityEditor.PlayerSettings;

public class DBManager : MonoBehaviour
{
    private string dbUri = "URI=file:dbPathfinding.sqlite";
    private string SQL_CREATE_PLAYER = "CREATE TABLE IF NOT EXISTS Players" +
        " (Id INTEGER UNIQUE NOT NULL PRIMARY KEY" +        
        ", User TEXT NOT NULL" +
        ", Password NOT NULL);";
        
    private string SQL_COUNT_ELEMENTS = "SELECT count(*) FROM Players;";
    private string SQL_CREATE_USER = "CREATE TABLE IF NOT EXISTS Users" +        
        "(Id INTEGER UNIQUE NOT NULL PRIMARY KEY" +
        ", PlayerId INTEGER" +
        ", Puntos INTEGER NOT NULL DEFAULT 0" +
        ", Vida INTEGER NOT NULL DEFAULT 0"+
        ", FOREIGN KEY(PlayerId) REFERENCES Players(Id));";
    private string[] USUARIOS = { "Alejandro", "Raquel", "Shara", "Ainoa", "Ricardo", "Carlos"};
    public Menu menu;
    public IDbConnection dbConnection;
    public GameManager gameManager;

    void Start()
    {
        Debug.Log("Start");
    }

   

    public IDbConnection Opendatabase()
    {
        dbConnection = new SqliteConnection(dbUri);
        //Debug.Log(" no abre");
        dbConnection.Open();
        //Debug.Log("abre");
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = "PRAGMA foreign_keys = ON";
        dbCommand.ExecuteNonQuery();
        return dbConnection;
    }

    public void InitializeDB()
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();       
        dbCmd.CommandText = SQL_CREATE_PLAYER + SQL_CREATE_USER;      
        dbCmd.ExecuteReader();        
    }
   
    public void InsertNewUser()
    {
        //if (CountNumberElements() > 0)
        {
            string user = menu.currentUser;
            string password = menu.currentPassword;
            //int puntos = gameManager.currentPuntos;
            //int vidas = gameManager.currentVidas;


            Debug.Log("Insertando datos de login");
                string command = "INSERT INTO Players (User, Password) VALUES ";
                command += $"('{user}', '{password}'),";
                Debug.Log(command);
                command = command.Remove(command.Length - 1, 1);
                command += ";";

            //Debug.Log("Insertando datos del juego");
            //command = "INSERT INTO Users (Puntos, Vida) VALUES ";
            //command += $"('{puntos}', '{vidas}'),";
            //Debug.Log(command);
            //command = command.Remove(command.Length - 1, 1);
            //command += ";";

            IDbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = command;
            dbCommand.ExecuteNonQuery();
        }        
    }

    public void InsertDataUsers(int puntos, int vidas)
    {
        //int puntos = gameManager.currentPuntos;
        //  puntos = gameManager.puntosTotales;
        //int vidas = gameManager.currentVidas;
        //vidas = gameManager.vidas;

        Debug.Log("Insertando datos del juego");
        string command = "INSERT INTO Users (Puntos, Vida) VALUES ";
        command += $"('{puntos}', '{vidas}'),";
        Debug.Log(command);
        command = command.Remove(command.Length - 1, 1);
        command += ";";


        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = command;
        dbCommand.ExecuteNonQuery();

    }

    //public void AddRandomData()
    //{
    //    if (CountNumberElements() > 0)
    //    {
    //        return;
    //    }
    //    int num_usuarios = USUARIOS.Length;        
    //    string command = "INSERT INTO Users (Nombre) VALUES ";
    //    for (int i = 0; i < num_usuarios; i++)
    //    {
    //        command += $"('{USUARIOS[i]}'),";
    //    }
    //    command = command.Remove(command.Length - 1, 1);
    //    command += ";";
    //    IDbCommand dbCommand = dbConnection.CreateCommand();
    //    dbCommand.CommandText = command;
    //    dbCommand.ExecuteNonQuery();
    //}

    public bool EqualUser(string user, string password) //este funciona solo para user
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = $"SELECT COUNT(*) FROM Players WHERE User='{user}';";

        object result = dbCmd.ExecuteScalar();
        int count = Convert.ToInt32(result);
        Debug.Log(count);

        if (count > 0)
        {
            Debug.Log("Usuario existente en la base de datos");

            dbCmd.CommandText = $"SELECT Password FROM Players WHERE User='{user}';";
            object passwordResult = dbCmd.ExecuteScalar();
            //string storedPassword = passwordResult as string;
            string storedPassword = passwordResult.ToString();

            if (storedPassword == password)
            {                
                Debug.Log("Contraseña correcta");
                Debug.Log("Inicio de sesión exitoso.");
                SceneManager.LoadScene(1);
                dbConnection.Close();
                return true;
            }
            else
            {
                Debug.Log("Contraseña incorrecta");
                return false;
            }
            
        }
        else
        {
            Debug.Log("El usuario no existe");            
        }
        return false;
    }

    public bool Login(string user, string password)
    {      
        if (!EqualUser(user, password)) //si no existe: false
        {           
            Debug.Log("El usuario no existe: Cuenta creada");
            InsertNewUser();
            SceneManager.LoadScene(1);
            dbConnection.Close();
            return false;           
            
        }         
        return true;
    }

    private int CountNumberElements()
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = SQL_COUNT_ELEMENTS;
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Read();
        return reader.GetInt32(0);
    }

    private void OnDestroy()
    {
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
    }
}

