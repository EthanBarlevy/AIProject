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
        agent.wanderAngle = agent.wanderAngle + Random.Range(-agent.wanderDisplacement, agent.wanderDisplacement);
        // create rotation (around y-axis)
        Quaternion rotation = Quaternion.AngleAxis(agent.wanderAngle, Vector3.up);
        // calculate point on circle
        Vector3 point = rotation * (Vector3.forward * agent.wanderRadius);
        // set point in front of agent
        Vector3 forward = agent.transform.forward * agent.wanderDistance;

        return CalculateSteering(agent, forward + point);
    }

    public static Vector3 CalculateSteering(Agent agent, Vector3 direction)
    {
        Vector3 desired = direction.normalized * agent.movement.maxSpeed;
        Vector3 steer = desired - agent.movement.velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, agent.movement.maxForce);

        return force;
    }
}
