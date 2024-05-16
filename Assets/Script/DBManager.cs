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

public class DBManager : MonoBehaviour
{
    private string dbUri = "URI=file:dbPathfinding.sqlite";
    private string SQL_CREATE_PLAYER = "CREATE TABLE IF NOT EXISTS Players (Id INTEGER UNIQUE NOT NULL PRIMARY KEY, User TEXT NOT NULL DEFAULT 'New User', Password NOT NULL);";
    private string SQL_COUNT_ELEMENTS = "SELECT count(*) FROM Players;";
    private string[] USERS = {};
    private string[] PASSWORDS = {};  
    public Menu menu;
    public IDbConnection dbConnection;
    


    void Start()
    {
        Debug.Log("Start");

        //dbConnection = Opendatabase();
        //InitializeDB();
 
 
        //Debug.Log("Data");
        //dbConnection.Close();
        //Debug.Log("End");

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
        dbCmd.CommandText = SQL_CREATE_PLAYER;
        dbCmd.ExecuteReader();
    }

    public void InsertNewUser()
    {
        if (CountNumberElements() > 0)
        {
            string user = menu.currentUser;
            string password = menu.currentPassword;
            
                Debug.Log("Insertando datos");

                string command = "INSERT INTO Players (User, Password) VALUES ";

                command += $"('{user}', '{password}'),";

                Debug.Log(command);

                command = command.Remove(command.Length - 1, 1);
                command += ";";

                Debug.Log(command);

                IDbCommand dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = command;
                dbCommand.ExecuteNonQuery();
            
        
        }        
    }
    //private bool SearchUserExists(string user)
    //{
    //    IDbCommand dbCmd = dbConnection.CreateCommand();
    //    dbCmd.CommandText = $"SELECT User FROM Players WHERE User='{user}';";
        
    //    object result = dbCmd.ExecuteScalar();
    //    if (result != null && result != DBNull.Value)
    //    {
    //        int count;
    //        if (int.TryParse(result.ToString(), out count))
    //        {
    //            return count > 0;
    //        }
    //    }

    //    return false;
    //    //Devuelve el numero de filas que coinciden con el usuario proporcionado y lo compara con 0
    //    //Si existe devuelve true, sino false. 
    //}

    public bool Equal(string user)
    {
        IDbCommand dbCmd = dbConnection.CreateCommand();
        dbCmd.CommandText = $"SELECT User FROM Players WHERE User='{user}';";
        if (user == menu.currentUser)
        {
           Debug.Log("Equal");
            return true;
        }
        else
        {
            Debug.Log("No equal");
            return false;
        }
    }

    public bool Login(string user, string password)
    {
        //if (!SearchUserExists(user))
        //{
        //    Debug.Log("El usuario no existe: Cuenta creada");
        //    InsertNewUser();
        //    return false;
        //}
        if (!Equal(user))
        {
            Debug.Log("El usuario no existe: Cuenta creada");
            InsertNewUser();
            return false;
        }

        else
        {
    IDbCommand dbCmd = dbConnection.CreateCommand();
                dbCmd.CommandText = $"SELECT Password FROM Players WHERE User='{user}';";

                object result = dbCmd.ExecuteScalar();
                string storedPassword = result as string;

                if (storedPassword == password)
                {
                    Debug.Log("Inicio de sesión exitoso.");
                SceneManager.LoadScene(1);
                    dbConnection.Close();
                return true;
                }
                else
                {
                    Debug.Log("Contraseña incorrecta.");
                    return false;
                }
        }

        
        

    }

    private int CountNumberElements()
    {
        IDbCommand dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = SQL_COUNT_ELEMENTS;
        IDataReader reader = dbCommand.ExecuteReader();
        reader.Read();
        return reader.GetInt32(0);
    }
}

