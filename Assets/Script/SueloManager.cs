using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class SueloManager : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshSurface surface;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        surface.BuildNavMesh(); 
    }
}
