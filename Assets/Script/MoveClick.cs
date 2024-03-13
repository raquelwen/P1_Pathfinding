using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveClick : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    private NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) //click izquierdo
        {
            Debug.Log("Click");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                agent.SetDestination(hit.point);
            }   
        }

    }
}
