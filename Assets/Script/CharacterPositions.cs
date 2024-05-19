using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPositions
{
    public string name;
    public float timestamp;
    public Vector3 position;

    public CharacterPositions(string name, float timestamp, Vector3 position)
    {
        this.name = name;
        this.timestamp = timestamp;
        this.position = position;
    }

    public override string ToString() //interpolated string
    {
        return $"{name} {timestamp} {position}";
    }
    public string ToCSV()
    {
        return $"{name} {timestamp} {position.x} {position.y} {position.z}";
    }
}
