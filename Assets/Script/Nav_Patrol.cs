using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Nav_Patrol : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject waypointParent;
    private GameObject[] waypoints;
    private int waypointintIndex = 0;
    private int maxWaypoints;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        maxWaypoints = waypointParent.transform.childCount;
        waypoints = new GameObject[maxWaypoints];

        for (int i = 0; i < maxWaypoints; i++)
        {
            waypoints[i] = waypointParent.transform.GetChild(i).gameObject;

        }
        GoToNextWaypoint();

    }

    // Update is called once per frame
    void Update() //Si he llegado al destino me voy al siguiente (sumo uno)
    {
        if (agent.remainingDistance < 0.1)
        {
            waypointintIndex = (waypointintIndex + 1) % maxWaypoints;
            GoToNextWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        agent.SetDestination(waypoints[waypointintIndex].transform.position);
    }
}
