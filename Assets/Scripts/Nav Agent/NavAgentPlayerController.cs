using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentPlayerController : MonoBehaviour
{
    public NavMeshSurface surface;

    public Camera cam;
    public NavMeshAgent agent;

    void Start()
    {
        if (cam = null) cam = Camera.main;
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // move agent
                agent.SetDestination(hit.point);
            }

        }
    }
}
