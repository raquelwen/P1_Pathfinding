using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Nav_TerrainModifier : MonoBehaviour
{
    private NavMeshModifier _meshsurface;
    // Start is called before the first frame update
    void Start()
    {
        _meshsurface = GetComponent<NavMeshModifier>();      
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ha entrado en zona peligrosa");

        if(other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent)) //me aseguro que lo que ha entrado es de tipo agente
        {
            //la superficie debe afectar al tipo agente. Me aseguro que el agente se ve afectado por este tipo de terreno.
            if (_meshsurface.AffectsAgentType(agent.agentTypeID)){
                agent.speed /= NavMesh.GetAreaCost(_meshsurface.area);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Ha salido de zona peligrosa");
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent)) //me aseguro que lo que ha entrado es de tipo agente
        {
            //la superficie debe afectar al tipo agente. Me aseguro que el agente se ve afectado por este tipo de terreno.
            if (_meshsurface.AffectsAgentType(agent.agentTypeID))
            {
                agent.speed *= NavMesh.GetAreaCost(_meshsurface.area);
            }
        }

    }
}
