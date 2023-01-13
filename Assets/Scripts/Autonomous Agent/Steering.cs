using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Steering
{
    public static Vector3 Seek(Agent agent, GameObject target)
    {
        return CalculateSteering(agent, (target.transform.position - agent.transform.position));
    }

    public static Vector3 Flee(Agent agent, GameObject target)
    {
        return CalculateSteering(agent, (agent.transform.position - target.transform.position));
    }

    public static Vector3 Wander(AutonomousAgent agent)
    {
        // random angle adjust
        agent.wanderAngle = agent.wanderAngle + Random.Range(-agent.data.wanderDisplacement, agent.data.wanderDisplacement);
        // create rotation (around y-axis)
        Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);
        // calculate point on circle
        Vector3 point = rotation * (Vector3.forward * agent.data.wanderRadius);
        // set point in front of agent
        Vector3 forward = agent.transform.forward * agent.data.wanderDistance;

        Debug.DrawRay(agent.transform.position, forward + point, Color.red);

        return CalculateSteering(agent, forward + point);
    }

    public static Vector3 Cohesion(Agent agent, GameObject[] neighbors)
    { 
        // get center point
        Vector3 center = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            center += neighbor.transform.position;
        }
        center /= neighbors.Length;

        return CalculateSteering(agent, center - agent.transform.position);
    }

    public static Vector3 Separation(Agent agent, GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            Vector3 direction = agent.transform.position - neighbor.transform.position;
            if (direction.magnitude < radius)
            {
                separation += direction / direction.sqrMagnitude;
            }
        }

        return CalculateSteering(agent, separation);
    }

    public static Vector3 Alignment(Agent agent, GameObject[] neighbors)
    {
        Vector3 averageVelocity = Vector3.zero;
        foreach (GameObject neighbor in neighbors)
        {
            averageVelocity += neighbor.GetComponent<Agent>().movement.velocity;
        }
        averageVelocity /= neighbors.Length;

        return CalculateSteering(agent, averageVelocity);
    }

    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 desired = direction.normalized * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        return force;
    }
}
