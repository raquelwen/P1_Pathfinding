using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataManager : MonoBehaviour
{
    public GameObject player;
    private float prevTime;
    private float prevSaveTime;
    private float logSaveInterval = 5;
    private float logInterval = 1;
    private List<CharacterPositions> playerPositions;
    private Positions playerpos;

    void Start()
    {
        prevTime = Time.realtimeSinceStartup;
        prevSaveTime = prevTime;
        playerPositions = new List<CharacterPositions>();

    }

    // Update is called once per frame
    void Update()
    {
        float currentTime = Time.realtimeSinceStartup;
        if (currentTime > prevTime + logInterval)
        {
            prevTime += logInterval;
            CharacterPositions cp = new CharacterPositions(player.name, currentTime, player.transform.position);
            playerPositions.Add(cp);            
        }
        if (currentTime > prevSaveTime + logSaveInterval)
        {
            prevSaveTime += logSaveInterval;
            SaveCSVToFile();
            SaveJSONToFile();
        }

        void SaveCSVToFile()
        {
            string data = "Name; Timestamp; x; y; z\n";
            foreach (CharacterPositions cp in playerPositions)
            {
                data += cp.ToCSV() + "\n";
            }

            //Debug.Log(data);
            FileManager.WritetoFile("positions.csv", data);
        }

        void SaveJSONToFile()
        {
            string data = "[";
            //generamos el json de forma automática 
            foreach (CharacterPositions cp in playerPositions)
            {
                data += JsonUtility.ToJson(cp) + "\n";
            }

            data += "]";
            FileManager.WritetoFile("positions.json", data);
        }
    }
}
